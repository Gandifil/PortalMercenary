namespace PortalMercenary;

public static class G
{
    public static ContentSchema Content { get; private set; }

    public static void Init(Game1 game1)
    {
        Content = new ContentSchema(game1.Content);
    }
}