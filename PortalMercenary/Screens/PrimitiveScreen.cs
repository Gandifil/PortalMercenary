using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;
using PortalMercenary.Game;
using PortalMercenary.Game.Controllers;

namespace PortalMercenary.Screens;

public class PrimitiveScreen: GameScreen
{
    private Vector2 _backgroundOffset = Vector2.Zero;
    private Texture2D _backgroundPattern;
    public static Character Player { get; private set; }
    
    public new Game1 Game { get; }
    
    private readonly SpriteBatch _spriteBatch;
    
    public PrimitiveScreen(Game1 game) : base(game)
    {
        Game = game;
        _spriteBatch = new SpriteBatch(GraphicsDevice);
    }

    public override void Initialize()
    {
        base.Initialize();
    }

    public override void LoadContent()
    {
        base.LoadContent();
        
        _backgroundPattern = Content.Load<Texture2D>("images/grass_tile");
        Player = Game.CharacterManager.GetSpawner()
            .MovePosition(new Vector2(100, 100))
            .WithController(new DollController())
            .Spawn("player")
            .MovePosition(new Vector2(100, 100))
            .WithController(new AiController())
            .Spawn("player")
            .MovePosition(new Vector2(100, 100))
            .WithController(new PlayerController())
            .Spawn("player")
            .Last();
    }

    public override void UnloadContent()
    {
        Content.UnloadAsset(_backgroundPattern.Name);
        
        base.UnloadContent();
    }

    public override void Update(GameTime gameTime)
    {
    }

    public override void Draw(GameTime gameTime)
    {
        _spriteBatch.Begin(samplerState: SamplerState.PointWrap);   
        _spriteBatch.Draw(_backgroundPattern, GraphicsDevice.PresentationParameters.Bounds, new Rectangle(_backgroundOffset.ToPoint(), GraphicsDevice.PresentationParameters.Bounds.Size), Color.White);
        _spriteBatch.End();
    }
}