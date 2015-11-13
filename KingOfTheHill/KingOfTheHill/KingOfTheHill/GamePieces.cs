using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KingOfTheHill
{
    class GamePieces
    {
        public Vector2 pos;
        int pieceSize;
        Rectangle textureRect;
        private bool GridContainment;
        public bool isHit = true;
        public int hitCounter = 0;
        Point mouse;
        
        public GamePieces(Vector2 position, int textureRows, int textureColumns, int tileSize)
        {
            pos = position;
            pieceSize = tileSize;
            textureRect = new Rectangle(textureColumns * pieceSize, textureRows * pieceSize, pieceSize, pieceSize);

        }
        public void Draw(Texture2D texture, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, pos, textureRect, Color.White);
            //spriteBatch.Draw(Game1.redT, Hitbox(), Color.White);
            if (isHit)
            {
                spriteBatch.Draw(texture, textureRect, Color.White);
            }
        }
        public void MovePiece(int x, int y)
        {
            pos.X = x;
            pos.Y = y;
        }

        private bool GridContaintment(int x, int y)
        {
            //if (x < gridBox.X)
            //    return true;
            //if (y < gridBox.Y)
            //    return true;
            //if (x + textureRect.Width > gridBox.Right)
            //    return true;
            //if (y + textureRect.Height > gridBox.Bottom)
            //    return true;

            //return false;

            //if (pos.X < 0)
            //    pos.X = 0;
            //if (pos.Y < 0)
            //    pos.Y = 0;
            //if (pos.X > 360 - pieceSize)
            //    pos.X = 360 - pieceSize;
            //if (pos.Y > 360 - pieceSize)
            //    pos.Y = 360 - pieceSize;
            if (x < arrayBox.X)
                return true;
            if (mousePos.Y < arrayBox.Y)
                return true;
            if (mousePos.X + shipBox.Width > arrayBox.Right)
                return true;
            if (mousePos.Y + shipBox.Height > arrayBox.Bottom)
                return true;
            return true;

        }

        public bool PickUpPiece(int x, int y)
        {
            return HitPiece(x, y);

        }

        private bool HitPiece(int x, int y)
        {
            Point p = new Point(x, y);
            if (Hitbox().Contains(p))
            {
                return true;
            }
            return false;
        }

        public Rectangle Hitbox()
        {
            return new Rectangle((int)pos.X, (int)pos.Y, pieceSize, pieceSize);
        }

        public bool HitPos(Point p)
        {
            if (HitPiece(p.X, p.Y))
            {
                hitCounter++;
                if (hitCounter == 1)
                {
                    isHit = true;
                }
            }
            return isHit;

        }
        
      

        //public HitPieces(Point mouse)
        //{
        //    if(mouse.contains(textureRect))
        //    {
        //        isHit=true;
        //    }

        

    }
}
