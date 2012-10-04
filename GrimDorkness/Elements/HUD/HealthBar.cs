using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace GrimDorkness
{
    // super jankaphonics!      this should be a HUD Element
    class HealthBar:HUDElement
    {

        Vector2 position;

        Rectangle sourceRect;
        Rectangle destRect;

        public HealthBar(Texture2D texture, int health, int maxHealth)
        {
            position = new Vector2(10.0f, 10.0f);

            sourceRect = new Rectangle(0, 0, texture.Width, texture.Height);

            sprite = new Sprite(texture, sourceRect, 2.0);


        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {


            base.Draw(spriteBatch, gameTime);
        }

        public void DrawHealthTicks(SpriteBatch spriteBatch, int health, int maxHealth)
        {
            int numberOfTicks = (10 * health) / maxHealth;

            for (int currentTick = 0; currentTick < numberOfTicks; currentTick++)
            {
                Vector2 currentTickPosition = new Vector2(10.0f + (10.0f * (float)currentTick), 10);

                sprite.Draw(spriteBatch, currentTickPosition, SpriteEffects.None);

            }
        }


    }
}
