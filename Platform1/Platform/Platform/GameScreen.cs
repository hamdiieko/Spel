﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Platform
{
    public class GameScreen
    {
        protected ContentManager content;
        protected List<List<string>> attributes, contents;
        protected InputManager inputManager;
        public virtual void Initialize()
        {

        }
        public virtual void LoadContent(ContentManager Content, InputManager inputManager)
        {
            content = new ContentManager(Content.ServiceProvider, "Content");
            attributes = new List<List<string>>();
            contents = new List<List<string>>();
            this.inputManager = inputManager;
        }
        public virtual void UnloadContent()
        {
            content.Unload();
            inputManager = null;
            attributes.Clear();
            contents.Clear();   
        }
        public virtual void Update(GameTime gameTime)
        {

        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
