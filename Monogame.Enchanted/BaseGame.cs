using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monogame.Enchanted.Debug;

namespace Monogame.Enchanted;

public class BaseGame: Game
{
    public GraphicsDeviceManager Graphics { get; }
    
    public SpriteBatch SpriteBatch  { get; private set; }
    
    public BaseGame(string title, int width, int height, bool fullScreen)
    {
        Graphics = new GraphicsDeviceManager(this);
        Graphics.PreferredBackBufferWidth = width;
        Graphics.PreferredBackBufferHeight = height;
        Graphics.IsFullScreen = fullScreen;
        Graphics.ApplyChanges();
        
        Content.RootDirectory = "Content";
        Window.Title = title;
        Window.AllowUserResizing = true;
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        SpriteBatch = new SpriteBatch(GraphicsDevice);
        
        AddDebug();
        base.Initialize();
    }

    [Conditional("DEBUG")]
    private void AddDebug()
    {
        Components.Add(new DebugGameComponent(this));
    }
}