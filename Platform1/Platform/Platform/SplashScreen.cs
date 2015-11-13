using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
    
namespace Platform
{
    public class SplashScreen : GameScreen
    {
        SpriteFont font;
        List<FadeAnimation> fade;
        List<Texture2D> images;

        FileManager fileManager;

        int imageNumber;

        public override void LoadContent(ContentManager Content, InputManager inputManager)
        {
            base.LoadContent(Content, inputManager);
            if (font== null)
                font = content.Load<SpriteFont>("Font1");

            imageNumber = 0;
            fileManager = new FileManager();
            fade = new List<FadeAnimation>();
            images = new List<Texture2D>();

            fileManager.LoadContent("Load/Splash.cme", attributes, contents);

            for (int i = 0; i < attributes.Count; i++)
            {
                for (int j = 0; j < attributes[i].Count; j++)
                {
                    switch (attributes[i][j])
                    {
                        case "Image":
                            images.Add(content.Load<Texture2D>(contents[i][j]));
                            fade.Add(new FadeAnimation());
                            break;
                    }
                }
            }
            for (int i = 0; i < fade.Count; i++)
            {
                fade[i].LoadContent(content, images[i], "", new Vector2(80,60));
                fade[i].Scale = 1.25f;
                fade[i].IsActive = true;
            }
        }
        public override void UnloadContent()
        {
                base.UnloadContent();
                fileManager = null;
        }
        public override void Update(GameTime gameTime)
        {
            inputManager.Update();

            fade[imageNumber].Update(gameTime);

            if (fade[imageNumber].Alpha == 0.0f)
                imageNumber++;
            if (imageNumber >= fade.Count - 1 || inputManager.KeyPressed(Keys.Z))
            {
                ScreenManager.Instance.AddScreen(new TitleScreen(), inputManager);
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            fade[imageNumber].Draw(spriteBatch);
        }
    }
}
