using Microsoft.Xna.Framework;
using System;

namespace TublyAdventures
{
    public class CollisionEventArgs : EventArgs
    {
        public Vector2 Depth;
        public float VelocityY;
    }

    public partial class GameObject
    {
        public delegate void CollisionEventHandler(GameObject sender, CollisionEventArgs args);

        public event CollisionEventHandler CollisionDetected;
        public event CollisionEventHandler TopCollided;
        public event CollisionEventHandler BottomCollided;
        public event CollisionEventHandler LeftCollided;
        public event CollisionEventHandler RightCollided;

        private void OnCollisionDetected(GameObject gameObject, CollisionEventArgs args)
        {
            CollisionDetected?.Invoke(gameObject, args);
        }

        private void OnBottomCollided(GameObject gameObject, CollisionEventArgs args)
        {
            BottomCollided?.Invoke(gameObject, args);

            if (gameObject.IsRigid && !IsStatic)
            {
                Y = gameObject.Y - Height + 1;

                if (Anchor == null || Anchor != gameObject)
                {
                    Anchor = gameObject;
                }
            }
        }

        private void OnTopCollided(GameObject gameObject, CollisionEventArgs args)
        {
            TopCollided?.Invoke(gameObject, args);
            VelocityY = Math.Abs(VelocityY);
        }

        private void OnLeftCollided(GameObject gameObject, CollisionEventArgs args)
        {
            LeftCollided?.Invoke(gameObject, args);
        }

        private void OnRightCollided(GameObject gameObject, CollisionEventArgs args)
        {
            RightCollided?.Invoke(gameObject, args);
        }

        private void CheckCollision(GameObject gameObj, CollisionEventArgs args)
        {
            if (!Game.Components.Contains(gameObj))
                return;

            if (args.Depth.Y > 0 && args.Depth.Y < 16)
            {
                if (args.Depth.X > 4 || args.Depth.X < -4)
                {
                    OnTopCollided(gameObj, args);
                }
            }

            if (args.Depth.Y < 0 && args.Depth.Y > -32)
            {
                if (args.Depth.X > 4|| args.Depth.X < -4)
                {
                    OnBottomCollided(gameObj, args);
                }
            }
            else if (Anchor == gameObj)
            {
                Anchor = null;
            }

            if (args.Depth.Y < -8 || args.Depth.Y > 8)
            {
                if (args.Depth.X < 0 && args.Depth.X > -16)
                {
                    OnLeftCollided(gameObj, args);
                }
            }

            if (args.Depth.Y < -8 || args.Depth.Y > 8)
            {
                if (args.Depth.X > 0 && args.Depth.X < 16)
                {
                    OnRightCollided(gameObj, args);
                }
            }
        }
    }
}