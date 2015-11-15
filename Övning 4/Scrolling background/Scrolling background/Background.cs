using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Scrolling_background
{
    class Background
    {
        List<Vector2> foreground, middleground, background;
        int fgSpacing, mgSpacing, bgSpacing;    
        float fgSpeed,mgSpeed,bgSpeed;
        Texture2D[] tex;
        GameWindow window;

        public Background(ContentManager Content, GameWindow window)
        {
            this.tex = new Texture2D[3];
            this.window = window;

            tex[0] = Content.Load<Texture2D>("Ground");
            tex[1] = Content.Load<Texture2D>("Cloud");
            tex[2] = Content.Load<Texture2D>("Cloud");
            
            foreground = new List<Vector2>();
            fgSpacing = tex[0].Width;
            fgSpeed = 0.75f;
            for (int i = 0; i < (window.ClientBounds.Width/fgSpacing)+2; i++)
            {
                foreground.Add(new Vector2(i * fgSpacing, window.ClientBounds.Height - tex[0].Height));
            }

            middleground = new List<Vector2>();
            mgSpacing = window.ClientBounds.Width / 5;
            mgSpeed = 0.5f;
            for (int i = 0; i < (window.ClientBounds.Width/mgSpacing); i++)
            {
                middleground.Add(new Vector2(i * mgSpacing, window.ClientBounds.Height - tex[0].Height - tex[1].Height));
            }

            background = new List<Vector2>();
            bgSpacing = window.ClientBounds.Width / 3;
            bgSpeed = 0.25f;
            for (int i = 0; i < (window.ClientBounds.Width/bgSpacing)+2; i++)
            {
                background.Add(new Vector2(i*bgSpacing,window.ClientBounds.Height - tex[0].Height -(int)(tex[1].Height*1.5)));
            }
        }

        public void Update()
        {

        }
        public void Draw(SpriteBatch sb)
        {
            foreach (Vector2 v in background)
            {
                sb.Draw(tex[2], v, Color.White);
            }
            foreach (Vector2 v in middleground)
            {
                sb.Draw(tex[1], v, Color.White);
            }
            foreach (Vector2 v in foreground)
            {
                sb.Draw(tex[0], v, Color.White);
            }
        }
    }
}
