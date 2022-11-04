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

namespace Imbroglios { 
    public class Animated2D : Basic2D
    {
        public Vector2 frames;
        public List<FrameAnimation> frameAnimationList = new List<FrameAnimation>();
        public Color color;
        public bool frameAnimations;
        public int currentAnimation = 0;

        public Animated2D(string PATH, Vector2 POSITION, Vector2 DIMENSIONS, Vector2 FRAMES, Color COLOR) : base(PATH, POSITION, DIMENSIONS)
        {
            Frames = new Vector2(FRAMES.X, FRAMES.Y);
            color = COLOR;
        }

        #region Properties
        public Vector2 Frames
        {
            set
            {
                frames = value;
                if (myModel != null)
                {
                    frameSize = new Vector2(myModel.Bounds.Width / frames.X, myModel.Bounds.Height / frames.Y);
                }
            }
            get { return frames; }
        }
        #endregion

        public override void Update(Vector2 OFFSET)
        {
            if (frameAnimations && frameAnimationList != null && frameAnimationList.Count > currentAnimation)
            {
                frameAnimationList[currentAnimation].Update();
            }

            base.Update(OFFSET);
        }

        public virtual int GetAnimationFromName(string ANIMATIONNAME)
        {
            for (int i = 0; i < frameAnimationList.Count; i++)
            {
                if (frameAnimationList[i].name == ANIMATIONNAME)
                {
                    return i;
                }
            }
            //shorthand for nullable int
            return -1;
        }

        public virtual void SetAnimationBuName(string NAME)
        {
            int tempAnimation = GetAnimationFromName(NAME);

            if (tempAnimation == -1)
            {
                frameAnimationList[tempAnimation].Reset();
            }

            currentAnimation = tempAnimation;
        }

        public override void Draw(Vector2 OFFSET)
        {
            //If it doesnt need animated it just draws as a basic 2d.
            if (frameAnimations && frameAnimationList[currentAnimation].Frames > 0)
            {
                frameAnimationList[currentAnimation].Draw(myModel, dimensions, frameSize, OFFSET, position, rotation, color, new SpriteEffects());
            }
            else
            {
                base.Draw(OFFSET);
            }
        }
    }
}
