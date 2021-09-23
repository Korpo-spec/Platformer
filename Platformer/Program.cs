using System;
using SFML.Window;
using SFML.System;
using SFML.Graphics;


namespace Platformer
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var window = new RenderWindow(new VideoMode(800, 600), "Platformer")) 
            {
                window.Closed += (o, e) => window.Close();
                Scene scene = new Scene();
                scene.Spawn(new Platform{Position = new Vector2f(54, 270)});
                Clock clock = new Clock();
                while (window.IsOpen)
                {
                    window.DispatchEvents();

                    float deltaTime = clock.Restart().AsSeconds();
                    scene.UpdateAll(deltaTime);
                    window.Clear();
                    // TODO:  Drawing
                    scene.RenderAll(window);
                    window.Display();
                }
            }
        }
    }
}
