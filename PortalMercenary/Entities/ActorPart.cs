using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Graphics;

namespace PortalMercenary.Entities;

public class ActorPart
{
    private readonly Actor _actor;
    public readonly Sprite Sprite;

    public Vector2 Position { get; set; }
    
    public ActorPart(Actor actor, Texture2DRegion texture2DRegion)
    {
        _actor = actor;
        Sprite = new Sprite(texture2DRegion)
        {
            OriginNormalized = new Vector2(0.5f, 0.5f)
        };
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        Sprite.Draw(spriteBatch, _actor.Position + Position, 0f, Vector2.One);
    }
}