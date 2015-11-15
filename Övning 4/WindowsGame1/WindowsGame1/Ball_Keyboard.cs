using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame1
{
    class Ball_Keyboard:Ball_Bounce
    {
        KeyboardState ks;
        public Ball_Keyboard(Texture2D tex, Vector2 pos, GameWindow window)
            : base(tex, pos, window)
        {
            this.color = Color.Gainsboro;
        }
        public override void Update()
        {
            ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Right) && (pos.X + (tex.Width / 2.0f)) < bX)
            {
                pos += speed;
            }
            if (ks.IsKeyDown(Keys.Left) && pos.X > 0)
            {
                pos -= speed;
            }
            hitBox.X = (int)pos.X;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, null, Color.Green, 0, new Vector2(tex.Width / 2.0f, tex.Height / 2.0f), 1, SpriteEffects.None, 0);
        }

        public override void HandleCollision()
        {
            if (ks.IsKeyDown(Keys.Right) && (pos.X + (tex.Width / 2.0f)) < bX)
            {
                pos -= speed / 2;
            }
            if (ks.IsKeyDown(Keys.Left) && (pos.X - (tex.Width / 2.0f)) > 0)
            {
                pos += speed / 2;
            }
        }
    }
}
