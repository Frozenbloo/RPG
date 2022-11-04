#region Using
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
#endregion

namespace Imbroglios
{
    public class FrameAnimation
    {
        public bool hasFired;
        protected int frames, currentFrame, maxPasses, currentPass, fireFrame;
        public string name;
        public Vector2 sheet, startFrame, sheetFrame, spriteDims;
        public TBTimer frameTimer;

        public PassObject FireAction;

        public FrameAnimation(Vector2 SPRITEDIMS, Vector2 SHEETDIMS, Vector2 START, int TOTALFRAMES, int TIMEPERFRAME, int MAXPASSES, string NAME = "")
        {
            spriteDims = SPRITEDIMS;
            sheet = SHEETDIMS;
            startFrame = START;
            sheetFrame = new Vector2(START.X, START.Y);
            frames = TOTALFRAMES;
            currentFrame = 0;
            frameTimer = new TBTimer(TIMEPERFRAME); //in milisecs
            maxPasses = MAXPASSES;
            currentPass = 0;
            name = NAME;
            FireAction = null;
            hasFired = false;
            fireFrame = 0;
        }

        public FrameAnimation(Vector2 SPRITEDIMS, Vector2 SHEETDIMS, Vector2 START, int TOTALFRAMES, int TIMEPERFRAME, int MAXPASSES, int FIREFRAME, PassObject FIREACTION, string NAME = "")
        {
            spriteDims = SPRITEDIMS;
            sheet = SHEETDIMS;
            startFrame = START;
            sheetFrame = new Vector2(START.X, START.Y);
            frames = TOTALFRAMES;
            currentFrame = 0;
            frameTimer = new TBTimer(TIMEPERFRAME); //in milisecs
            maxPasses = MAXPASSES;
            currentPass = 0;
            name = NAME;
            FireAction = FIREACTION;
            hasFired = false;
            fireFrame = FIREFRAME;
        }

        #region Properties
        public int Frames
        {
            get { return frames; }
        }

        public int CurrentFrame
        {
            get { return currentFrame; }
        }

        public int CurrentPass 
        { 
            get { return currentPass; } 
        }

        public int MaxPasses
        {
            get { return maxPasses; }
        }
        #endregion

        //REMINDER DON'T EVER TOUCH AGAIN YOU PERFECTED IT SO DON'T BE STUPID AND BREAK IT AGAIN
        public void Update()
        {
            if (frames > 1)
            {
                frameTimer.UpdateTimer();
                if (frameTimer.Test() && (maxPasses == 0 || maxPasses > currentPass))
                {
                    currentFrame++;
                    if (currentFrame >= frames)
                    {
                        currentPass++;
                    }
                    if (maxPasses == 0 || maxPasses > currentPass)
                    {
                        sheetFrame.X += 1;

                        if (sheetFrame.X >= sheet.X)
                        {
                            sheetFrame.X = 0;
                            sheetFrame.Y += 1;
                        }
                        if (currentFrame >= frames)
                        {
                            currentFrame = 0;
                            hasFired = false;
                            sheetFrame = new Vector2(startFrame.X, startFrame.Y);
                        }
                    }
                    frameTimer.Reset();
                }
            }
            if (FireAction != null && fireFrame == currentFrame && !hasFired)
            {
                FireAction(null);
                hasFired = true;
            }
        }

        public void Reset()
        {
            currentFrame = 0;
            currentPass = 0;
            sheetFrame = new Vector2(startFrame.X, startFrame.Y);
            hasFired = false;
        }

        public bool IsAtEnd()
        {
            if (currentFrame+1 >= frames)
            {
                return true;
            }
            return false;
        }

        public void Draw(Texture2D MYMODEL, Vector2 DIMENSIONS, Vector2 IMAGEDIMS, Vector2 OFFSET, Vector2 POSITION, float ROTATION, Color COLOR, SpriteEffects SPRITEEFFECT)
        {
            Globals.spriteBatch.Draw(MYMODEL, new Rectangle((int)((POSITION.X + OFFSET.X)), (int)((POSITION.Y + OFFSET.Y)), (int)Math.Ceiling(DIMENSIONS.X), (int)Math.Ceiling(DIMENSIONS.Y)), new Rectangle((int)(sheetFrame.X*IMAGEDIMS.X), (int)(sheetFrame.Y*IMAGEDIMS.Y), (int)IMAGEDIMS.X, (int)IMAGEDIMS.Y),COLOR, ROTATION, IMAGEDIMS/2, SPRITEEFFECT, 0);
        }
    }
}
