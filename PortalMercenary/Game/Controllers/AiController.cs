using MonoGame.Extended;

namespace PortalMercenary.Game.Controllers;

public class AiController: ICharacterController
{
    public void Update(Character character, float dt)
    {
        var player = G.Game.Player;
        character.Actor.Shift = (player.Position - character.Position).NormalizedCopy();
    }
}