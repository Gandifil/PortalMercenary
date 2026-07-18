using MonoGame.Extended;
using PortalMercenary.Screens;

namespace PortalMercenary.Game.Controllers;

public class AiController: ICharacterController
{
    public void Update(Character character, float dt)
    {
        var player = PrimitiveScreen.Player;
        character.Actor.Shift = (player.Position - character.Position).NormalizedCopy();
    }
}