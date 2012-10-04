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
    public enum ExplosionType
    {
        Basic = 0,
        Tiny = 1,
        Huge = 2
    }

    class Explosion:Entity
    {
        ExplosionType type;

        List<Rectangle> animDie;

        double animTimeElapsed = 0;
        double animDelay = 0.10;
        int frameNumber;
        int maxFrames;

        public Explosion(Texture2D texture, ExplosionType thisType, Vector2 position)
        {
            type = thisType;

            xPos = (int)position.X;
            yPos = (int)position.Y;

            health = 1;
            maxHealth = 1;


            switch (type)
            {
                case ExplosionType.Basic:
                    {
                        animDie = new List<Rectangle>
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
                        };

                        break;

                    }
                case ExplosionType.Huge:
                case ExplosionType.Tiny:
                default:
                    {
                        // this data is for the Tiny explosion
                        // ideally this would be stored in an external file
                        animDie = new List<Rectangle>
                        {
                            // TODO: see if we can fix these
                            new Rectangle(72, 84, 12, 13),              // #1
                            new Rectangle(84, 84, 12, 13),
                            new Rectangle(96, 84, 12, 13),              // #3
                            new Rectangle(108, 84, 12, 13),
                            new Rectangle(120, 84, 12, 13),             // #5
                            new Rectangle(132, 84, 12, 13),
                            new Rectangle(144, 84, 12, 13),             // #7
                            new Rectangle(156, 84, 12, 13), 
                            new Rectangle(168, 84, 12, 13),             // #9
                            new Rectangle(180, 84, 12, 13),
                            new Rectangle(192, 84, 12, 13),             // #11
                            new Rectangle(204, 84, 12, 13),
                            new Rectangle(212, 84, 12, 13),             // #13
   
                        };

                        

                        break;
                    }
            }

            maxFrames = animDie.Count();            // get the number of frames programmatically
                

            frameNumber = 0;            // start with the 1st frame
            sprite = new Sprite(texture, animDie[frameNumber], 2.0);


        }


        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            sprite.UpdateRect(animDie[frameNumber]);        // update to the newest frame
            sprite.Draw(spriteBatch, new Vector2(xPos, yPos), SpriteEffects.None);

            base.Draw(spriteBatch, gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            animTimeElapsed += gameTime.ElapsedGameTime.TotalSeconds;
            if (animTimeElapsed >= animDelay)
            {
                animTimeElapsed -= animDelay;
                ++frameNumber;
                if (frameNumber >= maxFrames)
                {
                    frameNumber = 0;                // make sure that we are still pointing at something!
                    health = 0;                     // kill it! we're finished
                }
            }

            base.Update(gameTime);
        }
    }
}
