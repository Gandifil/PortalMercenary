using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Tilemaps;
using MonoGame.Extended.Tilemaps.Rendering;
using PortalMercenary.Game;
using PortalMercenary.Game.Controllers;

namespace PortalMercenary.Screens;

public class TilemapGameScreen: GameScreen
{
    private readonly string _mapName;
    private readonly TilemapSpriteBatchRenderer _renderer;
    private readonly SpriteBatch _spriteBatch;
    public static Character Player { get; private set; }
    
    private Tilemap _tilemap;

    public TilemapGameScreen(string mapName) : base(G.Game)
    {
        _mapName = mapName;

        _renderer = new TilemapSpriteBatchRenderer();
        _spriteBatch = new SpriteBatch(GraphicsDevice);
    }

    public override void LoadContent()
    {
        base.LoadContent();
        
        _tilemap = Content.Load<Tilemap>(_mapName);
        _renderer.LoadTilemap(_tilemap);
        
        var objectLayer = _tilemap.Layers["spawners"] as TilemapObjectLayer ?? throw new Exception("spawners not found");
        
        Player = G.Game.CharacterManager.GetSpawner()
            .WithPosition(objectLayer.Objects[2].Position)
            .WithController(new AiController())
            .Spawn("player")
            .WithPosition(objectLayer.Objects[0].Position)
            .WithController(new PlayerController())
            .Spawn("player")
            .Last();
    }

    public override void UnloadContent()
    {
        base.UnloadContent();
        
        Content.UnloadAsset(_tilemap.Name);
    }

    public override void Update(GameTime gameTime)
    {
        _renderer.Update(gameTime);
        G.Game.Camera.LookAt(Player.Position);
    }

    public override void Draw(GameTime gameTime)
    {
        _renderer.Draw(_spriteBatch, G.Game.Camera);
    }
}