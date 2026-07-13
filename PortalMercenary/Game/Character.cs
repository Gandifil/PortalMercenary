using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Graphics;
using PortalMercenary.Entities;
using PortalMercenary.Entities.Animations;
using PortalMercenary.Extensions;
using PortalMercenary.Game.Controllers;

namespace PortalMercenary.Game;

public class Character
{
    private readonly CharacterOptions _options;
    public Actor Actor { get; set; }

    public PlayerController CharacterController { get; set; }
    
    public Character(CharacterOptions options)
    {
        _options = options;
        var atlas = G.Content.FreeTexPackerSpritesheets[options.Atlas].ToTexture2DAtlas();
        Actor = new Actor(atlas);
        CharacterController  = new PlayerController(this);
    }

    public void Update(float dt)
    {
        Actor.Update(dt, CharacterController.Update(dt));
    }

    public void Draw(SpriteBatch gameSpriteBatch)
    {
        Actor.Draw(gameSpriteBatch);
    }

    public void Attack()
    {
        Actor.Start(new AttackAnimation()
        {
            MaxTime = _options.Attack.ActorAnimationDuration,
        });
        var rotation = Actor.Shift.ToAngle() - MathF.PI / 2;
        var pos = new Vector2(1, 0) * 60f;
        pos.Rotate(rotation);
        G.Game.Animations.Spawn(G.Content.Spritesheets["viking"], "Attack", Actor.Position + pos, rotation);
    }
}