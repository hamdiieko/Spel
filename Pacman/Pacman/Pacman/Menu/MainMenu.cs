﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Pacman
{
    class MainMenu : Menu
    {
        Vector2 start_pos, site_pos, exit_pos;
        bool start_game;
        bool show_website;
        bool exit_game;

        public MainMenu(ContentManager content, GraphicsDeviceManager graphics)
            : base(content, graphics)
        {
            int menu_width = 300;
            int menu_height = 400;
            for (int i = 0; i < 3; i++)
            {
                Button b = new Button(menu_sheet, new Vector2((menu_width / 2) - (menu_sheet.Width / 2), i * 75 + 125));
                buttons.Add(b);
            }
        }

        public bool StartGame()
        {
            return start_game;
        }
        public bool ExitGame()
        {
            return exit_game;
        }

        private void TextPositions()
        {
            start_pos = new Vector2(buttons[0].position.X + (menu_sheet.Width / 2) - (int)font.MeasureString("Start").Length() / 2, buttons[0].position.Y + 15);
            site_pos = new Vector2(buttons[1].position.X + (menu_sheet.Width / 2) - (int)font.MeasureString("Website").Length() / 2, buttons[1].position.Y + 15);
            exit_pos = new Vector2(buttons[2].position.X + (menu_sheet.Width / 2) - (int)font.MeasureString("Exit").Length() / 2, buttons[2].position.Y + 15);
        }

        public override void Update(GameTime gameTime)
        {
            TextPositions();

            if (buttons[0].pressed == true)
                start_game = true;
            else if (buttons[1].pressed == true)
                show_website = true;
            else if (buttons[2].pressed == true)
                exit_game = true;

            if (show_website == true)
            {
                Process.Start("www.kodacreations.se"); //"firefox.exe", 
                show_website = false;
            }
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.DrawString(font, "Start", start_pos, new Color(255, 222, 0));
            spriteBatch.DrawString(font, "Website", site_pos, new Color(255, 222, 0));
            spriteBatch.DrawString(font, "Exit", exit_pos, new Color(255, 222, 0));
        }
    }
}
