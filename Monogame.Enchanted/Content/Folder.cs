using MonoGame.Extended.Serialization.Json;
using MonoGame.Extended.Content;

namespace Monogame.Enchanted.Content;

public class Folder<T>(ContentManager contentManager, string path)
{
    public T Load(string key) => contentManager.Load<T>(path + key);
}

public class JsonFolder<T>(ContentManager contentManager, string path)
{
    public T Load(string key) => contentManager.Load<T>(path + key + ".json", new JsonContentLoader());
}