using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using PortalMercenary.Game.Controllers;

namespace PortalMercenary.Game;

public class CharacterManager: SimpleDrawableGameComponent
{
    private readonly Game1 _game;
    private readonly List<Character> _characters = new();
    private readonly SpriteBatch _spriteBatch;

    public CharacterManager(Game1 game)
    {
        _game = game;
        _spriteBatch = new SpriteBatch(game.GraphicsDevice);
    }

    private Effect _cutEffect;
    protected override void LoadContent()
    {
        base.LoadContent();

        _cutEffect = G.Content.Effects["cutEffect"];
    }

    public CharacterSpawner GetSpawner() => new (this);
    

    public Character Add(Character character)
    {
        _characters.Add(character);
        G.Game.CollisionWorld.Insert(character);
        return character;
    }
    
    public override void Update(GameTime gameTime)
    {
        var dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
        foreach (var character in _characters)
            character.Update(dt);
        
        G.Game.CollisionWorld.RebuildDynamicLayers();
    }

    public override void Draw(GameTime gameTime)
    {
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp, effect: _cutEffect);
        foreach (var character in _characters)
            character.Draw(_spriteBatch);
        _spriteBatch.End();
    }
}