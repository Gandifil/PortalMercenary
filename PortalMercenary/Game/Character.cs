using System;
using System.Collections.Specialized;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using PortalMercenary.Entities;
using PortalMercenary.Entities.Animations;
using PortalMercenary.Extensions;
using PortalMercenary.Game.Controllers;

namespace PortalMercenary.Game;

public class Character: ICollisionActor
{
    private readonly CharacterOptions _options;
    private readonly AttackProcessor _attackProcessor;
    public Actor Actor { get; set; }

    public required ICharacterController Controller { get; init; }
    public Vector2 Position => Actor.Position;

    public Character(Vector2 position, CharacterOptions options)
    {
        Id = GetHashCode();
        _options = options;
        _attackProcessor = new AttackProcessor(options.Attack);
        Actor = new Actor(G.Content.FreeTexPackerSpritesheets[options.Atlas].ToTexture2DAtlas())
        {
            Position = position
        };
    }

    public void Update(float dt)
    {
        var oldPosition = Actor.Position;
        Controller.Update(this, dt);
        Actor.Update(dt);
        
        foreach (var collision in G.Game.CollisionWorld.QueryCollisions(this, null))
        {
            Actor.Position = oldPosition;
        }
    }

    public void Draw(SpriteBatch gameSpriteBatch)
    {
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
        Actor.Position += pos;
    }

    public int Id { get; }

    public CollisionShape2D Shape
    {
        get
        {
            var bounds = new BoundingCircle2D(Position, 20);
            return new CollisionShape2D(bounds);
        }
    }
}