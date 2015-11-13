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

namespace Pacman
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        List<GameField> game = new List<GameField>();
        //GameField game_field;
        MainMenu main_menu;
        public static Random rnd;

        enum GameState { Menu, Game, Won, Lost };
        GameState currentGameState = GameState.Menu;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        { base.Initialize(); }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);            
            main_menu = new MainMenu(this.Content, this.graphics);
            rnd = new Random();
        }

        protected override void UnloadContent()
        { }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            switch (currentGameState)
            {
                case GameState.Menu:
                    IsMouseVisible = true;
                    main_menu.Update(gameTime);
                    game.Clear();
                    if (main_menu.StartGame() == true)
                    {
                        GameField g = new GameField(this.Content, this.graphics, this.Window);
                        game.Add(g);
                        currentGameState = GameState.Game;
                    }
                        
                    else if (main_menu.ExitGame() == true)
                        this.Exit();
                    break;
                case GameState.Game:
                    game[0].Update(gameTime);
                    break;
                case GameState.Won:
                    break;
                case GameState.Lost:
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(5, 10, 15));
            spriteBatch.Begin();

            switch (currentGameState)
            {
                case GameState.Menu:
                    main_menu.Draw(spriteBatch);
                    break;
                case GameState.Game:
                    game[0].Draw(spriteBatch);
                    break;
                case GameState.Won:
                    game[0].DrawWon(spriteBatch);
                    break;
                case GameState.Lost:
                    game[0].DrawLost(spriteBatch);
                    break;
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
