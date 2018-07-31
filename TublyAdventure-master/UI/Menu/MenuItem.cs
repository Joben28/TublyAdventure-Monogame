using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TublyAdventures.UI
{
    public class MenuItem : UIString
    {
        /// <summary>
        /// Delegate to invoke on menu item execution.
        /// </summary>
        public Action Execute { get; set; } = Console.Beep;

        /// <summary>
        /// Color representation of a highlighted item.
        /// </summary>
        private Color HighlightColor { get; set; }

        /// <summary>
        /// This item is currently selected.
        /// </summary>
        public bool IsSelected { get; set; }

        /// <summary>
        /// Menu item for a menu scene collection.
        /// </summary>
        /// <param name="game">Current game instance.</param>
        /// <param name="font">SpriteFont for this menu item.</param>
        /// <param name="text">String value for this menu item.</param>
        public MenuItem(Game game, SpriteFont font, string text) : base(game, font)
        {
            Text = text;
            HighlightColor = Color.Yellow;
        }

        /// <summary>
        /// Draw this menu item to the window.
        /// </summary>
        /// <param name="gameTime">Current game instance game time.</param>
        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            //Draw with different color value if item is selected
            if (!IsSelected)
                _spriteBatch.DrawString(Font, Text, Position, RenderColor);
            else
                _spriteBatch.DrawString(Font, Text, Position, HighlightColor);

            _spriteBatch.End();

        }
    }
}
