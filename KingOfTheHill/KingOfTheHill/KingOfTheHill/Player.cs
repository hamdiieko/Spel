using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KingOfTheHill
{
    class Player
    {
        Texture2D texture;

        public GamePieces[] gamePieces;
        int pickedUpPieces;
        int pieceSize;
        public bool isHit = false;
        int hitPieces;

        public bool isPlayerVisible;
        Random rand = new Random(Guid.NewGuid().GetHashCode());

        public List<Vector2> positionsOccupied = new List<Vector2>();

        const int numberOfPieces = 24;
        public Player(Texture2D pieceTex, Player otherplayer)
        {
            texture = pieceTex;
            pieceSize = texture.Width / 6;
            isPlayerVisible = false;
            CreateGamePieces(otherplayer);
        }
        void CreateGamePieces(Player otherplayer)
        {
            gamePieces = new GamePieces[numberOfPieces];

            for (int i = 0; i < 24; i++)
            {
                gamePieces[i] = new GamePieces(GetRandPos(0, 384, 0, 384, otherplayer), 0, 5, pieceSize);
            }

            pickedUpPieces = -1;
        }

        public bool GameOver()
        {
            for (int i = 0; i < gamePieces.Length; i++)
            {
                if (!gamePieces[i].isHit)
                    return false;
            }
            return true;
        }
   
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < numberOfPieces; i++)
            {
                if(gamePieces[i] != null)
                {
                    if(gamePieces[i].isHit)
                    {
                        gamePieces[i].Draw(texture, spriteBatch);
                    }
                }
                    
            }
        }
        public void DrawAll(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < numberOfPieces; i++)
            {
                if (gamePieces[i] != null)
                {
                    
                    
                 gamePieces[i].Draw(texture, spriteBatch);
                    
                }

            }
        }   


        public void SetPiecesVisibilty(bool b)
        {
            foreach (GamePieces gp in gamePieces)
            {
                gp.isHit = b;
            }
        }

        public void MovePiece(int x, int y)
        {
            if (pickedUpPieces != -1)
            {
                int new_x = (x / pieceSize) * pieceSize;
                int new_y = (y / pieceSize) * pieceSize;

                gamePieces[pickedUpPieces].MovePiece(new_x, new_y);
            }
        }
        
        
        public void UsePieces(int x, int y, Player p1, Player p2)
        {
            if(pickedUpPieces == -1)
            {
                for (int i = 0; i < numberOfPieces; i++)
			   {
			       if (gamePieces[i].PickUpPiece(x,y))
                  {
                       pickedUpPieces = i;
                       int idx = p1.positionsOccupied.FindIndex(0, (Vector2 v) => { return v == gamePieces[i].pos; });
                       p1.positionsOccupied.RemoveAt(idx);
                       break;
                  }
			   }
            }
            else
            {
                bool placefree = true;
                for (int i = 0; i < p1.positionsOccupied.Count; i++)
                {
                    if (p1.positionsOccupied[i] == gamePieces[pickedUpPieces].pos)
                    {
                        placefree = false;
                    }
                }
                for (int i = 0; i < p2.positionsOccupied.Count; i++)
                {
                    if (p2.positionsOccupied[i] == gamePieces[pickedUpPieces].pos)
                    {
                        placefree = false;
                    }
                }
                if (placefree)
                {
                    p1.positionsOccupied.Add(gamePieces[pickedUpPieces].pos);


                    pickedUpPieces = -1;
                }
                
            }
        }

        int GetRandX(int x, int x2)
        {
            return rand.Next(x, x2);
        }
        int GetRandY(int y, int y2)
        {
            return rand.Next(y, y2);
        }


        Vector2 GetRandPos(int x, int x2, int y, int y2, Player otherplayer)
        {
            bool keeprandomnize = true;
            int posX = 0;
            int posY = 0;
            Vector2 pos = Vector2.Zero;
            while (keeprandomnize) {
                keeprandomnize = false;
                int X = GetRandX(x, x2);
                int Y = GetRandY(y, y2);

                posX = (int)(X / 32) * 32;
                posY = (int)(Y / 32) * 32;

                pos = new Vector2(posX, posY);
                for (int i = 0; i < positionsOccupied.Count; i++)
                {
                    if (positionsOccupied[i] == pos)
                    {
                        keeprandomnize = true;
                    }
                }

                if(otherplayer != null)
                {
                    for (int i = 0; i < otherplayer.positionsOccupied.Count; i++)
                    {
                        if (otherplayer.positionsOccupied[i] == pos)
                        {
                            keeprandomnize = true;
                        }
                    }
                }

                
            }
            positionsOccupied.Add(pos);

            return new Vector2(posX, posY);
        }

        public bool IsAllPiecesHit()
        {
            bool allHit = true;
            for (int i = 0; i < numberOfPieces; i++)
            {
                if (isHit == false)
                {
                    allHit = false;
                }
                
            }


            return allHit;
        }
        public bool HitPiece(Point p)
        {
            bool b = false;
            for (int i = 0; i < gamePieces.Length; i++)
            {
                if(gamePieces[i].HitPos(p))
                {
                    b = true;
                }
            }

            return b;

        }
            
    }
}
