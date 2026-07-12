using Microsoft.Xna.Framework.Content;
using Monogame.Enchanted.Content;
using PortalMercenary.Game;

namespace PortalMercenary;

public class ContentSchema(ContentManager contentManager)
{
    public readonly JsonFolder<CharacterOptions> Characters = new(contentManager, "characters/");
}