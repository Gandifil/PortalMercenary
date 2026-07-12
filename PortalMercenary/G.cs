namespace PortalMercenary;

public static class G
{
    public static ContentSchema Content { get; private set; }
    public static Game1 Game { get; private set; }

    public static void Init(Game1 game)
    {
        Game = game;
        Content = new ContentSchema(game.Content);
    }
}