using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;


namespace WindowsGame1
{
    class Ball_Mouse:Ball
    {
        public Ball_Mouse(Texture2D tex)
            : base(tex, new Vector2(),new float(radius),new Rectangle=hitBox)// varfor funkar inte detta måste kalla på fler argument från konstruktorn i Ball klassen men de går inte, Helpz
        {

        }

        public override void Update()
        {
            pos.X = Mouse.GetState().X;
            pos.Y = Mouse.GetState().Y;
            
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, Color.Chartreuse);
        }

        public override void HandleCollision()
        {
            throw new NotImplementedException();
        }

    }
}
