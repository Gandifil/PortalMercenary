using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Graphics;
using MonoGame.Extended.Particles.Modifiers.Interpolators;

namespace PortalMercenary;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    // The destination rectangle for the background pattern to fill.
    private Rectangle _backgroundDestination;

    // The offset to apply when drawing the background pattern so it appears to
    // be scrolling.
    private Vector2 _backgroundOffset;
    private Texture2D _backgroundPattern;
    private Texture2DAtlas _atlas;
    private Texture2D _atlasTexture2D;

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

        // Initialize the offset of the background pattern at zero.
        _backgroundOffset = Vector2.Zero;

        // Set the background pattern destination rectangle to fill the entire
        // screen background.
        _backgroundDestination = GraphicsDevice.PresentationParameters.Bounds;

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _atlasTexture2D = Content.Load<Texture2D>("images/Tilemap_color3");
        _atlas = Texture2DAtlas.Create("images/Tilemap_color3", _atlasTexture2D, 64, 64);

        // Load the background pattern texture.
        _backgroundPattern = Content.Load<Texture2D>("images/grass_tile");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(new Color(32, 40, 78, 255));

        _spriteBatch.Begin(samplerState: SamplerState.PointWrap);
        _spriteBatch.Draw(_backgroundPattern, GraphicsDevice.PresentationParameters.Bounds, new Rectangle(_backgroundOffset.ToPoint(), GraphicsDevice.PresentationParameters.Bounds.Size), Color.White);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}