using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// FIXME: do we need all of these XNA headers? Probably not.
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
    /// <summary>
    /// Faders overlay the full-screen for the purposes of fading to black or white.
    /// </summary>
    class Fader: Element
    {

        public const float DEFAULT_FADE_SHIFT = 0.01f;

        public enum FadeStatus
        {
            noFade = 0,
            fadeOut = 1,
            fadeIn = 2,
            black = 3
        };

        const float FADE_OPAQUE = 1.0f;
        const float FADE_TRANSPARENT = 0.0f;



        float fadeShift = 0.01f;

        FadeStatus fadeStatus;

        float currentFade = 1.0f;               // how transparent are we? 1.0f = completely opaque. 0.0f = completely transparent.

        Texture2D fadePixel;

        Rectangle fullScreen;

        public Fader(Texture2D texture, Rectangle newFullScreen)
        {
            // FIXME: not using Sprite. This might be dumb.

            fadePixel = texture;
            fullScreen = newFullScreen;

            fadeStatus = FadeStatus.noFade;
            currentFade = 0.0f;

        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (fadeStatus == FadeStatus.noFade) return;        // not fading, get outta here!
            if (currentFade == 0.0f) return;

            Color fadeColor = new Color(1.0f, 1.0f, 1.0f, currentFade);

            spriteBatch.Draw(fadePixel, fullScreen, fadeColor);

        }

        public override void Update(GameTime gameTime)
        {
            switch (fadeStatus)
            {
                case FadeStatus.fadeOut:
                    {
                        currentFade += fadeShift;
                        if (currentFade >= FADE_OPAQUE) currentFade = FADE_OPAQUE;

                        break;
                    }
                case FadeStatus.fadeIn:
                    {
                        currentFade -= fadeShift;
                        if (currentFade <= FADE_TRANSPARENT) currentFade = FADE_TRANSPARENT;

                        break;
                    }
                case FadeStatus.black:
                    {
                        if (currentFade != FADE_OPAQUE) currentFade = FADE_OPAQUE;

                        break;
                    }
                case FadeStatus.noFade:
                    {
                        if (currentFade != FADE_TRANSPARENT) currentFade = FADE_TRANSPARENT;

                        break;
                    }
                default:
                    {
                        // ERROR!

                        break;
                    }


            }

            base.Update(gameTime);
        }

        public void fadeIn(float newFadeShift)
        {
            currentFade = FADE_OPAQUE;
            fadeStatus = FadeStatus.fadeIn;
            fadeShift = newFadeShift;
        }

        public void fadeOut(float newFadeShift)
        {
            currentFade = FADE_TRANSPARENT;
            fadeStatus = FadeStatus.fadeOut;
            fadeShift = newFadeShift;
        }

    }
}
