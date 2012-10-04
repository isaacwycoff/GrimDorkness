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
    public enum ShipType
    {
        Grey = 0,
        Brown = 1,
        Green = 2,
        Needle = 3
    }

    public enum AIType
    {
        Basic = 0,
        ZigZag = 1,
        AngleLeft = 2,
        AngleRight = 3
    }

    public enum EnemyShipAnim
    {
        FlyLeft = 0,
        FlyStraight = 1,
        FlyRight = 2
    }

    public enum EnemyAnimFrame
    {
        Left = 0,
        Straight = 1,
        Right = 2
    }

    class EnemyShip:Entity
    {
        List<Rectangle> animFly;

        // define what type of ship/plane this is:
        ShipType shipType;

        // define how this ahip/plane behaves:
        AIType aiType;

        Game game;

        EnemyShipAnim currentAnim = EnemyShipAnim.FlyStraight;

        EnemyAnimFrame currentFrame = EnemyAnimFrame.Straight;

        Vector2 gunBarrelOffset;

        double fireCooldownRemaining = 0;
        double fireDelay = 0.2;                 // TODO: might want to set this in the constructor

        double timeTillChangeDirection = 0;     // TODO: add zig-zag AI

        bool pointsTallied = false;

        // constructor:
        public EnemyShip(Texture2D texture, ShipType newShipType, AIType newAIType, Vector2 initialPosition, Game thisGame)
        {

            game = thisGame;

            // set up our initial position:
            xPos = (int)initialPosition.X;
            yPos = (int)initialPosition.Y;

            team = Team.Enemy;

            shipType = newShipType;             // what type of ship?
            aiType = newAIType;                 // what type of behavior?


            gunBarrelOffset = new Vector2(20.0f, 40.0f);

            // set up graphics and stats based on ship type:
            switch (newShipType)
            {
                case ShipType.Grey:
                    {
                        animFly = new List<Rectangle>
                        {
                            new Rectangle(95, 0, 25, 27),
                            new Rectangle(120, 0, 25, 27),
                            new Rectangle(145, 0, 25, 27)
                        };

                        health = 5;
                        maxHealth = health;
                        damage = 10;
                        speed = 3;
                        state = 0x00;
                        break;
                    }
                case ShipType.Brown:
                    {
                        animFly = new List<Rectangle>
                        {
                            new Rectangle(96, 28, 25, 27),      // it was showing a dark line on the left with x=95
                            new Rectangle(120, 28, 25, 27),
                            new Rectangle(145, 28, 25, 27)
                        };

                        health = 5;
                        maxHealth = health;
                        damage = 10;
                        speed = 3;
                        state = 0x00;
                        break;
                    }
                case ShipType.Green:
                    {
                        animFly = new List<Rectangle>
                        {
                            new Rectangle(168, 0, 25, 27),
                            new Rectangle(168, 28, 25, 27),
                            new Rectangle(168, 55, 25, 27)
                        };

                        health = 5;
                        maxHealth = health;
                        damage = 10;
                        speed = 4;
                        state = 0x00;

                        break;
                    }
                case ShipType.Needle:
                    {
                        animFly = new List<Rectangle>
                        {
                            new Rectangle(95, 84, 25, 27),
                            new Rectangle(120, 84, 25, 27),
                            new Rectangle(145, 84, 25, 27)
                        };

                        health = 5;
                        maxHealth = health;
                        damage = 10;
                        speed = 7;
                        state = 0x00;
                        break;
                    }
                default:
                    {
                        animFly = new List<Rectangle>
                        {
                            new Rectangle(95, 0, 25, 27),
                            new Rectangle(120, 0, 25, 27),
                            new Rectangle(145, 0, 25, 27)
                        };


                        health = 5;
                        maxHealth = health;
                        damage = 10;
                        speed = 5;
                        state = 0x00;
                        break;
                    }
            }   // end switch 

            sprite = new Sprite(texture, animFly[2], 2.0);



        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            sprite.UpdateRect(animFly[(int)currentFrame]);
            sprite.Draw(spriteBatch, new Vector2(xPos, yPos), SpriteEffects.None);
        
        }

        public override bool TakeDamage(int damage)
        {
            if (health == 0 && pointsTallied == false)
            {
                pointsTallied = true;
                game.addPoints(1);
            }

            return base.TakeDamage(damage);
        }

        public override void Update(GameTime gameTime)
        {

            switch (aiType)
            {
                case AIType.AngleLeft:
                    {
                        currentFrame = EnemyAnimFrame.Left;
                        xPos -= speed / 2;
                        yPos += speed;

                        break;
                    }
                case AIType.AngleRight:
                    {
                        currentFrame = EnemyAnimFrame.Right;
                        xPos += speed / 2;
                        yPos += speed;

                        break;
                    }
                case AIType.Basic:
                    {
                        yPos += speed;
                        if (fireCooldownRemaining <= 0.0f)
                        {
                            Vector2 bulletPosition = new Vector2((float)xPos + gunBarrelOffset.X, (float)yPos + gunBarrelOffset.Y);
                            game.AddNewBullet(BulletType.FireBall, Direction.South, Team.Enemy, bulletPosition);

                            fireCooldownRemaining = fireDelay;
                        }
                        else
                        {
                            fireCooldownRemaining -= gameTime.ElapsedGameTime.TotalSeconds;

                        }
                        break;
                    }
                case AIType.ZigZag:
                default:
                    {
                        yPos += speed;


                        break;
                    }
            }   // end switch

        }


        public void Fire()
        {

        }

    }
}
