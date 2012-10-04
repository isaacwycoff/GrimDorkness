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

    [Flags]
    public enum PlayerState
    {
        Attacking = 0x01,
        Dying = 0x02,
        Farting = 0x04,
    }

    public enum ShipAnim
    {
        FlyStraight = 0,
        FlyLeft = 1,
        FlyRight = 2
    }

    public enum PlayerCommand
    {
        Idle = 0,
        FlyLeft = 1,
        FlyRight = 2,
        FlyForward = 3,
        FlyBackward = 4,
        Fire = 5,
        AltFire = 6
    }

    public enum WeaponType
    {
        Basic = 0,
        RapidFire = 1,
        Beam = 2
    }

    public enum FlyingAnimFrame
    {
        FarLeft = 0,
        SlightLeft = 1,
        Straight = 2,
        SlightRight = 3,
        FarRight = 4
    }

    class PlayerShip:Entity
    {

        List<Rectangle> animFly;

        ShipAnim currentAnim;

        double animTimeElapsed = 0;
        double animDelay = 0.2;

        double fireCooldownRemaining = 0;
        double fireDelay = 0.2;

        Vector2 gunBarrelOffset;

        FlyingAnimFrame currentFrame = FlyingAnimFrame.Straight;     // fly straight



        public PlayerShip(Texture2D texture)
        {
            // set up the rectangles for the ship's animation:
            // ideally this would be stored in an external file
            animFly = new List<Rectangle>
            {
                new Rectangle(0, 140, 23, 29),                  // flying far left
                new Rectangle(24, 140, 23, 29),
                new Rectangle(48, 140, 23, 29),                 // flying straight
                new Rectangle(72, 140, 23, 29),
                new Rectangle(96, 140, 23, 29),                 // flying far right
            };

            // these should be defined in an external file:
            health = 100;
            maxHealth = health;
            damage = 10;
            xPos = 400;
            yPos = 500;
            speed = 5;
            state = 0x00;

            team = Team.Player;

            gunBarrelOffset = new Vector2(20.0f, -2.0f);     // TODO: find the exact barrel offset

            sprite = new Sprite(texture, animFly[2], 2.0);  // TODO: get rid of magic numbers!


            // PlayerShip specific:
            currentAnim = ShipAnim.FlyStraight;

        }
        public Vector2 GetBarrelPosition()
        {
            Vector2 gunBarrelPosition = new Vector2((float)xPos + gunBarrelOffset.X, (float)yPos + gunBarrelOffset.Y);
            return gunBarrelPosition;
        }
        public int GetHealth()
        {
            return health;
        }



        public int GetMaxHealth()
        {
            return maxHealth;
        }

        // the user has issued a command
        // the ship will attempt to comply
        public void SendCommand(PlayerCommand command, GameTime gameTime)
        {
            float timeScale = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timeScale = 1.0f;

            // default animation -- may get overwritten later in SendCommand:
            currentAnim = ShipAnim.FlyStraight;

            switch(command)
            {
                case PlayerCommand.Idle:
                {
                        // no commands from the user
                        break;
                }
                case PlayerCommand.FlyLeft:
                {
                    xPos -= (int)(speed * timeScale);
                    if (xPos <= 0) xPos = 0;

                    currentAnim = ShipAnim.FlyLeft;

                    break;
                }
                case PlayerCommand.FlyRight:
                {
                    xPos += (int)(speed * timeScale);
                    if (xPos >= 700) xPos = 700;


                    currentAnim = ShipAnim.FlyRight;

                    break;
                }
                case PlayerCommand.FlyForward:
                {
                    yPos -= (int)(speed * timeScale);
                    if (yPos <= 0) yPos = 0;

                    break;
                }
                case PlayerCommand.FlyBackward:
                {
                    yPos += (int)(speed * timeScale);
                    if (yPos >= 500) yPos = 500;       // TODO: get rid of magic numbers


                    break;
                }
                case PlayerCommand.Fire:
                {
                    fireCooldownRemaining = fireDelay;

                    break;
                }
                case PlayerCommand.AltFire:
                {

                    break;
                }
                default:
                {

                    break;
                }
            }


        }


        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {


            animTimeElapsed += gameTime.ElapsedGameTime.TotalSeconds;
            if (animTimeElapsed >= animDelay)
            {
                animTimeElapsed = 0;

                if (currentAnim == ShipAnim.FlyLeft)
                {
                    --currentFrame;
                    if (currentFrame < FlyingAnimFrame.FarLeft) currentFrame = FlyingAnimFrame.FarLeft;
                }
                else if (currentAnim == ShipAnim.FlyRight)
                {
                    ++currentFrame;
                    if (currentFrame > FlyingAnimFrame.FarRight) currentFrame = FlyingAnimFrame.FarRight;
                }
                else
                {
                    // if we're not moving left or right, tilt back towards the center:
                    if (currentFrame > FlyingAnimFrame.Straight) --currentFrame;
                    else if (currentFrame < FlyingAnimFrame.Straight) ++currentFrame;
                }

            }

            sprite.UpdateRect(animFly[(int)currentFrame]);
            sprite.Draw(spriteBatch, new Vector2(xPos, yPos), SpriteEffects.None);


        }

        public override void Update(GameTime gameTime)
        {
            // manage behavior cooldowns -- delays on certain actions to prevent them from being spammed

            if (fireCooldownRemaining > 0)
            {
                fireCooldownRemaining -= gameTime.ElapsedGameTime.TotalSeconds;
                if (fireCooldownRemaining < 0) fireCooldownRemaining = 0;
            }

            base.Update(gameTime);
        }

        public bool CanFire()
        {
            if (fireCooldownRemaining <= 0.0f) return true;
            return false;
        }

        public bool Repair(int damage)
        {
            if (health >= maxHealth) return false;

            health += damage;

            if (health > maxHealth) health = maxHealth;


            // TODO
            return true;
        }

        public bool Invulnerability(float time)
        {

            // TODO
            return true;
        }

        public bool SwitchWeapon(WeaponType type)
        {
            // TODO

            return true;
        }

        public bool AddAmmo()
        {
            // TODO

            return true;
        }



    }
}
