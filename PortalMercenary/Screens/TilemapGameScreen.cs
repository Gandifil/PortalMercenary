using System;
using System.Collections.Generic;
using System.Linq;
using Coroutine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using MonoGame.Extended.Collisions.Layers;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Tilemaps;
using MonoGame.Extended.Tilemaps.Rendering;
using PortalMercenary.Game;
using PortalMercenary.Game.Controllers;
using PortalMercenary.Utils;

namespace PortalMercenary.Screens;

public class TilemapGameScreen: GameScreen
{
    private readonly string _mapName;
    private readonly TilemapSpriteBatchRenderer _renderer;
    private readonly SpriteBatch _spriteBatch;
    public static Character Player { get; private set; }

    public CollisionWorld2D CollisionWorld { get; private set; }
    
    private Tilemap _tilemap;

    public TilemapGameScreen(string mapName) : base(G.Game)
    {
        _mapName = mapName;

        _renderer = new TilemapSpriteBatchRenderer();
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        CollisionWorld = new CollisionWorld2D(new Layer(new SpatialHash(new SizeF(128f, 128f))));
        G.Init2(this);
    }

    public override void LoadContent()
    {
        base.LoadContent();
        
        _tilemap = Content.Load<Tilemap>(_mapName);
        _renderer.LoadTilemap(_tilemap);
        CollisionWorld.AddTilemapCollision(_tilemap);
        
        var spawnPoints = _tilemap.Layers["spawners"] as TilemapObjectLayer ?? throw new Exception("spawners not found");

        var spawner = G.Game.CharacterManager.GetSpawner();
        Player = spawner
            .WithPosition(spawnPoints.Objects[0].Position)
            .WithController(new PlayerController())
            .Spawn("player")
            .Last();
        
        CoroutineHandler.Start(Spawn(spawner, 
            spawnPoints.Objects.Where(x => x.Id != 0).Select(x => x.Position).ToArray()));
    }

    private IEnumerator<Wait> Spawn(CharacterSpawner spawner, Vector2[] positions)
    {
        spawner.WithController(new AiController());

        for (var minutes = 0; minutes < 3; minutes++)
        {
            for (var seconds = 0; seconds < 60; seconds+=10)
            {
                for (var i = 0; i < (minutes+1); i++)
                {
                    spawner.WithPosition(Random.Shared.GetItems(positions, 1)[0]).Spawn("player");
                }
                yield return new Wait(10);
            }
        }
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