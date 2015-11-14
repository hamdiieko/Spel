using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame1
{
    class Ball_Bounce:Ball
    {
        public int bX;
        public int bY;
        public Vector2 speed;
        public Color color;
        
        public Ball_Bounce(Texture2D tex, Vector2 pos, GameWindow window) :base(tex,pos)
        {
            this.bX = window.ClientBounds.Width;
            this.bY = window.ClientBounds.Height;
            this.speed = new Vector2(2, 0);
        }

        public override void Update()
        {
            pos += speed;

            if (pos.X <= 0 && speed.X < 0 || pos.X + tex.Width >= bX && speed.X > 0)
                speed.X *= -1;
            if (pos.Y <= 0 && speed.Y < 0 || pos.Y + tex.Height >= bY && speed.Y > 0)
                speed.Y *= -1;

            
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos,null,Color.Blue,0,new Vector2(tex.Width/2.0f,tex.Height/2.0f),1,SpriteEffects.None,0);
        }
        public override void HandleCollision()
        {
            pos -= speed / 2;
              
        }
    }
}
