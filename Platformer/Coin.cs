using System;
using SFML.Window;
using SFML.System;
using SFML.Graphics;


namespace Platformer
{
    public class Coin : Entity
    {

        public Coin() : base("tileset")
        {
            sprite.TextureRect = new IntRect(200, 127, 16, 16);
            sprite.Origin = new Vector2f (8,8);
        }

        public override void Update(Scene scene, float deltaTime)
        {
            
            if (scene.FindByType<Hero>(out Hero hero))
            {
                if (Collision.RectangleRectangle(Bounds, hero.Bounds, out _))
                {
                    Dead = true;
                }
            }
            
        }

    }
}
