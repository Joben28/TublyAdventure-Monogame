using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TublyAdventures
{
    public class Camera2D
    {
        /// <summary>
        /// Camera view port from graphics device of game instance.
        /// </summary>
        private readonly Viewport _viewport;

        /// <summary>
        /// 2D Camera for game drawing and rendering.
        /// </summary>
        /// <param name="viewport">Viewport of graphics device for game.</param>
        public Camera2D(Viewport viewport)
        {
            //Initial values
            _viewport = viewport;
            Rotation = 0;
            Zoom = 1;
            Origin = new Vector2(viewport.Width / 2f, viewport.Height / 2f);
            Position = Vector2.Zero;
        }

        /// <summary>
        /// Position of this camera.
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// Rotation value for this camera.
        /// </summary>
        public float Rotation { get; set; }

        /// <summary>
        /// Zoom value for this camera.
        /// </summary>
        public float Zoom { get; set; }

        /// <summary>
        /// Origin position of this camera.
        /// </summary>
        public Vector2 Origin { get; set; }

        /// <summary>
        /// Get the view matrix translation of this camera.
        /// </summary>
        /// <returns>View matrics translation.</returns>
        public Matrix GetViewMatrix()
        {
            return
                Matrix.CreateTranslation(new Vector3(-Position, 0.0f)) *
                Matrix.CreateTranslation(new Vector3(-Origin, 0.0f)) *
                Matrix.CreateRotationZ(Rotation) *
                Matrix.CreateScale(Zoom, Zoom, 1) *
                Matrix.CreateTranslation(new Vector3(Origin, 0.0f));
        }

        /// <summary>
        /// Focus this camera on a specified game object.
        /// </summary>
        /// <param name="obj">Game object to focus.</param>
        public void FocusGameObject(GameObject obj)
        {
            Position = new Vector2(obj.X - (_viewport.Width/2), 0);
        }

        /// <summary>
        /// Get the centered position of this cameras viewport.
        /// </summary>
        /// <returns>Centered camera position.</returns>
        public Vector2 CenterPosition()
        {
            return new Vector2(_viewport.Width / 2, _viewport.Height / 2);
        }

        /// <summary>
        /// Get centered position of this camera view port with and offset.
        /// </summary>
        /// <param name="xOffset">X position offset.</param>
        /// <param name="yOffset">Y position offset.</param>
        /// <returns>Center position with specified offset.</returns>
        public Vector2 CenterPosition(float xOffset, float yOffset)
        {
            return new Vector2((_viewport.Width / 2) - xOffset, (_viewport.Height / 2) - yOffset);
        }
    }
}
