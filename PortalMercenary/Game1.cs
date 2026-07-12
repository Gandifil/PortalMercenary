using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PortalMercenary.Game;

namespace PortalMercenary;

public class Game1 : Microsoft.Xna.Framework.Game
{
    private GraphicsDeviceManager _graphics;
    public SpriteBatch SpriteBatch  { get; private set; }
    public CharacterManager CharacterManager { get; private set; }
    
    // The offset to apply when drawing the background pattern so it appears to
    // be scrolling.
    private Vector2 _backgroundOffset = Vector2.Zero;
    private Texture2D _backgroundPattern;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        _graphics.PreferredBackBufferWidth = 800;
        _graphics.PreferredBackBufferHeight = 450;
        _graphics.ApplyChanges();
        Content.RootDirectory = "Content";
        Window.Title = "Portal Mercenary";
        Window.AllowUserResizing = true;
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // Create a new graphics device manager.
        SpriteBatch = new SpriteBatch(GraphicsDevice);

        Components.Add(CharacterManager = new CharacterManager(this));
        G.Init(this);
        base.Initialize();
    }
    
    protected override void LoadContent()
    { 
        _backgroundPattern = Content.Load<Texture2D>("images/grass_tile");
        CharacterManager.Spawn("player");
    }

    protected override void Update(GameTime gameTime)
    {
        var keys = Keyboard.GetState();
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            keys.IsKeyDown(Keys.Escape))
            Exit();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(new Color(32, 40, 78, 255));

        SpriteBatch.Begin(samplerState: SamplerState.PointWrap);   
        SpriteBatch.Draw(_backgroundPattern, GraphicsDevice.PresentationParameters.Bounds, new Rectangle(_backgroundOffset.ToPoint(), GraphicsDevice.PresentationParameters.Bounds.Size), Color.White);
        SpriteBatch.End();
        
        SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
        base.Draw(gameTime);
        SpriteBatch.End();
    }
}