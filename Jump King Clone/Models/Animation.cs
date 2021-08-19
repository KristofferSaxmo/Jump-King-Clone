using Microsoft.Xna.Framework.Graphics;
using System;

namespace Jump_King_Clone.Models
{
    public class Animation : ICloneable
    {
        public int CurrentFrame { get; set; }

        public int[] Frames { get; private set; }

        public int FrameHeight { get { return Texture.Height; } }

        public float[] FrameSpeed { get; set; }

        public int FrameWidth { get; private set; }

        public bool IsLooping { get; set; }

        public Texture2D Texture { get; private set; }

        public Animation(Texture2D texture, int[] frames, float[] frameSpeed, int frameWidth)
        {
            Texture = texture;

            Frames = frames;

            FrameWidth = frameWidth;

            IsLooping = false;

            FrameSpeed = frameSpeed;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}