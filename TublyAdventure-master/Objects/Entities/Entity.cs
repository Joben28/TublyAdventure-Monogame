using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TublyAdventures.Objects.Animation;

namespace TublyAdventures.Objects.Entities
{
    public class Entity : AnimatedObject
    {
        /// <summary>
        /// UI string displayed above this entity.
        /// </summary>
        protected UIString SpriteTitle;

        /// <summary>
        /// Is this entity still considered alive.
        /// </summary>
        public bool IsDead { get; set; }

        /// <summary>
        /// Speed value of this entity.
        /// </summary>
        public int Speed { get; set; }

        /// <summary>
        /// Jump value of this entity.
        /// </summary>
        public int JumpHeight { get; set; }

        /// <summary>
        /// Keyboard state with previous input.
        /// </summary>
        protected KeyboardState _previousKeyboardState;

        /// <summary>
        /// A operational game object entity, that can move, jump and be pronounced dead or alive.
        /// </summary>
        /// <param name="game">Current game instance.</param>
        public Entity(Game game) : base(game)
        {
            //Initial values
            IsStatic = false;
            Width = 64;
            Height = 64;
            IsInteractive = true;
            SpriteTitle = new UIString(game, game.Content.Load<SpriteFont>("UI/Fonts/DebugFont"));
            SpriteTitle.Text = string.Empty;
        }

        /// <summary>
        /// Update method for this entity object.
        /// </summary>
        /// <param name="gameTime">Current game instance game time.</param>
        public override void Update(GameTime gameTime)
        {
            UpdateSpriteTitle();

            if (Y > Game.GraphicsDevice.Viewport.Height)
            {
                IsDead = true;
            }

            _previousKeyboardState = Keyboard.GetState();
            base.Update(gameTime);
        }

        /// <summary>
        /// Update this entitiy's UI string text and position value.
        /// </summary>
        public virtual void UpdateSpriteTitle()
        {
            if(SpriteTitle.Text != string.Empty)
            {
                SpriteTitle.Position = new Vector2(X, Y - SpriteTitle.Height);
            }

            if(IsDead)
            {
                Game.Components.Remove(this);
            }
        }

        /// <summary>
        /// Draw this entity to the game world.
        /// </summary>
        /// <param name="gameTime">Current game instance game time.</param>
        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(transformMatrix: Game1.Camera.GetViewMatrix());
            SpriteTitle.Draw(gameTime);
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// Check is a key has been pressed a single time.
        /// </summary>
        /// <param name="key">Keyboard key pressed.</param>
        /// <returns>If this key has been pushed a single time.</returns>
        protected bool CheckKey(Keys key)
        {
            if (Keyboard.GetState().IsKeyDown(key) && _previousKeyboardState.IsKeyUp(key))
                return true;

            return false;
        }
    }
}
