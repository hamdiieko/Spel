using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KingOfTheHill
{
    class Tile
    {
        Vector2 position;
        Rectangle rectTexture;

        public Tile(int x, int y, int textureX, int textureY, int tileSize)
        {
            position = new Vector2(x, y);
            rectTexture = new Rectangle(textureX, textureY, tileSize, tileSize);
        }
        

        public void Draw(Texture2D texture, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, rectTexture, Color.White);
        }
    }  
}
