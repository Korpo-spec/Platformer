using System;
using SFML.Window;
using SFML.System;
using SFML.Graphics;


namespace Platformer
{
    class Program
    {
        public const int windowH = 600;
        public const int windowW = 800;
        static void Main(string[] args)
        {
            using (var window = new RenderWindow(new VideoMode(windowW, windowH), "Platformer")) 
            {
                window.Closed += (o, e) => window.Close();


                Scene scene = new Scene();
                scene.Load("level0");
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
