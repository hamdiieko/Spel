using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Pacman
{
    class Menu
    {
        protected Texture2D menu_sheet;
        protected SpriteFont font;
        protected List<Button> buttons = new List<Button>();
        protected int menu_width = 300;
        protected int menu_height = 400;
        GraphicsDeviceManager graphics;

        public Menu(ContentManager content, GraphicsDeviceManager graphics)
        {
            this.graphics = graphics;
            menu_sheet = content.Load<Texture2D>("menu");
            font = content.Load<SpriteFont>("ButtonFont");
        }

        private void SetResolution()
        {
            graphics.PreferredBackBufferWidth = menu_width;
            graphics.PreferredBackBufferHeight = menu_height;
            graphics.ApplyChanges();
        }

        public virtual void Update(GameTime gameTime)
        {
            SetResolution();
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].Update(gameTime);
            }
        }



        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(menu_sheet, new Vector2((menu_width / 2) - (menu_sheet.Width / 2), 35), new Rectangle(0, 100, 150, 50), Color.White);
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].Draw(spriteBatch);
            }
        }
    }
}
