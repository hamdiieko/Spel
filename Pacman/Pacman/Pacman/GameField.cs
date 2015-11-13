using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Pacman
{
    class GameField
    {
        SpriteFont font;
        Texture2D character_sheet;
        Texture2D world_sheet;
        Vector2 pacman_startpos;
        int score = 0;
        double start_timer = 4;
        public static int tile_X = 0, tile_Y = 0;
        GraphicsDeviceManager graphics;
        GameWindow window;
        bool game_won, game_lost;

        Pacman pacman;
        List<Character> ghosts = new List<Character>();
        List<Food> food = new List<Food>();
        List<string> level_data = new List<string>();
        Tile[,] level;


        public GameField(ContentManager content, GraphicsDeviceManager graphics, GameWindow window)
        {
            this.graphics = graphics;
            this.window = window;
            character_sheet = content.Load<Texture2D>("pacman2");
            world_sheet = content.Load<Texture2D>("world");
            font = content.Load<SpriteFont>("InterfaceFont");
            CreateLevel();
        }


        private void CreateLevel()
        {
            StreamReader file = new StreamReader("level.txt");
            while (!file.EndOfStream)
                level_data.Add(file.ReadLine());
            file.Close();

            for (int i = 0; i < level_data.Count; i++)
                tile_Y += 1;
            for (int j = 0; j < level_data[1].Length; j++)
                tile_X += 1;

            level = new Tile[tile_Y, tile_X];

            for (int i = 0; i < level_data.Count; i++)
            {
                for (int j = 0; j < level_data[i].Length; j++)
                {
                    if (level_data[i][j] == 'x')
                    {
                        level[i, j] = new Tile(world_sheet, new Vector2(j * 16, i * 16), new Rectangle(0, 0, 16, 16));
                    }
                    else if (level_data[i][j] == 'f')
                    {
                        level[i, j] = new Tile(world_sheet, new Vector2(j * 16, i * 16), new Rectangle(16, 0, 16, 16));
                        Food f = new Food(world_sheet, new Vector2(j * 16, i * 16), new Rectangle(0, 16, 16, 16)); food.Add(f);
                    }
                    else if (level_data[i][j] == 'r' || level_data[i][j] == 'm' || level_data[i][j] == 'b' || level_data[i][j] == 'o')
                    {
                        level[i, j] = new Tile(world_sheet, new Vector2(j * 16, i * 16), new Rectangle(16, 0, 16, 16));
                        Food f = new Food(world_sheet, new Vector2(j * 16, i * 16), new Rectangle(0, 16, 16, 16)); food.Add(f);
                        Character g = new Ghost(character_sheet, new Vector2(j * 16, i * 16), level_data); ghosts.Add(g);
                    }
                    else if (level_data[i][j] == 'p')
                    {
                        level[i, j] = new Tile(world_sheet, new Vector2(j * 16, i * 16), new Rectangle(16, 0, 16, 16));
                        Food f = new Food(world_sheet, new Vector2(j * 16, i * 16), new Rectangle(0, 16, 16, 16)); food.Add(f);
                        pacman_startpos = new Vector2(j * 16, i * 16);
                        pacman = new Pacman(character_sheet, new Vector2(j * 16, i * 16), level_data, pacman_startpos);
                    }
                    else if (level_data[i][j] == 'e')
                    {
                        level[i, j] = new Tile(world_sheet, new Vector2(j * 16, i * 16), new Rectangle(16, 16, 16, 16));
                    }
                }
            }
        }

        private void SetResolution()
        {
            graphics.PreferredBackBufferWidth = tile_X * 16;
            graphics.PreferredBackBufferHeight = tile_Y * 16;
            graphics.ApplyChanges();
        }

        public bool GameWon()
        {
            return game_won;
        }
        public bool GameLost()
        {
            return game_lost;
        }


        public void Update(GameTime gameTime)
        {      
            SetResolution();
            start_timer -= gameTime.ElapsedGameTime.TotalSeconds;
            if (start_timer <= 0)
            {
                if (food.Any() && pacman.Health() != 0)
                {
                    pacman.Update(gameTime);
                    foreach (Character g in ghosts)
                    {
                        g.Update(gameTime);
                        if (pacman.hitbox.Intersects(g.hitbox))
                            pacman.HitByGhost = true;
                    }
                    for (int i = 0; i < food.Count; i++)
                    {
                        food[i].Update(gameTime);
                        if (pacman.hitbox.Intersects(food[i].hitbox))
                        {
                            food.RemoveAt(i);
                            score += 1;
                        }
                    }
                }
                else if (!food.Any() && pacman.Health() != 0)
                {

                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < level.GetLength(0); i++)
                for (int j = 0; j < level.GetLength(1); j++)
                    level[i, j].Draw(spriteBatch);

            foreach (Food f in food)
                f.Draw(spriteBatch);

            pacman.Draw(spriteBatch);

            foreach (Character g in ghosts)
                g.Draw(spriteBatch);

            if (start_timer > 0)
            {
                spriteBatch.DrawString(font, "Game starts in " + (int)start_timer + " seconds", new Vector2(5, 5), Color.White);
            }
            else
            {
                spriteBatch.DrawString(font, "Score: " + score, new Vector2(5, 5), Color.White);
                spriteBatch.DrawString(font, "Lives: " + pacman.Health(), new Vector2(tile_X * 16 - (int)font.MeasureString("Lives: " + pacman.Health()).Length() - 5, 5), Color.White);
            }

        }

        public void DrawWon(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "You won!", new Vector2(window.ClientBounds.Width / 2 - (int)font.MeasureString("You won!").Length() / 2, 5), Color.White);
            spriteBatch.DrawString(font, "Score:"+score, new Vector2(window.ClientBounds.Width / 2 - (int)font.MeasureString("Score:"+score).Length() / 2, 25), Color.White);
        }

        public void DrawLost(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "You lost!", new Vector2(window.ClientBounds.Width / 2 - (int)font.MeasureString("You lost!").Length() / 2, 5), Color.White);
            spriteBatch.DrawString(font, "Score:" + score, new Vector2(window.ClientBounds.Width / 2 - (int)font.MeasureString("Score:" + score).Length() / 2, 25), Color.White);
        }
    }
}