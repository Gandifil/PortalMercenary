using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using PortalMercenary.Entities;
using PortalMercenary.Entities.Animations;
using PortalMercenary.Extensions;
using PortalMercenary.Game.Controllers;

namespace PortalMercenary.Game;

public class Character
{
    private readonly CharacterOptions _options;
    public Actor Actor { get; set; }

    public required ICharacterController Controller { get; init; }
    public Vector2 Position => Actor.Position;

    public Character(Vector2 position, CharacterOptions options)
    {
        _options = options;
        Actor = new Actor(G.Content.FreeTexPackerSpritesheets[options.Atlas].ToTexture2DAtlas())
        {
            Position = position
        };
    }

    public void Update(float dt)
    {
        Controller.Update(this, dt);
        Actor.Update(dt);
    }

    public void Draw(SpriteBatch gameSpriteBatch)
    {
        Actor.Draw(gameSpriteBatch);
    }

    public void Attack()
    {
        if (Actor.IsFree)
        {
            Actor.Start(new AttackAnimation()
            {
                MaxTime = _options.Attack.ActorAnimationDuration,
            });
            var rotation = Actor.Shift.ToAngle() - MathF.PI / 2;
            var pos = new Vector2(1, 0) * 60f;
            pos.Rotate(rotation);
            G.Game.Animations.Spawn(G.Content.Spritesheets["viking"], "Attack", Actor.Position + pos, rotation);
            G.Content.Sounds["swing3"].Play();
        }
    }
}