using System;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Graphics;

namespace PortalMercenary.Graphics;

public class CuttingSprite: Sprite
{
    public bool IsСut {get; private set; }
    
    public CuttingSprite(Sprite source) : base(source)
    {
        Color = Color.Black; // отображает спрайт полностью
    }

    public enum Direction
    {
        Up, // 1
        Down, // 0
    }

    public void Cut(Direction direction)
    {
        U = (TextureRegion.LeftUV + TextureRegion.RightUV) / 2;
        V = (TextureRegion.TopUV + TextureRegion.BottomUV) / 2;
        //var randomAngle = ( - .5f) * MathF.PI;
        Angle = Random.Shared.NextSingle();//(MathF.Cos(randomAngle) + 1f) / 2;
        Mode = direction == Direction.Up ? 1 : 0;
        Color = new Color(U, V, Angle, Mode);
        IsСut = true;
    }
    
    private float U;
        
    private float V;
        
    private float Angle;

    private float Mode;
}