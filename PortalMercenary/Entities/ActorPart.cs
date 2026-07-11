using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Graphics;

namespace PortalMercenary.Entities;

public class ActorPart
{
    private readonly Actor _actor;
    public readonly Sprite[] Sprites;

    public Vector2 Position { get; set; }
    public float Rotation { get; set; }
    public float Depth { get; set; }
    public Vector2 Shift { get; set; }
    
    public ActorPart(Actor actor, Sprite[] sprites)
    {
        _actor = actor;
        Sprites = sprites;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        Sprites[(int)_actor.Direction].Draw(spriteBatch, _actor.Position + Shift + Position, Rotation, Vector2.One);
    }
}