using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
namespace WindowsGame1
{
    abstract class Ball
    {
        protected Vector2 pos;
        protected Texture2D tex;
        public float radius;
        public Rectangle hitBox;
        public Ball(Texture2D tex, Vector2 pos,float radius,Rectangle hitBox)
        {
            this.tex = tex;
            this.pos = pos;
            this.radius = tex.Width/2 ;
            this.hitBox = new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height);

        }
        public virtual bool CircleCollision(Ball other)
        {
            return Vector2.Distance(pos, other.pos) < (radius + other.radius);
        }

        public abstract void Update();


        public abstract void Draw(SpriteBatch spriteBatch);

        public abstract void HandleCollision();

        public bool PixelCollition(Ball other)
        {
            Color[] dataA = new Color[tex.Width * tex.Height];
            tex.GetData(dataA);
            Color[] dataB = new Color[other.tex.Width * other.tex.Height];
            other.tex.GetData(dataB);

            int top = Math.Max(hitBox.Top, other.hitBox.Top);
            int bottom = Math.Min(hitBox.Bottom, other.hitBox.Bottom);
            int left = Math.Max(hitBox.Left, other.hitBox.Left);
            int right = Math.Min(hitBox.Right, other.hitBox.Right);

            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    Color colorA = dataA[(x - hitBox.Left) + (y - hitBox.Top) * hitBox.Width];
                    Color colorB = dataB[(x - other.hitBox.Left) + (y - other.hitBox.Top) * other.hitBox.Width];
                    if (colorA.A != 0 && colorB.A != 0)
                    {
                        return true;
                    }
                }
            }
            return false;   

        }
        
    }
}
    