using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Monogame.Enchanted;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using MonoGame.Extended.Collisions.Layers;
using PortalMercenary.Animation;
using PortalMercenary.Game;
using PortalMercenary.Screens;

namespace PortalMercenary;

public class Game1 : BaseGame
{
    public CharacterManager CharacterManager { get; private set; }

    public DecalsComponent DecalsComponent { get; private set; }

    public CollisionWorld2D CollisionWorld { get; private set; }
    
    public TempAnimatedSpriteComponent Animations { get; private set; }

    public Game1(): base("Викинги: Кровь на Траве", 800, 450, false) { }

    protected override void Initialize()
    {
        G.Init(this);
        Components.Add(CharacterManager = new CharacterManager(this));
        Components.Add(DecalsComponent = new DecalsComponent(100));
        Components.Add(Animations = new TempAnimatedSpriteComponent(this));
        CollisionWorld = new CollisionWorld2D(new Layer(new SpatialHash(new SizeF(128f, 128f))));
        base.Initialize();
        ScreenManager.ShowScreen(new PrimitiveScreen(this));
    }

    protected override void Update(GameTime gameTime)
    {
        var keys = Keyboard.GetState();
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            keys.IsKeyDown(Keys.Escape))
            Exit();

        G.Tweener.Update(gameTime.GetElapsedSeconds());
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        //GraphicsDevice.Clear(new Color(32, 40, 78, 255));

        //SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
        base.Draw(gameTime);
        //SpriteBatch.End();
    }
}