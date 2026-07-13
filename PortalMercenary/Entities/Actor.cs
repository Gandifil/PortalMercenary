using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Graphics;
using PortalMercenary.Entities.Animations;

namespace PortalMercenary.Entities;

public class Actor
{
    private readonly ActorBody _body;

    public Vector2 Position { get; set; } = Vector2.Zero;

    public ActorOptions Options { get; set; } = new();

    public Direction Direction { get; private set; }
    public Vector2 Shift { get; set; }
    //public Vector2 ControlShift { get; set; } = Vector2.Zero;

    private float _t; 
    
    public Actor(Texture2DAtlas textureAtlas)
    {
        _body = new ActorBody(this, textureAtlas);
    }
    
    public void Update(float dt)
    {
        if (_animation?.IsFinished ?? false)
            _animation = null;
        
        if (_animation is not null)
        {
            _animation.Update(dt);
            return;
        }
        
        if (Shift == Vector2.Zero)
            return;

        if (Shift.Y > 0)
            Direction =  Direction.Down;
        else if (Shift.Y < 0)
            Direction =  Direction.Up;
        else if (Shift.X > 0)
            Direction =  Direction.Right;
        else if (Shift.X < 0)
            Direction =  Direction.Left;
        
        Position += Shift * 100 * dt;
        
        var t = dt * Options.Speed;
        _t += Shift.X > 0 ? t: -t; // по или против часовой стрелки
        var pos = new Vector2(Options.StepHeight * MathF.Cos(_t), Options.StepWidth * MathF.Sin(_t));

        pos.Rotate(Shift.ToAngle());
        _body.Legs[0].Position = pos;
        _body.Legs[1].Position = -pos;
        _body.Body.Position = new Vector2(0, Options.StepHeight * (float)Math.Sin(_t));
        SetWeaponPosition(_t * 0.66f);
    }

    private void SetWeaponPosition(float d)
    {
        var pos = new Vector2(20f, 0);
        var radians = Shift.ToAngle();
        pos.RotateAround(Vector2.Zero, radians);
        var fluctuating = 10f * new Vector2(3 * MathF.Cos(d), MathF.Abs(MathF.Sin(d)));
        fluctuating.Rotate(radians - MathF.PI / 2);
        _body.Weapon.Position = pos + fluctuating;
        _body.Weapon.Depth = pos.Y / 100f;
        _body.Weapon.Rotation = radians - MathF.PI / 2;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.DrawCircle(Position, 5, 16, Color.Red);
        foreach (var part in _body.Parts.OrderBy(x => x.Depth))
            part.Draw(spriteBatch);
    }
    
    private ActorAnimation _animation;

    public bool IsFree => _animation is null;

    public void Start(AttackAnimation attackAnimation)
    {
        if (IsFree)
        {
            _animation = attackAnimation;
            _animation.Start(_body);
        }
    }
}