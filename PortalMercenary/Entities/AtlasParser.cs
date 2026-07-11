using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Graphics;

namespace PortalMercenary.Entities;

public static class AtlasParser
{
    public static Sprite[] GetBootSprites(Texture2DAtlas atlas)
    {
        var regions = atlas.Where(x => x.Name.Contains("leg", StringComparison.OrdinalIgnoreCase)).ToArray();
        var sprites = new Sprite[Enum.GetNames<Direction>().Length];
        // sprites[0] = new Sprite(atlas.GetRegion("boot_left_01"))
        // {
        //     OriginNormalized = new Vector2(0.5f, 0.5f)
        // };
        // sprites[1] = new Sprite(atlas.GetRegion("boot_right_back_01"))
        // {
        //     OriginNormalized = new Vector2(0.5f, 0.5f)
        // };
        // sprites[2] = new Sprite(atlas.GetRegion("boot_profile_01"))
        // {
        //     OriginNormalized = new Vector2(0.5f, 0.5f),
        //     Effect = SpriteEffects.FlipHorizontally,
        // };
        // sprites[3] = new Sprite(atlas.GetRegion("boot_profile_01"))
        // {
        //     OriginNormalized = new Vector2(0.5f, 0.5f)
        // };
        
        foreach (var direction in Enum.GetValues<Direction>())
        {
            var key = direction switch
            {
                Direction.Up => "back",
                Direction.Down => "front",
                Direction.Left => "R View",
                Direction.Right => "R View",
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
            sprites[(int)direction] = new Sprite(regions.First(x => x.Name.Contains(key, StringComparison.OrdinalIgnoreCase)))
            {
                OriginNormalized = new Vector2(0.5f, 0.5f),
            };
            if (direction == Direction.Left)
                sprites[(int)direction].Effect |= SpriteEffects.FlipHorizontally;
        }
        return sprites;
    }

    public static Sprite[] GetHeadSprites(Texture2DAtlas atlas)
    {
        var regions = atlas.Where(x => x.Name.Contains("head", StringComparison.OrdinalIgnoreCase)).ToArray();
        var sprites = new Sprite[Enum.GetNames<Direction>().Length];
        foreach (var direction in Enum.GetValues<Direction>())
        {
            var key = direction switch
            {
                Direction.Up => "back",
                Direction.Down => "front",
                Direction.Left => "R View",
                Direction.Right => "R View",
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
            sprites[(int)direction] = new Sprite(regions.First(x => x.Name.Contains(key, StringComparison.OrdinalIgnoreCase)))
            {
                OriginNormalized = new Vector2(0.5f, 0.5f),
            };
            if (direction == Direction.Left)
                sprites[(int)direction].Effect |= SpriteEffects.FlipHorizontally;
        }
        
        return sprites;
    }

    public static Sprite[] GetBodySprites(Texture2DAtlas atlas)
    {
        var regions = atlas.Where(x => x.Name.Contains("body", StringComparison.OrdinalIgnoreCase)).ToArray();
        var sprites = new Sprite[Enum.GetNames<Direction>().Length];
        foreach (var direction in Enum.GetValues<Direction>())
        {
            var key = direction switch
            {
                Direction.Up => "back",
                Direction.Down => "front",
                Direction.Left => "R View",
                Direction.Right => "R View",
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
            sprites[(int)direction] = new Sprite(regions.First(x => x.Name.Contains(key, StringComparison.OrdinalIgnoreCase)))
            {
                OriginNormalized = new Vector2(0.5f, 0.5f),
            };
            if (direction == Direction.Left)
                sprites[(int)direction].Effect |= SpriteEffects.FlipHorizontally;
        }
        
        return sprites;
    }
}