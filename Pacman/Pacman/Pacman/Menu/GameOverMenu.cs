using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman
{
    class GameOverMenu:Menu
    {
        Texture2D menu_sheet;
        SpriteFont font;
        public GameOverMenu(ContentManager content, GraphicsDeviceManager graphics):base(content, graphics)
        {
            //menu_sheet = content.Load<Texture2D>("menu");
            //font = content.Load<SpriteFont>("ButtonFont");
            //for (int i = 0; i < 3; i++)
            //{
            //    Button b = new Button(menu_sheet, new Vector2((menu_width / 2) - (menu_sheet.Width / 2), i * 75 + 125));
            //    buttons.Add(b);
            //}
        }
    }
}
