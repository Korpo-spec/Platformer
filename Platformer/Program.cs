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
                scene.Spawn(new Door{Position = new Vector2f(100, 100)});
                scene.Spawn(new Key{ Position =  new Vector2f(50, 100)});
                scene.Spawn(new Hero{Position = new Vector2f(70, 264)});
                
                for (int i = 0; i < 20; i++)
                {
                    scene.Spawn(new Platform { Position = new Vector2f(18+ i * 18, 288)});
                }
                scene.Spawn(new Background());
                window.SetView( new View( new Vector2f(200, 150), new Vector2f(400, 300)));
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
