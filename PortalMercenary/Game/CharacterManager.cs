using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace PortalMercenary.Game;

public class CharacterManager: SimpleDrawableGameComponent
{
    private readonly Game1 _game;
    private readonly List<Character> _characters = new();

    public CharacterManager(Game1 game)
    {
        _game = game;
    }

    public Character Spawn(string key)
    {
        var character = new Character(G.Content.Characters[key]);
        _characters.Add(character);
        return character;
    }
    
    public override void Update(GameTime gameTime)
    {
        var dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
        foreach (var character in _characters)
            character.Update(dt);
    }

    public override void Draw(GameTime gameTime)
    {
        foreach (var character in _characters)
            character.Draw(_game.SpriteBatch);
    }
}