﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Platform
{
    public class TitleScreen:GameScreen
    {
        SpriteFont font;
        MenuManager menu;

        public override void LoadContent (ContentManager Content, InputManager inputManager)
        {
            base.LoadContent(Content, inputManager);
            if (font == null)
                font = content.Load<SpriteFont>("font1");
            menu = new MenuManager();
            menu.LoadContent(content, "Title");
        }
        public override void UnloadContent()
        {
            base.UnloadContent();
            menu.UnloadContent();
        }
        public override void Update(GameTime gameTime)
        {
            inputManager.Update();
            menu.Update(gameTime);
            if (inputManager.KeyPressed(Keys.Z))
                ScreenManager.Instance.AddScreen(new SplashScreen(), inputManager);
                 
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            menu.Draw(spriteBatch);
        }
    }
}
