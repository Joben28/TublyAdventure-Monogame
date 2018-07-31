using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TublyAdventures.Objects.Animation
{
    public class AnimationAtlas
    {
        /// <summary>
        /// Animation slides held within this atlas.
        /// </summary>
        public Dictionary<string, AnimationSlide> Slides { get; private set; }

        /// <summary>
        /// Current slide being displayed for this animation atlas.
        /// </summary>
        public AnimationSlide CurrentSlide { get; set; }

        /// <summary>
        /// Atlas for holding animation slide collections.
        /// </summary>
        public AnimationAtlas()
        {
            Slides = new Dictionary<string, AnimationSlide>();
        }

        /// <summary>
        /// Play the animations in a slide.
        /// </summary>
        /// <param name="slideKey">Key for the slide in collection.</param>
        public void PlaySlide(string slideKey)
        {
            CurrentSlide = Slides[slideKey];
            if(CurrentSlide != null)
            {
                CurrentSlide.PlayFrame();
            }
        }

        /// <summary>
        /// Add a new slide to the atlas' collection.
        /// </summary>
        /// <param name="slideKey">Indentifying key for this slide.</param>
        /// <param name="slide">Slide to add to atlas' collection.</param>
        public void AddSlide(string slideKey, AnimationSlide slide)
        {
            Slides.Add(slideKey, slide);
        }
    }
}
