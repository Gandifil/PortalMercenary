using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace Monogame.Enchanted.Debug;

public static class DebugHighlight
{
    [Conditional("DEBUG")]
    public static void DrawCircle(SpriteBatch spriteBatch, Vector2 center, float r)
    {
        spriteBatch.DrawCircle(center, r, 20, Color.White);
    }
}