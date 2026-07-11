using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Graphics;

namespace PortalMercenary.Entities;

public class ActorPart
{
    private readonly Actor _actor;
    public readonly Sprite[] Sprites = new Sprite[4];

    public Vector2 Position { get; set; }
    public Vector2 Shift { get; set; }
    
    public ActorPart(Actor actor, Sprite[] sprites)
    {
        _actor = actor;
        Sprites = sprites;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        Sprites[(int)_actor.Direction].Draw(spriteBatch, _actor.Position + Shift + Position, 0f, Vector2.One * .25f);
    }
}