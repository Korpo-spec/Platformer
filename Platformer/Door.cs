using System;
using SFML.Window;
using SFML.System;
using SFML.Graphics;

namespace Platformer
{
    public class Door : Entity
    {
        public Door() : base("tileset")
        {
            sprite.TextureRect = new IntRect(180, 103, 18, 23);
            sprite.Origin = new Vector2f(9, 11);
        }
    }
}
