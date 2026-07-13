using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using MonoGame.Extended.Graphics;
using PortalMercenary.Entities.Animations;

namespace PortalMercenary.Game;

public class AttackProcessor
{
    private readonly SoundEffect _contentSound;
    private readonly CharacterOptions.AttackOptions _options;
    private readonly SpriteSheet _contentSpritesheet;

    public AttackProcessor(CharacterOptions.AttackOptions options)
    {
        _options = options;
        _contentSpritesheet = G.Content.Spritesheets[_options.SpriteSheet];
        _contentSound = G.Content.Sounds[_options.Sound];
    }
    
    public void Execute(Character attacker)
    {
        var actor = attacker.Actor;
        var rotation = actor.Shift.ToAngle() - MathF.PI / 2;
        var localPosition = new Vector2(1, 0) * 60f;
        localPosition.Rotate(rotation);
        var globalPosition = actor.Position + localPosition;
        
        actor.Start(new AttackAnimation()
        {
            MaxTime = _options.ActorAnimationDuration,
        });
        G.Game.Animations.Spawn(_contentSpritesheet, "Attack", globalPosition, rotation);
        _contentSound.Play();
        var collActor = new StrikeCollisionActor(globalPosition);
        
        G.Game.CollisionWorld.Insert(collActor);
        foreach (var collision in G.Game.CollisionWorld.QueryCollisions(collActor, null))
            if (collision.Other is Character other && other != attacker)
                other.Damage(localPosition / 2);
        G.Game.CollisionWorld.Remove(collActor);
    }
}
