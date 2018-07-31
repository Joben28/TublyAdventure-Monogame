using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TublyAdventures.Global;

namespace TublyAdventures.Objects.Entities
{
    public class Monster : Entity
    {
        /// <summary>
        /// Current index of the destination this monster is traveling to.
        /// </summary>
        private int _destinationIndex = 0;

        /// <summary>
        /// Vector2 position this monster is currently traveling to.
        /// </summary>
        private Vector2 TargetDestination;

        /// <summary>
        /// Destinations in which this monster will travel to.
        /// </summary>
        private List<Vector2> Destinations;

        /// <summary>
        /// Vector 2 position this monster initally spawned at.
        /// </summary>
        private Vector2 OriginalPosition;

        /// <summary>
        /// Monster or 'enemy' entity for the game.
        /// </summary>
        /// <param name="game">Current game instance.</param>
        public Monster(Game game) : base(game)
        {
            //Initial values
            Texture = Game.Content.Load<Texture2D>("Player/blob_Characters");
            SetAnimationAtlas(CharacterBuilder.BuildCharacter("Blue"));
            SpriteTitle.Text = "Evil Librarian";
            SpriteTitle.RenderColor = Color.OrangeRed;


            LeftCollided += CollidedWithLeft;
            RightCollided += CollidedWithRight;

            Speed = 4;
            JumpHeight = 6;
        }

        /// <summary>
        /// Called when this monster object is loaded in game.
        /// </summary>
        protected override void LoadContent()
        {
            OriginalPosition = new Vector2(X, Y);
            Destinations = new List<Vector2>()
            {
                new Vector2(Game1.random.Next(X - 800, X + 800), Y),
                new Vector2(Game1.random.Next(X - 800, X + 800), Y)
            };
            TargetDestination = Destinations[_destinationIndex];
            base.LoadContent();
        }

        /// <summary>
        /// Called when this object experiences a collision on the right side.
        /// </summary>
        /// <param name="sender">Object it's colliding with.</param>
        /// <param name="args">Collision event arguments.</param>
        private void CollidedWithRight(GameObject sender, CollisionEventArgs args)
        {
            if (sender.GetType() == typeof(Player))
            {
                var player = sender as Player;
                player.IsDead = true;
            }
        }

        /// <summary>
        /// Called when this object experiences a collision on the left side.
        /// </summary>
        /// <param name="sender">Object it's colliding with.</param>
        /// <param name="args">Collision event arguments.</param>
        private void CollidedWithLeft(GameObject sender, CollisionEventArgs args)
        {
            if (sender.GetType() == typeof(Player))
            {
                var player = sender as Player;
                player.IsDead = true;
            }
        }

        /// <summary>
        /// Update method for this monster object during game loop.
        /// </summary>
        /// <param name="gameTime">Current game instance game time.</param>
        public override void Update(GameTime gameTime)
        {

            if (X - TargetDestination.X < 5 && X - TargetDestination.X > -5)
            {
                if (_destinationIndex < Destinations.Count - 1)
                {
                    _destinationIndex++;
                }
                else
                {
                    _destinationIndex = 0;
                }
                TargetDestination = Destinations[_destinationIndex];
            }

            if (X > TargetDestination.X)
            {
                MoveX(-Speed);
                PlayAnimation(gameTime, "Left");
            }
            if (X < TargetDestination.X)
            {
                MoveX(Speed);
                PlayAnimation(gameTime, "Right");
            }


            base.Update(gameTime);
        }
    }
}
