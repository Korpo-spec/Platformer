using System;
using SFML.Window;
using SFML.System;
using SFML.Graphics;

namespace Platformer
{
    public class Key : Entity
    {
        public Key() : base("tileset")
        {
            sprite.TextureRect = new IntRect(126, 18, 18, 18);
            sprite.Origin = new Vector2f(9, 9);
        }
        public override void Update(Scene scene, float deltaTime)
        {
            if (scene.FindByType<Hero>(out Hero hero))
            {
                if (Collision.RectangleRectangle(Bounds, hero.Bounds, out _))
                {
                    if (scene.FindByType<Door>(out Door door))
                    {
                        door.Unlocked = true;
                        Dead = true;
                        
                    }
                }
            }
        }
    }
}
