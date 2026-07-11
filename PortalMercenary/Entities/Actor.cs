using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Graphics;

namespace PortalMercenary.Entities;

public class Actor
{
    private readonly ActorPart[] _parts, _legs;
    private readonly ActorPart _head, _body;

    public Vector2 Position { get; set; } = Vector2.Zero;

    public ActorOptions Options { get; set; } = new();

    public Direction Direction { get; set; }

    private double _t = 0; 
    
    public Actor(Texture2DAtlas textureAtlas)
    {
        _parts = new ActorPart[4];
        _legs = new ActorPart[2];

        var bootSprites = AtlasParser.GetBootSprites(textureAtlas);
        _parts[0] = _legs[0] = new ActorPart(this, bootSprites)
        {
            Shift = new  Vector2(-10, 0),
        };
        
        _parts[1] = _legs[1] = new ActorPart(this, bootSprites)
        {
            Shift = new  Vector2(10, 0)
        };

        _parts[2] = _body = new ActorPart(this, AtlasParser.GetBodySprites(textureAtlas))
        {
            Shift = new  Vector2(0, -20f),
        };

        _parts[3] = _head = new ActorPart(this, AtlasParser.GetHeadSprites(textureAtlas))
        {
            Shift = new  Vector2(0, -70f),
        };
    }
    
    public void Update(GameTime gameTime, Vector2 shift)
    {
        if (shift == Vector2.Zero)
            return;
        
        if (shift.Y > 0)
            Direction =  Direction.Down;
        else if (shift.Y < 0)
            Direction =  Direction.Up;
        else if (shift.X > 0)
            Direction =  Direction.Right;
        else if (shift.X < 0)
            Direction =  Direction.Left;
        
        Position += shift;
        
        var dt = gameTime.ElapsedGameTime.TotalSeconds * Options.Speed;
        _t += shift.X > 0 ? dt: -dt; // по или против часовой стрелки
        var pos = new Vector2(Options.StepHeight * (float)Math.Cos(_t), Options.StepWidth * (float)Math.Sin(_t));

        pos.Rotate(shift.ToAngle());
        _legs[0].Position = pos;
        _legs[1].Position = -pos;
        
        _body.Position = new Vector2(0, Options.StepHeight * (float)Math.Sin(_t));
    }

    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        spriteBatch.DrawCircle(Position, 5, 16, Color.Red);
        foreach (var part in _parts)
            part.Draw(spriteBatch);
    }
}