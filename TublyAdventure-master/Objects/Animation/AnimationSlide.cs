using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TublyAdventures.Objects.Animation
{
    public class AnimationSlide
    {
        /// <summary>
        /// Index of the current sub-texture being displayed.
        /// </summary>
        public int Index { get; private set; }

        /// <summary>
        /// Number of sub-textures in collection.
        /// </summary>
        public int Count { get { return Animations.Count; } }

        /// <summary>
        /// Sub-texture to be displayed when animation is not being played.
        /// </summary>
        public SubTexture DefaultTexture;

        /// <summary>
        /// Sub-texture collection of animation frames.
        /// </summary>
        private List<SubTexture> Animations;

        /// <summary>
        /// Current sub-texture being displayed.
        /// </summary>
        public SubTexture CurrentTexture;

        /// <summary>
        /// Collection of sub-textures to be cycled through for animation.
        /// </summary>
        public AnimationSlide()
        {
            Animations = new List<SubTexture>();
        }

        /// <summary>
        /// Play the next frame in this animation slide.
        /// </summary>
        public void PlayFrame()
        {
            if (Index > Count - 1)
            {
                Index = 0;
            }

            CurrentTexture = Animations[Index];
            Index++;
        }

        /// <summary>
        /// Add a new sub-texture to this animation slide.
        /// </summary>
        /// <param name="texture">Sub-texture to add to collection.</param>
        public void AddTextureToSlide(SubTexture texture)
        {
            Animations.Add(texture);
            CurrentTexture = texture;
        }
    }
}
