using MonoGame.Extended.Serialization.Json;
using MonoGame.Extended.Content;

namespace Monogame.Enchanted.Content;

public class Folder<T>(ContentManager contentManager, string path)
{
    public T Load(string key) => contentManager.Load<T>(path + key);

    public T this[string key] => Load(key);
}

public class JsonFolder<T>(ContentManager contentManager, string path)
{
    private readonly IContentLoader _contentLoader = new JsonContentLoader();
    
    public T Load(string key) => contentManager.Load<T>(path + key + ".json", _contentLoader);

    public T this[string key] => Load(key);
}