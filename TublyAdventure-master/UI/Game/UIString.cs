using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TublyAdventures
{
    public class UIString : DrawableGameComponent
    {
        /// <summary>
        /// Sprite font for this UI string.
        /// </summary>
        public SpriteFont Font { get; set; }

        /// <summary>
        /// Text value of what this UI string displays.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Position of this UI string on window.
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// Color in which this UI string will render.
        /// </summary>
        public Color RenderColor { get; set; }

        /// <summary>
        /// Width of this UI string.
        /// </summary>
        public float Width { get { return Font.MeasureString(Text).X; } }

        /// <summary>
        /// Height of this UI string.
        /// </summary>
        public float Height { get { return Font.MeasureString(Text).Y; } }

        protected SpriteBatch _spriteBatch;

        /// <summary>
        /// A UI string to be displayed to thegame window.
        /// </summary>
        /// <param name="game">Current game instance.</param>
        /// <param name="font">SpriteFont texture for this UI string.</param>
        public UIString(Game game, SpriteFont font) : base(game)
        {
            //Initial values
            Font = font;
            RenderColor = Color.Black;
            _spriteBatch = game.Services.GetService<SpriteBatch>();
        }

        /// <summary>
        /// Draw this UI string to the window.
        /// </summary>
        /// <param name="gameTime">Current game instance game time.</param>
        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.DrawString(Font, Text, Position, RenderColor);
            base.Draw(gameTime);
        }
    }
}
