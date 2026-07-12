using System;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace PortalMercenary.Entities.Animations;

public class AttackAnimation: ActorAnimation
{
    private ActorBody _actorBody;
    private ActorPart _weapon;
    private Vector2 _backupPosition;
    private float _backupRotation;

    private const float Range = MathF.PI / 3;
    private const float Radius = 50f;
    
    public override void Start(ActorBody actorBody)
    {
        _actorBody = actorBody;
        _weapon = actorBody.Weapon;
        _backupPosition = _weapon.Position;
        _backupRotation = _weapon.Rotation;
    }

    public override void Update(float dt)
    {
        base.Update(dt);

        var currentAngle = MathHelper.Lerp(-Range, Range, _dt / MaxTime);
        var pos = Radius * new Vector2(0, -1);
        pos.Rotate(_actorBody.Actor.Shift.ToAngle() + currentAngle);
        _weapon.Position = pos;
        _weapon.Rotation = pos.ToAngle() - MathF.PI / 2;
    }

    public override void Finish()
    {
        _weapon.Position = _backupPosition;
        _weapon.Rotation = _backupRotation;
    }
}