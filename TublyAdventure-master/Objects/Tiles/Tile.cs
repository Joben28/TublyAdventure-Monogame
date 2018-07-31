using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TublyAdventures.Objects.Tiles
{
    public class Tile : GameObject
    {
        /// <summary>
        /// A game object tile block.
        /// </summary>
        /// <param name="game">Current game isntance.</param>
        /// <param name="texture">Texture of this tile block.</param>
        /// <param name="sourceRectangle">Rectangle source for this tile in a sprite sheet.</param>
        public Tile(Game game, Texture2D texture, Rectangle sourceRectangle) : base(game)
        {
            //Initial values
            DrawRectangle = sourceRectangle;
            Width = sourceRectangle.Width;
            Height = sourceRectangle.Height;
            Texture = texture;
        }
    }
}
