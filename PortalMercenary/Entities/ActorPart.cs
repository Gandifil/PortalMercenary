using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Graphics;
using PortalMercenary.Graphics;

namespace PortalMercenary.Entities;

public class ActorPart
{
    private readonly Actor _actor;
    public readonly CuttingSprite[] Sprites;

    public Vector2 Position { get; set; }
    public float Rotation { get; set; }
    public float Depth { get; set; }
    public Vector2 Shift { get; set; }
    
    public bool IsCut {get; private set; }
    
    public ActorPart(Actor actor, Sprite[] sprites)
    {
        _actor = actor;
        Sprites = sprites.Select(x => new CuttingSprite(x)).ToArray();
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        var sprite = Sprites[(int)_actor.Direction];
        sprite.Draw(spriteBatch, _actor.Position + Shift + Position, Rotation, Vector2.One);
    }
    
    public void Cut(CuttingSprite.Direction direction)
    {
        IsCut = true;
        foreach (var sprite in Sprites)
            sprite.Cut(direction);
    }
}