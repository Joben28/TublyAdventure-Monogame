using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TublyAdventures.Objects.Tiles
{
    public class BouncyTile : Tile
    {
        /// <summary>
        /// A game object tile block that has bouncing properties in objects it anchors.
        /// </summary>
        /// <param name="game">Current game instance.</param>
        /// <param name="texture">Texture of this tile block.</param>
        /// <param name="sourceRectangle">Rectangle source for this tile in a sprite sheet.</param>
        public BouncyTile(Game game, Texture2D texture, Rectangle sourceRectangle) : base(game, texture, sourceRectangle)
        {
            TopCollided += BounceObject;
            IsInteractive = true;
        }

        /// <summary>
        /// Tied to a TopCollided event thrown in a game object when an object lands on it.
        /// </summary>
        /// <param name="sender">Object which has collided with this top.</param>
        /// <param name="args">Collision even arguments for this collision.</param>
        private void BounceObject(GameObject sender, CollisionEventArgs args)
        {
            if(args.Depth.X >= 40 || args.Depth.X <= -40)
            {
                sender.VelocityY = 0;
                sender.ApplyForce(0, -10);
            }
        }
    }
}
