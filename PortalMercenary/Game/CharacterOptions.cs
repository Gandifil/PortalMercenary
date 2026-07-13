namespace PortalMercenary.Game;

public class CharacterOptions
{
    public string Atlas { get; set; }

    public AttackOptions Attack { get; set; }

    public class AttackOptions
    {
        public float ActorAnimationDuration { get; set; }
    }
}