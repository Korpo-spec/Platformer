using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;


namespace Platformer
{
    public class Entity
    {
        private string textureName;
        protected Sprite sprite;
        public bool Dead;
        protected Entity(string textureName)
        {

        }
        public Vector2f Position
        {
            get => sprite.Position;
            set => sprite.Position = value;
        }
        public readonly FloatRect Bounds;
        
        public void Create(Scene scene)
        {

        }
        public void Update(Scene scene, float deltaTime)
        {

        }
        public void Render(RenderTarget target)
        {
            
        }


    }
    
}
