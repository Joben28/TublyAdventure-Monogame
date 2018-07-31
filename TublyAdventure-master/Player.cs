using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TublyAdventures.Objects.Entities;
using TublyAdventures.Global;

namespace TublyAdventures
{
    public class Player : Entity
    {
        /// <summary>
        /// Base speed of player before acceleration.
        /// </summary>
        private double _startSpeed = 4;

        /// <summary>
        /// Has this player used their double jump.
        /// </summary>
        private bool _hasDoubleJumped;

        /// <summary>
        /// Player object controlled by game user.
        /// </summary>
        /// <param name="game">Current game instance.</param>
        public Player(Game game) : base(game)
        {
            //Initial values
            Texture = Game.Content.Load<Texture2D>("Player/blob_Characters");
            SetAnimationAtlas(CharacterBuilder.BuildCharacter("Yellow"));
            SpriteTitle.Text = "Tosker";
            SpriteTitle.RenderColor = Color.Green;
            IsStatic = false;
            Width = 64;
            Height = 64;
            Speed = 6;
            JumpHeight = 8;

            //Delegate game object collison events
            BottomCollided += Landed;
        }

        /// <summary>
        /// Accelerate the player towards max speed.
        /// </summary>
        private void Accelerate()
        {
            if (_startSpeed < Speed && Anchor != null)
            {
                _startSpeed += 0.02;
            }
        }

        /// <summary>
        /// Method called when player has landed on another game object.
        /// </summary>
        /// <param name="sender">Game object collided with.</param>
        /// <param name="args">Collision event arguments.</param>
        private void Landed(GameObject sender, CollisionEventArgs args)
        {
            if(sender.GetType() == typeof(Monster))
            {
                var monster = sender as Monster;
                NeighboringObjects.Remove(monster);
                monster.IsDead = true;
                ApplyForce(0, -10);
            }
        }

        /// <summary>
        /// Check the keyboard input of the player.
        /// </summary>
        /// <param name="gameTime">Current game instance game time. This is used for aniamtion.</param>
        private void CheckInput(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                Move((int)_startSpeed);
                PlayAnimation(gameTime, "Right");
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                Move((int)-_startSpeed);
                PlayAnimation(gameTime, "Left");
            }
            else
            {
                //Player has stopped moving, restart player speed to base
                _startSpeed = 4;
                SetDefaultFrame();
            }

            if (CheckKey(Keys.Space))
            {
                Jump();
            }
        }

        /// <summary>
        /// Move the player left or right.
        /// </summary>
        /// <param name="x">X position value change.</param>
        private void Move(int x)
        {
            MoveX(x);
            Accelerate();
        }

        /// <summary>
        /// Make the current player jump.
        /// </summary>
        private void Jump()
        {
            if (!_hasDoubleJumped && Anchor != null)
            {
                VelocityY = 0;
                ApplyForce(0, -JumpHeight);
            }
            else if (!_hasDoubleJumped && VelocityY > -2)
            {
                VelocityY = 0;
                ApplyForce(0, -JumpHeight);
                _hasDoubleJumped = true;
            }
        }

        /// <summary>
        /// Player update method
        /// </summary>
        /// <param name="gameTime">Current game instance game time.</param>
        public override void Update(GameTime gameTime)
        {
            //Focus game camera on the player
            Game1.Camera.FocusGameObject(this);

            //If player has an anchor, they have landed and double jump refreshes
            if (Anchor != null)
                _hasDoubleJumped = false;

            //Check the keyboard input
            CheckInput(gameTime);

            base.Update(gameTime);
        }
    }
}
