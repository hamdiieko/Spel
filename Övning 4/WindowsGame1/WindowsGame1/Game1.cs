using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame1
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        List<Ball> gameObjects = new List<Ball>();
        Texture2D tex;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            

            base.Initialize();
        }
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            tex = Content.Load<Texture2D>("ball");

            
            Ball b = new Ball_Mouse(tex);
            gameObjects.Add(b);
            b = new Ball_Bounce(tex, new Vector2(200, Window.ClientBounds.Height - 30), Window);
            gameObjects.Add(b);
            b = new Ball_Keyboard(tex, new Vector2(50, Window.ClientBounds.Height - 30), Window);
            gameObjects.Add(b);
        }
        protected override void UnloadContent()
        {
            
        }
        protected override void Update(GameTime gameTime)
        {
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            foreach (Ball g in gameObjects)
            {
                g.Update();
            }
            foreach (Ball ball in gameObjects)
            {
                if(!(ball is Ball_Mouse))
                    foreach (Ball other in gameObjects)
                    {
                        if(!(other is Ball_Mouse)&& ball!= other)
                            if (ball.CircleCollision(other))
                            {
                                while (ball.CircleCollision(other))
                                {
                                    ball.HandleCollision();
                                    other.HandleCollision();
                                }
                            }   
                    }
            }
            

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            foreach (Ball g in gameObjects)
            {
                g.Draw(spriteBatch);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
