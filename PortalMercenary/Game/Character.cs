using Microsoft.Xna.Framework.Graphics;
using PortalMercenary.Entities;
using PortalMercenary.Entities.Animations;
using PortalMercenary.Extensions;
using PortalMercenary.Game.Controllers;

namespace PortalMercenary.Game;

public class Character
{
    public Actor Actor { get; set; }

    public PlayerController CharacterController { get; set; }
    
    public Character(CharacterOptions options)
    {
        var atlas = G.Content.Spritesheets[options.Atlas].ToTexture2DAtlas();
        Actor = new Actor(atlas);
        CharacterController  = new PlayerController(this);
    }

    public void Update(float dt)
    {
        Actor.Update(dt, CharacterController.Update(dt));
    }

    public void Draw(SpriteBatch gameSpriteBatch)
    {
        Actor.Draw(gameSpriteBatch);
    }

    public void Attack()
    {
        Actor.Start(new AttackAnimation()
        {
            MaxTime = .3f,
        });
    }
}