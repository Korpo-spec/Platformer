using System;
using SFML.Graphics;
using System.Collections.Generic;
using SFML.System;
using SFML.Window;
using System.IO;
using System.Text;

namespace Platformer
{
    public class Scene
    {
        private readonly Dictionary<string, Texture> textures;
        private readonly List<Entity> entities;
        private string nextScene;
        private string currentScene;
        public Scene() 
        {
            textures =  new Dictionary<string, Texture>();
            entities = new List<Entity>();
        }
        public void Spawn(Entity entity)
        {
            entities.Add(entity);
            entity.Create(this);
        }
        public Texture LoadTexture(string name)
        {
            if (textures.TryGetValue(name,out Texture found))
            {
                return found;
            }
            string fileName = $"assets/{name}.png";
            Texture texture = new Texture(fileName);
            textures.Add(name, texture);
            return texture;
        }
        public void UpdateAll(float deltaTime)
        {
            HandleSceneChange();
            for (int i = entities.Count - 1; i >= 0; i--)
            {
                Entity entity = entities[i];
                entity.Update(this, deltaTime);

            }
            for (int i = 0; i < entities.Count;)
            {
                Entity entity = entities[i];
                if (entity.Dead) entities.RemoveAt(i);
                else i++;
            }
        }
        public void RenderAll(RenderTarget target)
        {
            for (int i = entities.Count - 1; i >= 0; i--)
            {
                Entity entity = entities[i];
                entity.Render(target);

            }
        }
        public bool TryMove(Entity entity, Vector2f movement)
        {
            entity.Position += movement;
            bool collided = false;
            for (int i = 0; i < entities.Count; i++) 
            {
                Entity other = entities[i];
                if (!other.Solid) continue;
                if (other == entity) continue;
                
                FloatRect boundsA = entity.Bounds;
                FloatRect boundsB = other.Bounds;
                if (Collision.RectangleRectangle(boundsA, boundsB, out Collision.Hit hit))
                {
                    entity.Position += hit.Normal * hit.Overlap;
                    i = -1;
                    collided = true;
                }
            }
            return collided;
        }

        public void Reload()
        {
            nextScene = currentScene;
        }

        public void Load(string sceneName)
        {
            nextScene = sceneName;
        }

        private void HandleSceneChange()
        {
            if(nextScene == null) return;
            entities.Clear();
            

            string file = $"assets/{nextScene}.txt";
            Console.WriteLine($"Loading scene '{file}'");

            foreach (var line in File.ReadLines(file, Encoding.UTF8))
            {
                string parsed = line.Trim();
                int commentAt = parsed.IndexOf('#');
                if (commentAt >= 0)
                {
                    parsed = parsed.Substring(0, commentAt);
                    parsed = parsed.Trim();
                }
                if (parsed.Length == 0)
                {
                    continue;
                }
                string[] words = parsed.Split(" ");
                Vector2f position = new Vector2f(int.Parse(words[1]), int.Parse(words[2]));

                switch (words[0])
                {
                    case "w":
                        Spawn(new Platform{Position = position});
                        break;
                    case "d":
                        Spawn(new Door {
                            Position = position,
                            NextRoom = words[3]
                            });
                        break;
                    case "k":
                        Spawn(new Key {Position = position});
                        break;
                    case "h":
                        Spawn(new Hero{ Position = position});
                        break;
                    case "c":
                        Spawn(new Coin{ Position = position});
                        break;
                }
            }

            currentScene = nextScene;
            nextScene = null;
            Spawn(new Background());
        }

        public bool FindByType<T>(out T found) where T : Entity
        {
            foreach(Entity entity in entities)
            {
                if (entity is T typed)
                {
                    found = typed;
                    return true;
                }
            }
            
            found = default(T);
            return false;
        }
        
    }
}
