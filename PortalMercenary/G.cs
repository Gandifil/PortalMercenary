using MonoGame.Extended.Tweening;
using PortalMercenary.Screens;

namespace PortalMercenary;

public static class G
{
    public static ContentSchema Content { get; private set; }
    public static Game1 Game { get; private set; }
    public static Tweener Tweener { get; private set; }
    public static TilemapGameScreen Screen { get; private set; }

    public static void Init(Game1 game)
    {
        Game = game;
        Tweener = new ();
        Content = new ContentSchema(game.Content);
    }

    public static void Init2(TilemapGameScreen tilemapGameScreen)
    {
        Screen = tilemapGameScreen;
    }
}