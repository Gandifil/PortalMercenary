namespace PortalMercenary.Entities.Animations;

public abstract class ActorAnimation
{
    public required float MaxTime { get; init; }

    public bool IsFinished { get; private set; }

    protected float _dt;

    public abstract void Start(ActorBody actor);
    
    public virtual void Update(float dt)
    {
        _dt += dt;

        if (_dt >= MaxTime)
        {
            IsFinished = true;
            Finish();
        }
    }
    
    public abstract void Finish();
}