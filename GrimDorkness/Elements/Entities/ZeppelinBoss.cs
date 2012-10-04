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
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace GrimDorkness
{
    class ZeppelinBoss:Entity
    {
        Rectangle frameUnharmed;
        Rectangle frameHarmed;

        const int ZEPPELIN_POINT_VALUE = 50;
        const int ZEPPELIN_HEALTH = 500;


        Game game;

        // pixel offsets for each gun:
        Vector2 gun1Offset, gun2Offset, gun3Offset, gun4Offset;

        double fireCooldownRemaining = 0;
        double fireDelay = 0.4;
        
        // used for AI in phase 1:
        double longCooldownRemaining = 10.0;
        double gunsHotTime = 10.0;
        double gunsCoolTime = 5.0;

        Boolean gunsHot = true;

        Boolean pointsTallied = false;

        Boolean moveLeft = true;

        public ZeppelinBoss(Texture2D texture, Vector2 initialPosition, Game thisGame)
        {
            game = thisGame;

            health = ZEPPELIN_HEALTH;
            maxHealth = health;
            damage = 10;

            team = Team.Enemy;

            // set up our initial position:
            xPos = (int)initialPosition.X;
            yPos = (int)initialPosition.Y;

            frameUnharmed = new Rectangle(0, 11, 97, 131);
            frameHarmed = new Rectangle(97, 11, 95, 131);

            gun1Offset = new Vector2(10.0f, 130.0f);
            gun2Offset = new Vector2(180.0f, 130.0f);
            gun3Offset = new Vector2(90.0f, 240.0f);
            gun4Offset = new Vector2(130.0f, 240.0f);

            sprite = new Sprite(texture, frameUnharmed, 2.0);

            


        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            sprite.Draw(spriteBatch, new Vector2(xPos, yPos), SpriteEffects.None);

            base.Draw(spriteBatch, gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            // what phase is the boss in?


            // entering
            if (yPos < 10)
            {
                yPos += 2;
            }
            // between 50% and 100%
            else if (health > maxHealth / 2)
            {
                if (xPos < 400)
                {
                    xPos += 1;
                }

                    longCooldownRemaining -= gameTime.ElapsedGameTime.TotalSeconds;
                    if (longCooldownRemaining <= 0.0f)
                    {
                        if(gunsHot)
                        {
                        gunsHot = false;
                        longCooldownRemaining = gunsCoolTime;
                        }
                        else
                        {
                            gunsHot = true;
                            longCooldownRemaining = gunsHotTime;
                        }
                    }
                    

                

                if (fireCooldownRemaining <= 0.0f && gunsHot)
                {
                    Vector2 bulletPosition = new Vector2((float)xPos + gun1Offset.X, (float)yPos + gun1Offset.Y);
                    game.AddNewBullet(BulletType.FireBall, Direction.South, Team.Enemy, bulletPosition);

                    bulletPosition = new Vector2((float)xPos + gun2Offset.X, (float)yPos + gun2Offset.Y);
                    game.AddNewBullet(BulletType.FireBall, Direction.South, Team.Enemy, bulletPosition);


                    fireCooldownRemaining = fireDelay;

                }
                else
                {
                    fireCooldownRemaining -= gameTime.ElapsedGameTime.TotalSeconds;
                    if (fireCooldownRemaining < 0.0f) fireCooldownRemaining = 0.0f;

                }

                

            }
            // boss health is between 0% and 50%
            else
            {
                sprite.UpdateRect(frameHarmed);

/*                if (moveLeft)
                {
                    xPos -= 1;
                    if (xPos <= 0) moveLeft = false;
                }
                else
                {
                    xPos += 1;
                    if (xPos >= 800 - 95) moveLeft = true;
                }
*/

                if (fireCooldownRemaining <= 0.0f)
                {
                    Vector2 bulletPosition = new Vector2((float)xPos + gun1Offset.X, (float)yPos + gun1Offset.Y);
                    game.AddNewBullet(BulletType.FireBall, Direction.South, Team.Enemy, bulletPosition);

                    bulletPosition = new Vector2((float)xPos + gun2Offset.X, (float)yPos + gun2Offset.Y);
                    game.AddNewBullet(BulletType.FireBall, Direction.South, Team.Enemy, bulletPosition);

                    bulletPosition = new Vector2((float)xPos + gun3Offset.X, (float)yPos + gun3Offset.Y);
                    game.AddNewBullet(BulletType.FireBall, Direction.South, Team.Enemy, bulletPosition);

                    bulletPosition = new Vector2((float)xPos + gun4Offset.X, (float)yPos + gun4Offset.Y);
                    game.AddNewBullet(BulletType.FireBall, Direction.South, Team.Enemy, bulletPosition);

                    fireCooldownRemaining = fireDelay;

                }
                else
                {
                    fireCooldownRemaining -= gameTime.ElapsedGameTime.TotalSeconds;
                    if (fireCooldownRemaining < 0.0f) fireCooldownRemaining = 0.0f;

                }



            }



            if (moveLeft)
            {
                xPos -= 1;
                if (xPos <= 0) moveLeft = false;
            }
            else
            {
                xPos += 1;
                if (xPos >= 800 - 95) moveLeft = true;
            }



            base.Update(gameTime);
        }


        public int GetHealth()
        {
            return health;
        }



        public int GetMaxHealth()
        {
            return maxHealth;
        }

        public override bool TakeDamage(int damage)
        {
            if (health == 0 && pointsTallied == false)
            {
                pointsTallied = true;
                game.addPoints(ZEPPELIN_POINT_VALUE);
            }


            return base.TakeDamage(damage);
        }

    }
}
