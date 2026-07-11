using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Graphics;
using MonoGame.Extended.Tilemaps.Tiled.Document;

namespace PortalMercenary.Entities;

public class Actor
{
    private readonly ActorPart[] _parts, _legs;
    private readonly ActorPart _head, _body, _weapon;

    public Vector2 Position { get; set; } = Vector2.Zero;

    public ActorOptions Options { get; set; } = new();

    public Direction Direction { get; set; }

    private float _t; 
    
    public Actor(Texture2DAtlas textureAtlas)
    {
        _parts = new ActorPart[5];
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

        _parts[4] = _weapon = new ActorPart(this, AtlasParser.GetWeaponSprites(textureAtlas))
        {
            Shift = new  Vector2(0, -20f),
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
        
        var dt = (float)gameTime.ElapsedGameTime.TotalSeconds * Options.Speed;
        _t += shift.X > 0 ? dt: -dt; // по или против часовой стрелки
        var pos = new Vector2(Options.StepHeight * MathF.Cos(_t), Options.StepWidth * MathF.Sin(_t));

        pos.Rotate(shift.ToAngle());
        _legs[0].Position = pos;
        _legs[1].Position = -pos;
        //_head.Depth = -0.4f;
        _body.Position = new Vector2(0, Options.StepHeight * (float)Math.Sin(_t));
        SetWeaponPosition(shift, _t * 0.66f);
    }

    private void SetWeaponPosition(Vector2 shift, float d)
    {
        var pos = new Vector2(20f, 0);
        var radians = shift.ToAngle();
        pos.RotateAround(Vector2.Zero, radians);
        var fluctuating = 10f * new Vector2(3 * MathF.Cos(d), MathF.Abs(MathF.Sin(d)));
        fluctuating.Rotate(radians - MathF.PI / 2);
        _weapon.Position = pos + fluctuating;
        _weapon.Depth = pos.Y / 100f;
        _weapon.Rotation = radians - MathF.PI / 2;
    }

    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        spriteBatch.DrawCircle(Position, 5, 16, Color.Red);
        foreach (var part in _parts.OrderBy(x => x.Depth))
            part.Draw(spriteBatch);
    }
}