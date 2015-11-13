using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KingOfTheHill
{
    class Grid
    {
        Texture2D gridTex;
        int tileSize;
        Tile[,] grid;
        const int nrOfRows = 12;
        const int nrOfColumns = 12;

        public Grid(Texture2D tileTex)
        {
            gridTex = tileTex;
            tileSize = gridTex.Width / 2;

            CreateGrid();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < nrOfRows; i++)
            {
                for (int j = 0; j < nrOfColumns; j++)
                {
                    grid[i, j].Draw(gridTex, spriteBatch);
                }
            }

        }

        public void CreateGrid()
        {
            grid = new Tile[nrOfRows, nrOfColumns];

            for (int i = 0; i < nrOfRows; i++)
            {
                for (int j = 0; j < nrOfColumns; j++)
                {
                    if ((i + j) % 2 == 0)
                    {
                        grid[i, j] = new Tile(i * tileSize, j * tileSize, 0, 0, tileSize);
                    }
                    else
                    {
                        grid[i, j] = new Tile(i * tileSize, j * tileSize, tileSize, 0, tileSize);
                    }

                }
            }
        }
    }
}
