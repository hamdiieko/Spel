using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace KingOfTheHill
{
    public enum Gamestate { Start, randomPlacePieces, rePlacePieces1, rePlacePieces2, playP1, playP2, gameover}
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D player1Pieces, player2Pieces, gridTex, startmenu;
        Vector2 position = Vector2.Zero;
        Vector2 offset = new Vector2(50, 50);
        Player[] players;
        int currentPlayer;
        Grid grid;
        MouseState mouseState, oldMouseState;
        KeyboardState keyboardState, oldKeyboardState;
        GamePieces playerPieces1, playerPieces2;
        SpriteFont font;

        public static Texture2D redT;
        

        Gamestate currentGameState = Gamestate.Start;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1280;
        }
        protected override void Initialize()
        {

            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            player1Pieces = Content.Load<Texture2D>("chess_white"); 
            player2Pieces = Content.Load<Texture2D>("chess_black");
            gridTex = Content.Load<Texture2D>("chess_tiles");
            startmenu = Content.Load<Texture2D>("Start");
            font = Content.Load<SpriteFont>("SpriteFont1");
            //redT = Content.Load<Texture2D>("red");

            grid = new Grid(gridTex);

            IsMouseVisible = true;

            players = new Player[2];
            players[0] = new Player(player1Pieces, null);
            players[1] = new Player(player2Pieces, players[0]);

            players[0].SetPiecesVisibilty(true);
            players[1].SetPiecesVisibilty(true);
            currentPlayer = 0;

        }

        protected override void UnloadContent()
        {

        }
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            oldMouseState = mouseState;
            mouseState = Mouse.GetState();
            Point mousePoint = new Point(mouseState.X, mouseState.Y);

            oldKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();

            switch (currentGameState)
            {
                case Gamestate.Start:
                    if (keyboardState.IsKeyDown(Keys.Enter) && oldKeyboardState.IsKeyUp(Keys.Enter))
                    {
                        currentGameState = Gamestate.randomPlacePieces;
                    }
                    break;
                case Gamestate.randomPlacePieces:
                    if (keyboardState.IsKeyDown(Keys.Enter) && !oldKeyboardState.IsKeyDown(Keys.Enter))
                    {
                        currentGameState = Gamestate.rePlacePieces1;
                    }
                    break;
                case Gamestate.rePlacePieces1:
                    HandleInput();
                    currentPlayer = 0;
                    
                    if (mouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released)
                    {
                        players[currentPlayer].UsePieces(mouseState.X, mouseState.Y, players[0], players[1]);
                    }
                    players[currentPlayer].MovePiece(mouseState.X, mouseState.Y);

                    
                    if (keyboardState.IsKeyDown(Keys.Enter) && !oldKeyboardState.IsKeyDown(Keys.Enter))
                    {
                        currentGameState = Gamestate.rePlacePieces2;
                        players[currentPlayer].SetPiecesVisibilty(false);
                    }

                    break;
                case Gamestate.rePlacePieces2:
                    currentPlayer = 1;
                    if (mouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released)
                    {
                        players[currentPlayer].UsePieces(mouseState.X, mouseState.Y, players[1], players[0]);
                    }
                    players[currentPlayer].MovePiece(mouseState.X, mouseState.Y);

                    if (keyboardState.IsKeyDown(Keys.Enter) && !oldKeyboardState.IsKeyDown(Keys.Enter))
                    {
                        currentGameState = Gamestate.playP1;
                        players[currentPlayer].SetPiecesVisibilty(false);
                    }
                    break;
                case Gamestate.playP1:
                    currentPlayer = 0;

                    players[currentPlayer].isPlayerVisible = false;
                    if (mouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released)
                    {
                        if (players[1].HitPiece(mousePoint))
                        {
                            int i = 0;
                            i++;
                        }
                    }
                    

                    if(players[1].GameOver())
                    {
                        this.Exit();
                    }

                    if (keyboardState.IsKeyDown(Keys.Enter) && !oldKeyboardState.IsKeyDown(Keys.Enter))
                    {
                        currentGameState = Gamestate.playP2;
                    }
                    break;
                case Gamestate.playP2:
                    currentPlayer = 1;
                    players[currentPlayer].isPlayerVisible = false;
                    if (mouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released)
                    {
                        players[0].HitPiece(mousePoint);

                    }
                    if (players[0].GameOver())
                    {
                        this.Exit();
                    }

                    if (keyboardState.IsKeyDown(Keys.Enter) && !oldKeyboardState.IsKeyDown(Keys.Enter))
                    {
                        currentGameState = Gamestate.playP1;
                    }
                    break;
                case Gamestate.gameover:
                    break;
                default:
                    break;
            }

            players[currentPlayer].MovePiece(mouseState.X, mouseState.Y);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            grid.Draw(spriteBatch);

            switch (currentGameState)
            {
                case Gamestate.Start:
                    spriteBatch.Draw(startmenu, Vector2.Zero, Color.White);
                    break;
                case Gamestate.randomPlacePieces:

                    spriteBatch.DrawString(font, "Your pieces are being place randomly", new Vector2(400, 50), Color.Black);
                    break;
                case Gamestate.rePlacePieces1:
                    players[0].Draw(spriteBatch);
                    
                    spriteBatch.DrawString(font, "Player 1, this is your chance to rearrange your pieces", new Vector2(400, 50), Color.Black);
                    break;
                case Gamestate.rePlacePieces2:
                    players[1].Draw(spriteBatch);

                    spriteBatch.DrawString(font, "Player 2, this is your chance to rearrange your pieces", new Vector2(400, 50), Color.Black);
                    break;
                case Gamestate.playP1:
                    players[1].Draw(spriteBatch);
                    players[0].DrawAll(spriteBatch);
                    
                    spriteBatch.DrawString(font, "Player 1, Find Player 2:s Pieces", new Vector2(400, 50), Color.Black);
                    break;
                case Gamestate.playP2:
                    players[0].Draw(spriteBatch);
                    players[1].DrawAll(spriteBatch);
                    
                    spriteBatch.DrawString(font, "Player 2, Find Player 1:s Pieces", new Vector2(400, 50), Color.Black);
                    break;
                case Gamestate.gameover:
                    break;
                default:
                    break;
            }

            //for (int i = 0; i < 2; i++)
            //{
            //    players[i].Draw(spriteBatch);
            //}
            

            spriteBatch.End();
            base.Draw(gameTime);
        }
        void HandleInput()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            if (keyboardState.IsKeyUp(Keys.Enter) && oldKeyboardState.IsKeyDown(Keys.Enter))
            {
                ChangePlayer();
            }
        }

        void ChangePlayer()
        {
            if (currentPlayer == 1)
            {
                currentPlayer = 0;
            }
            else
            {
                currentPlayer = 1;
            }
        }
    }
}
