using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Graphics;

namespace PortalMercenary;

public class Game1 : Game
{
    private Texture2DAtlas _atlas;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    // The destination rectangle for the background pattern to fill.
    private Rectangle _backgroundDestination;

    // The offset to apply when drawing the background pattern so it appears to
    // be scrolling.
    private Vector2 _backgroundOffset;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
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

        var cardsTexture = Content.Load<Texture2D>("images/Tilemap_color3");
        _atlas = Texture2DAtlas.Create("images/Tilemap_color3", cardsTexture, 62, 64);
        // TODO: use this.Content to load your game content here
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
        _spriteBatch.Begin(samplerState: SamplerState.PointWrap);

        _spriteBatch.Draw(_atlas[15], new Rectangle(_backgroundOffset.ToPoint(), _backgroundDestination.Size), Color.White);

        _spriteBatch.End();


        base.Draw(gameTime);
    }
}