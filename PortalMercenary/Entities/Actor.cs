using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Graphics;

namespace PortalMercenary.Entities;

public class Actor
{
    private readonly Texture2DRegion _boot;

    private readonly ActorPart[] _parts;

    public Vector2 Position { get; set; } = Vector2.Zero;

    private double _t = 0; 
    
    public Actor(Texture2DAtlas ironSet)
    {
        _boot = ironSet.GetRegion("boot_profile_01");
        
        
        _parts = new ActorPart[2];
        _parts[0] = new ActorPart(this, _boot);
        _parts[1] = new ActorPart(this, _boot);
    }
    
    public void Update(GameTime gameTime, Vector2 shift)
    {
        if (shift == Vector2.Zero)
            return;
        
        
        
        Position += shift;
        const int speed = 3;

        var dt = gameTime.ElapsedGameTime.TotalSeconds * speed;
        if (shift.X > 0)
        {
            _parts[0].Sprite.Effect = SpriteEffects.None;
            _parts[1].Sprite.Effect = SpriteEffects.None;
            _t += dt;
        }
        else
        {
            _parts[0].Sprite.Effect = SpriteEffects.FlipHorizontally;
            _parts[1].Sprite.Effect = SpriteEffects.FlipHorizontally;
            _t -= dt;
        }
        var pos = new Vector2(3 * (float)Math.Cos(_t), 30 * (float)Math.Sin(_t));

        pos.Rotate(shift.ToAngle());
        _parts[0].Position = pos;
        _parts[1].Position = -pos;
    }

    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        spriteBatch.DrawCircle(Position, 5, 16, Color.Red);
        _parts[0].Draw(spriteBatch);
        _parts[1].Draw(spriteBatch);
    }
}