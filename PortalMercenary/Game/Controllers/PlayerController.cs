using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using PortalMercenary.Entities.Animations;

namespace PortalMercenary.Game.Controllers;

public class PlayerController
{
    private readonly Character _character;

    public PlayerController(Character character)
    {
        _character = character;
    }

    public Vector2 Update(float dt)
    {
        var keys = Keyboard.GetState();
        
        var move = Vector2.Zero;
        if (keys.IsKeyDown(Keys.W) || keys.IsKeyDown(Keys.Up))    move.Y -= 1;
        if (keys.IsKeyDown(Keys.S) || keys.IsKeyDown(Keys.Down))   move.Y += 1;
        if (keys.IsKeyDown(Keys.A) || keys.IsKeyDown(Keys.Left))   move.X -= 1;
        if (keys.IsKeyDown(Keys.D) || keys.IsKeyDown(Keys.Right))  move.X += 1;

        if (keys.IsKeyDown(Keys.Space))
            _character.Attack();

        if (move != Vector2.Zero)
        {
            move = move.NormalizedCopy() * 100 * dt;
        }
        return move;
    }
}