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
    public enum BulletType
    {
        FireBall = 1,
        Photon = 2
    }


/*    animDie = new List<Rectangle>
                        {
                            new Rectangle(0, 0, 24, 27),
                            new Rectangle(24, 0, 24, 27),
                            new Rectangle(48, 0, 24, 27),
                            new Rectangle(72, 0, 24, 27),
                            new Rectangle(96, 0, 24, 27),
                            new Rectangle(0, 27, 24, 27),
                            new Rectangle(24, 27, 24, 27),
                            new Rectangle(48, 27, 24, 27),
                            new Rectangle(72, 27, 24, 27),
                            new Rectangle(96, 27, 24, 27),
                            new Rectangle(120, 27, 24, 27),
                            new Rectangle(144, 27, 24, 27)
                        }; */

    class Bullet:Entity
    {
        BulletType type;
        Direction direction;


        public Bullet(Texture2D texture, BulletType newBulletType, Direction newDirection, Team newTeam, Vector2 initialPosition)
        {

            type = newBulletType;
            switch (type)
            {
                case BulletType.FireBall:
                    {
                        

                        break;
                    }
                case BulletType.Photon:
                    {

                        break;
                    }



                default:
                    {
                        break;
                    }
            }

            health = 1;
            maxHealth = 1;

            direction = newDirection;


            Rectangle sourceRect = new Rectangle(183, 74, 4, 4);


            sprite = new Sprite(texture, sourceRect, 2.0);

            speed = 7;

            team = newTeam;

            xPos = (int)initialPosition.X;
            yPos = (int)initialPosition.Y;


        }

        public override void Update(GameTime gameTime)
        {
            if (direction == Direction.North)
            {
                yPos -= speed;
            }
            if (direction == Direction.South)
            {
                yPos += speed;
            }

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
          //  sprite.updateRect(animFly[(int)currentFrame]);
            sprite.Draw(spriteBatch, new Vector2(xPos, yPos), SpriteEffects.None);

            base.Draw(spriteBatch, gameTime);
        }
    }
}
