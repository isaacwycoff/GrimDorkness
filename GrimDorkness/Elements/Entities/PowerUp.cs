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
    enum PowerUpType
    {
        shield = 0,
        repair = 1,
        invulnerability = 2,
        missile = 3,
        railgun = 4
    };

    /// <summary>
    /// Floating power-ups dropped by enemies
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    class PowerUp : Entity
    {
        // PowerUp specific constants:
        const double WAVE_THETA_INCREMENT = 0.06;        // used to determine the speed of the back & forth wave motion
        const double WAVE_RADIUS = 30.0;                 // radius of back & forth wave motion in pixels
        // WAVE_RADIUS 5 means the powerup will move back and forth 10 pixels
        const float POWERUP_DROP_SPEED = 1.0f;          // how quickly do the powerups drop off the screen in pixels?

        PowerUpType powerUpType;

        float xOrigin, yOrigin;

        double waveTheta;       // theta used for calculating current position in wave



        /// <summary>
        /// Constructor
        /// </summary>
        public PowerUp(Texture2D texture, PowerUpType type, Vector2 initialPosition)
        {
            xOrigin = initialPosition.X;
            yOrigin = initialPosition.Y;

            xPos = (int)initialPosition.X;
            yPos = (int)initialPosition.Y;

            health = 1;
            maxHealth = 1;

            waveTheta = 0.0;

            team = Team.PowerUp;

            powerUpType = type;

            Rectangle src_rect;

            switch (type)
            {
                case PowerUpType.invulnerability:
                case PowerUpType.missile:
                case PowerUpType.railgun:
                case PowerUpType.repair:
                case PowerUpType.shield:
                default:
                    {
                        src_rect = new Rectangle(72, 2, 24, 25);

                        break;
                    }
            }

            sprite = new Sprite(texture, src_rect, 2.0);




        }   // end constructor

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            sprite.Draw(spriteBatch, new Vector2(xPos, yPos), SpriteEffects.None);

            base.Draw(spriteBatch, gameTime);
        }

        public override void Update(GameTime gameTime)
        {

            waveTheta += WAVE_THETA_INCREMENT;

            if (waveTheta >= (Math.PI * 2)) waveTheta -= Math.PI * 2;

            yOrigin += POWERUP_DROP_SPEED;

            xPos = (int)(xOrigin + (WAVE_RADIUS * Math.Cos(waveTheta)));

            yPos = (int)yOrigin;

//            yPos += (int)POWERUP_DROP_SPEED;


            base.Update(gameTime);
        }


    }


}
