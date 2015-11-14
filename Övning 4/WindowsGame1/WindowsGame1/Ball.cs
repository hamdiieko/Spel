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
        protected Rectangle hitBox;
        public Ball(Texture2D tex, Vector2 pos,float radius,Rectangle hitBox)
        {
            this.tex = tex;
            this.pos = pos;
            this.radius = radius;
            this.hitBox = new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height);

        }
        public virtual bool CircleCollision(Ball other)
        {
            return Vector2.Distance(pos, other.pos) < (radius + other.radius);
        }

        public abstract void Update();


        public abstract void Draw(SpriteBatch spriteBatch);

        public abstract void HandleCollision();
        
    }
}
    