using System;
using SFML.Window;
using SFML.System;
using SFML.Graphics;


namespace Platformer
{
    public class Hero : Entity
    {
        
        public const float WalkSpeed = 100f;
        public const float JumpForce = 250f;
        public const float GravityForce = 400f;
        
        private float verticalSpeed;
        private bool isGrounded;
        private bool isUpPressed;
        private bool faceRight = false;
        private Text gui;
        public Hero() : base("characters")
        {
            sprite.TextureRect = new IntRect(0, 0, 24, 24);
            sprite.Origin = new Vector2f(12, 12);
            gui = new Text();
            gui.CharacterSize = 24;
            gui.Font = new Font("assets/future.ttf");
            gui.FillColor = Color.Black;
        }
        
        public override FloatRect Bounds 
        {
            get 
            {
                var bounds = base.Bounds;
                bounds.Left += 3;
                bounds.Width -= 6;
                bounds.Top += 3;
                bounds.Height -= 3;
                return bounds;
            }
        }
        
        private void UpdateAnimation(float deltaTime)
        {
            if (time > 1/10f)
            {
                firstFrame = !firstFrame;
                time = 0;

            }
            if (firstFrame)
            {
                sprite.TextureRect = new IntRect(0,0,24,24);
            }
            else
            {
                sprite.TextureRect = new IntRect(24,0,24,24);
            }
            time += deltaTime;
        }

        private float time = 0;
        private bool firstFrame;

        public override void Update(Scene scene, float deltaTime)
        {
            if(Keyboard.IsKeyPressed(Keyboard.Key.Left))
            {
                scene.TryMove(this, new Vector2f(-WalkSpeed * deltaTime, 0));
                faceRight = false;
                UpdateAnimation(deltaTime);
            }
            if(Keyboard.IsKeyPressed(Keyboard.Key.Right))
            {
                scene.TryMove(this, new Vector2f(WalkSpeed * deltaTime, 0));
                faceRight =  true;
                UpdateAnimation(deltaTime);
            }
            if(Keyboard.IsKeyPressed(Keyboard.Key.Up))
            {
                if(isGrounded && !isUpPressed)
                {
                    verticalSpeed = -JumpForce;
                    isUpPressed = true;
                }
                else
                {
                    isUpPressed = false;
                }
                
                    
            }
            verticalSpeed += GravityForce * deltaTime;
            if(verticalSpeed > 500f) verticalSpeed = 500;
            
            isGrounded = false;
            Vector2f velocity = new Vector2f(0,verticalSpeed*deltaTime);
            if(scene.TryMove(this,velocity))
            {
                if( verticalSpeed > 0f)
                {
                    isGrounded = true;
                    verticalSpeed = 0f;
                }
                else
                {
                    verticalSpeed = -0.5f * verticalSpeed;
                }
            }
            
            if(Position.Y > Program.windowH ||
               Position.Y < 0 ||
               Position.X > Program.windowW ||
               Position.X < 0) scene.Reload();
            
        }

        public override void Render(RenderTarget target)
        {
            sprite.Scale = new Vector2f(faceRight ? -1 : 1, 1);
            base.Render(target);
            gui.DisplayedString = $"Coins: {Coins}";
            gui.Position = new Vector2f(25,20);
            target.Draw(gui);
            
            
        }
        

        private int Coins = 0;
        
        
        public void AddCoin()
        {
            Coins++;
        }
    }
}
