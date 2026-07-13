using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Monogame.Enchanted.Content;
using MonoGame.Extended.Graphics;
using MonoGame.Extended.Serialization.Json;
using PortalMercenary.Animation;
using PortalMercenary.Game;

namespace PortalMercenary;

public class ContentSchema(ContentManager contentManager)
{
    public readonly ContentLoaderFolder<CharacterOptions> Characters = 
        new(contentManager, "characters/", ".json", new JsonContentLoader());
    
    public readonly Folder<FreeTexturePackerReader.SpriteSheet> FreeTexPackerSpritesheets = new(contentManager, "images/");
    
    public readonly Folder<SoundEffect> Sounds = new(contentManager, "sounds/");
    
    public readonly ContentLoaderFolder<SpriteSheet> Spritesheets = 
        new(contentManager, "images/", ".json", new SpriteSheetContentLoader());
}