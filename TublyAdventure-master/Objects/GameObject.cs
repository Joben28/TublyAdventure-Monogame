using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TublyAdventures.Global;

namespace TublyAdventures
{
    public partial class GameObject : DrawableGameComponent
    {
        /// <summary>
        /// Identifying name of game object.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// X-Coordinate of this objects position.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Y-Coordinate of this objects position.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Width of this object.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Height of this object.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Weight of this object in the world.
        /// </summary>
        public int Mass { get; set; }

        /// <summary>
        /// Radius in which this object detects neighboring objects.
        /// </summary>
        public int Radius { get; set; }

        /// <summary>
        /// Current X velocity of this object.
        /// </summary>
        public float VelocityX { get; set; }

        /// <summary>
        /// Current Y velocity of this object.
        /// </summary>
        public float VelocityY { get; set; }

        /// <summary>
        /// This object does not move, and applied force does no effect.
        /// </summary>
        public bool IsStatic { get; set; }

        /// <summary>
        /// This object is solid and cannot be passed through.
        /// </summary>
        public bool IsRigid { get; set; }

        /// <summary>
        /// Whether this object is visible in the current world.
        /// </summary>
        public bool IsVisible { get; set; }

        /// <summary>
        /// This object should be checked for interactive triggers.
        /// </summary>
        public bool IsInteractive { get; set; }

        /// <summary>
        /// Visual texture of this object.
        /// </summary>
        public Texture2D Texture { get; set; }

        /// <summary>
        /// Color in which this object is rendered with.
        /// </summary>
        public Color RenderColor { get; set; }

        /// <summary>
        /// Collection of other nearby game objects. (See 'Radius')
        /// </summary>
        public List<GameObject> NeighboringObjects { get; set; }

        /// <summary>
        /// The anchor in which this object is landed on.
        /// </summary>
        public GameObject Anchor { get; set; }

        /// <summary>
        /// Source rectangle for drawing rectangle from texture. (Used for animation sprite sheets)
        /// </summary>
        public Rectangle DrawRectangle { get; set; }

        /// <summary>
        /// Physical boundary of this object in the world.
        /// </summary>
        public Rectangle Boundary
        {
            get
            {
                return new Rectangle(X, Y, Width, Height);
            }
        }

        /// <summary>
        /// Boundary for when nearby objects are considered neighbors.
        /// </summary>
        public Rectangle NeighborBoundary
        {
            get
            {
                return new Rectangle(X - (Radius / 2), Y - (Radius / 2), Width + Radius, Height + Radius);
            }
        }

        /// <summary>
        /// Game spritebatch for drawing this object.
        /// </summary>
        protected SpriteBatch _spriteBatch;

        /// <summary>
        /// Physical object in game with visable and physical representations.
        /// </summary>
        /// <param name="game">Current game instance.</param>
        public GameObject(Game game) : base(game)
        {
            //Gets sprite batch from game services. (See Game1.cs 'LoadContent' method)
            _spriteBatch = game.Services.GetService<SpriteBatch>();

            //Initial values on construction.
            NeighboringObjects = new List<GameObject>();
            IsVisible = true;
            IsStatic = true;
            IsRigid = true;
            Radius = 128;
            RenderColor = Color.White;
        }

        #region Update & Draw

        /// <summary>
        /// Checks if this object has an anchor prevent downward(-Y) velocity.
        /// </summary>
        private void CheckAnchor()
        {
            if (!IsStatic)
            {
                if (Anchor == null || VelocityY < 0)
                    Y += (int)VelocityY;
                else
                    VelocityY = 0;
            }
        }

        /// <summary>
        /// Updates neighbor collection and checks for colision.
        /// </summary>
        private void UpdateNeighbors()
        {
            foreach (var gameObject in NeighboringObjects.ToList())
            {
                if (ValidateAsNeighbor(gameObject))
                {
                    var depth = RectangleExtensions.GetIntersectionDepth(Boundary, gameObject.Boundary);
                    var collisionArgs = new CollisionEventArgs() { Depth = depth };

                    OnCollisionDetected(gameObject, collisionArgs);
                    CheckCollision(gameObject, collisionArgs);
                }
            }
        }

        /// <summary>
        /// Update override from parent 'DrawableGameComponent', is called on Game update in Game1.cs.
        /// </summary>
        /// <param name="gameTime">Game time of current game instance.</param>
        public override void Update(GameTime gameTime)
        {
            CheckAnchor();
            UpdateNeighbors();

            base.Update(gameTime);
        }

        /// <summary>
        /// Confirm that this object is a neighbor and is still active in the game.
        /// </summary>
        /// <param name="gameObject">Object to check</param>
        /// <returns>Whether or not this object is a neighbor.</returns>
        public bool ValidateAsNeighbor(GameObject gameObject)
        {
            if (Game.Components.Contains(gameObject))
            {
                if (NeighborBoundary.Intersects(gameObject.Boundary))
                {
                    if (!NeighboringObjects.Contains(gameObject))
                        NeighboringObjects.Add(gameObject);

                    return true;
                }
                else
                {
                    if (NeighboringObjects.Contains(gameObject))
                        NeighboringObjects.Remove(gameObject);
                }
            }
            else
            {
                if (NeighboringObjects.Contains(gameObject))
                    NeighboringObjects.Remove(gameObject);

                if (gameObject == Anchor)
                    Anchor = null;
            }
            return false;
        }

        /// <summary>
        /// Draw function override from parent 'DrawableComponent'.
        /// </summary>
        /// <param name="gameTime">Current game instance game time.</param>
        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(transformMatrix: Game1.Camera.GetViewMatrix());

            if (IsVisible) //If this object is visible, then draw.
                _spriteBatch.Draw(Texture, Boundary, DrawRectangle, RenderColor);

            base.Draw(gameTime);
            _spriteBatch.End();
        }
        #endregion

        #region Movement
        /// <summary>
        /// Apply a specified force to this object.
        /// </summary>
        /// <param name="x">X velocity</param>
        /// <param name="y">Y velocity</param>
        public void ApplyForce(float x, float y)
        {
            VelocityX += x;
            VelocityY += y;
        }

        /// <summary>
        /// Move this object on it's X-coordinate.
        /// </summary>
        /// <param name="value">Value for this object to move.</param>
        public void MoveX(int value)
        {
            //Padded boundary for safe collision checks on smaller movements.
            var territoryBoundary = new Rectangle((int)X, (int)Y, Boundary.Width+2, Boundary.Height+2);

            //Determine if this movement can or cannot be made.
            bool canMoveRight = true;
            bool canMoveLeft = true;

            //Checks if this object will be prevented by any solid game object neighbors.
            foreach (var no in NeighboringObjects.ToList())
            {
                if (territoryBoundary.Intersects(no.Boundary))
                {
                    var depth = RectangleExtensions.GetIntersectionDepth(territoryBoundary, no.Boundary);

                    if (depth.Y < -8 || depth.Y > 8)
                    {
                        if (depth.X < 0 && depth.X > -8)
                        {
                            canMoveRight = false;
                            OnRightCollided(no, new CollisionEventArgs() { Depth = depth });
                        }
                        if (depth.X > 0 && depth.X < 8)
                        {
                            canMoveLeft = false;
                            OnLeftCollided(no, new CollisionEventArgs() { Depth = depth });
                        }
                    }
                }
            }

            if (value > 0 && canMoveRight)
                X += value;
            if (value < 0 && canMoveLeft)
                X += value;

            VelocityX = value;
        }
        #endregion

    }
}
