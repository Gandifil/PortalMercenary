using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monogame.Enchanted.Debug;
using MonoGame.Extended;
using MonoGame.Extended.Tweening;
using PortalMercenary.Entities;
using PortalMercenary.Extensions;
using PortalMercenary.Game.Controllers;

namespace PortalMercenary.Game;

public class Character: Entity
{
    private readonly CharacterOptions _options;
    private readonly AttackProcessor _attackProcessor;
    
    public Actor Actor { get; set; }

    public required ICharacterController Controller { get; init; }

    public bool IsRunning { get; set; }

    public Character(Vector2 position, CharacterOptions options): base(position)
    {
        _options = options;
        _attackProcessor = new AttackProcessor(options.Attack);
        Actor = new Actor(this, G.Content.FreeTexPackerSpritesheets[options.Atlas].ToTexture2DAtlas());
    }

    public override void Update(float dt)
    {
        var oldPosition = Position;
        Controller.Update(this, dt);

        var speed = IsRunning ? _options.RunMovementSpeed : _options.MovementSpeed;
        Position += Actor.Shift * speed * dt;
        
        foreach (var collision in G.Game.CollisionWorld.QueryCollisions(this, null))
        {
            Position = oldPosition;
        }
        Actor.Update(IsRunning ? 3*dt : dt);
    }

    public void Draw(SpriteBatch gameSpriteBatch)
    {
        DebugHighlight.DrawCircle(gameSpriteBatch, Position, 20);
        Actor.Draw(gameSpriteBatch);
    }

    public void Attack()
    {
        if (Actor.IsFree)
        {
            _attackProcessor.Execute(this);
        }
    }

    public void Damage(Vector2 pos)
    {
        G.Tweener.TweenTo(
                target: this, 
                expression: x => x.Position, 
                toValue: Position + pos, 
                duration: .1f, 
                delay: 0)
            .Easing(EasingFunctions.ElasticOut);
        G.Game.DecalsComponent.Add(Position);
        G.Content.Sounds[_options.DamageSounds[Random.Shared.Next(_options.DamageSounds.Length)]].Play();

        Actor.CutAnything();
    }

    public override CollisionShape2D Shape
    {
        get
        {
            var bounds = new BoundingCircle2D(Position, 20);
            return new CollisionShape2D(bounds);
        }
    }
}