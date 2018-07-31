using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TublyAdventures.Objects.Animation
{
    public class AnimatedObject : GameObject
    {
        /// <summary>
        /// Atlas that hold this animated objects animation slides.
        /// </summary>
        private AnimationAtlas _atlas;

        /// <summary>
        /// Amount of miliseconds since the last animation frame played.
        /// </summary>
        private double LastUpdate = 0;

        /// <summary>
        /// Amount of miliseconds for a delay between playing frames.
        /// </summary>
        public double Delay = 100;

        /// <summary>
        /// An animated game object.
        /// </summary>
        /// <param name="game">Current game instance.</param>
        public AnimatedObject(Game game) : base(game)
        {
            //Initial values
            _atlas = new AnimationAtlas();
        }

        /// <summary>
        /// Add a new slide to this animated objects atlas.
        /// </summary>
        /// <param name="slideKey">Indentifying key for this animation slide.</param>
        /// <param name="slide">Slide to add to this animated objects atlas.</param>
        protected void AddAnimationSlide(string slideKey, AnimationSlide slide)
        {
            if (_atlas.CurrentSlide == null)
            {
                _atlas.CurrentSlide = slide;
                var t = _atlas.CurrentSlide.CurrentTexture;
                DrawRectangle = DrawRectangle = new Rectangle(t.X, t.Y, t.Width, t.Height);
            }

            _atlas.AddSlide(slideKey, slide);
        }

        /// <summary>
        /// Set the animation atlas for this animated object.
        /// </summary>
        /// <param name="atlas">An animation atlas supplied with animation slides.</param>
        public void SetAnimationAtlas(AnimationAtlas atlas)
        {
            _atlas = atlas;
        }

        /// <summary>
        /// Play the animation for this animated game object.
        /// </summary>
        /// <param name="gameTime">Current game instance game time.</param>
        /// <param name="slideKey">Indentifying key for the animation slide to be played.</param>
        public void PlayAnimation(GameTime gameTime, string slideKey)
        {
            if (LastUpdate > Delay)
            {
                _atlas.PlaySlide(slideKey);
                var t = _atlas.CurrentSlide.CurrentTexture;

                DrawRectangle = new Rectangle(t.X, t.Y, t.Width, t.Height);
                LastUpdate = 0;
            }

            LastUpdate += gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        /// <summary>
        /// Have this animated object display it's single default animation frame.
        /// </summary>
        public void SetDefaultFrame()
        {
            var t = _atlas.CurrentSlide.DefaultTexture;
            DrawRectangle = new Rectangle(t.X, t.Y, t.Width, t.Height);
        }
    }
}
