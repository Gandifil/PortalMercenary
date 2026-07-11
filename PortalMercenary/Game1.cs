using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Graphics;
using MonoGame.Extended.Particles.Modifiers.Interpolators;
using PortalMercenary.Entities;

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
    private Texture2DAtlas _ironSet;
    private Texture2D _atlasTexture2D;
    
    private Actor _player;

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

        _ironSet = Content.Load<Texture2DAtlas>("images/iron_set");
        _backgroundPattern = Content.Load<Texture2D>("images/grass_tile");

        _player = new Actor(_ironSet);
    }

    protected override void Update(GameTime gameTime)
    {
        var keys = Keyboard.GetState();
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            keys.IsKeyDown(Keys.Escape))
            Exit();
        
        var move = Vector2.Zero;
        if (keys.IsKeyDown(Keys.W) || keys.IsKeyDown(Keys.Up))    move.Y -= 1;
        if (keys.IsKeyDown(Keys.S) || keys.IsKeyDown(Keys.Down))   move.Y += 1;
        if (keys.IsKeyDown(Keys.A) || keys.IsKeyDown(Keys.Left))   move.X -= 1;
        if (keys.IsKeyDown(Keys.D) || keys.IsKeyDown(Keys.Right))  move.X += 1;

        if (move != Vector2.Zero)
        {
            move = move.NormalizedCopy() * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        _player.Update(gameTime, move);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(new Color(32, 40, 78, 255));

        _spriteBatch.Begin(samplerState: SamplerState.PointWrap);
        _spriteBatch.Draw(_backgroundPattern, GraphicsDevice.PresentationParameters.Bounds, new Rectangle(_backgroundOffset.ToPoint(), GraphicsDevice.PresentationParameters.Bounds.Size), Color.White);
        _spriteBatch.End();
        
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        _player.Draw(_spriteBatch, gameTime);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}