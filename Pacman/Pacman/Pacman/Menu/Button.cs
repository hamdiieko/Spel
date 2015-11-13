using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman
{
    class Button:Object
    {
        MouseState mouse_state, old_mouse_state;
        Point mouse_position;
        public bool pressed;

        public Button(Texture2D texture, Vector2 position):base(texture, position)
        {
            this.texture = texture;
            this.position = position;
        }

        public override void Update(GameTime gameTime)
        {
            old_mouse_state = mouse_state;
            mouse_state = Mouse.GetState();
            mouse_position.X = mouse_state.X;
            mouse_position.Y = mouse_state.Y;

            hitbox = new Rectangle((int)position.X, (int)position.Y, 150, 50);

            if (hitbox.Contains(mouse_position) && mouse_state.LeftButton == ButtonState.Pressed && old_mouse_state.LeftButton == ButtonState.Released)
                pressed = true;
            else
                pressed = false;

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (hitbox.Contains(mouse_position))
                spriteBatch.Draw(texture, position, new Rectangle(0, 50, 150, 50), Color.White);
            else
                spriteBatch.Draw(texture, position, new Rectangle(0, 0, 150, 50), Color.White);

            base.Draw(spriteBatch);
        }
    }
}
