using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TublyAdventures.Scene
{
    public class BaseScene
    {
        /// <summary>
        /// Game instance this scene exists in.
        /// </summary>
        private Game _game;

        /// <summary>
        /// Game object collection for this scene.
        /// </summary>
        public List<GameObject> GameObjects { get; private set; }

        /// <summary>
        /// Drawable component collection for this scene -- usually for UI sprites.
        /// </summary>
        public List<DrawableGameComponent> DrawableComponents { get; private set; }

        /// <summary>
        /// Component collection for this scene.
        /// </summary>
        public List<GameComponent> Components { get; private set; }

        /// <summary>
        /// Keyboard state with previous input.
        /// </summary>
        protected KeyboardState _previousKeyboardState;

        /// <summary>
        /// Manager instance that this scene is a child to.
        /// </summary>
        protected SceneManager _manager;

        /// <summary>
        /// Return if this scene is currently playing.
        /// </summary>
        public bool IsPlaying
        {
            get { return _game == null ? false : true; }
        }

        /// <summary>
        /// Scene object used to instantiate and store game component collections.
        /// </summary>
        public BaseScene()
        {
            GameObjects = new List<GameObject>();
            DrawableComponents = new List<DrawableGameComponent>();
            Components = new List<GameComponent>();
        }

        /// <summary>
        /// Loads all scene components in collection and adds them to game components.
        /// </summary>
        /// <param name="game">Game instance components are loaded to.</param>
        public virtual void Start(Game game)
        {
            _manager = game.Services.GetService<SceneManager>();
            if (IsPlaying)
                return;

            _game = game;
            foreach (var go in GameObjects)
            {
                game.Components.Add(go);
            }
            foreach (var gc in Components)
            {
                game.Components.Add(gc);
            }
            foreach (var dc in DrawableComponents)
            {
                game.Components.Add(dc);
            }
        }

        /// <summary>
        /// Removes all components from instantiated game instance and closes scene.
        /// </summary>
        public virtual void End()
        {
            if (!IsPlaying)
                return;

            foreach (var go in GameObjects)
            {
                _game.Components.Remove(go);
            }
            foreach (var gc in Components)
            {
                _game.Components.Remove(gc);
            }
            foreach (var dc in DrawableComponents)
            {
                _game.Components.Remove(dc);
            }

            _game = null;
        }

        /// <summary>
        /// End and clear components of this scene.
        /// </summary>
        public virtual void Restart()
        {
            End();
            GameObjects.Clear();
            Components.Clear();
            DrawableComponents.Clear();
        }

        /// <summary>
        /// Add component to scene collection.
        /// </summary>
        /// <param name="gameObject">Adds game object to collection.</param>
        public void AddComponent(GameObject gameObject)
        {
            GameObjects.Add(gameObject);
        }

        /// <summary>
        /// Add component to scene collection.
        /// </summary>
        /// <param name="gameObject">Adds component to collection.</param>
        public void AddComponent(GameComponent component)
        {
            Components.Add(component);
        }

        /// <summary>
        /// Add component to scene collection.
        /// </summary>
        /// <param name="gameObject">Adds drawable component to collection.</param>
        public void AddComponent(DrawableGameComponent component)
        {
            DrawableComponents.Add(component);
        }

        /// <summary>
        /// Main scene loop for updates to component collections.
        /// </summary>
        /// <param name="gameTime">Instantiated game instace 'GameTime' property.</param>
        public virtual void Update(GameTime gameTime)
        {

        }

        /// <summary>
        /// Check is a key has been pressed a single time.
        /// </summary>
        /// <param name="key">Keyboard key pressed.</param>
        /// <returns></returns>
        protected bool CheckKey(Keys key)
        {
            if (Keyboard.GetState().IsKeyDown(key) && _previousKeyboardState.IsKeyUp(key))
                return true;

            return false;
        }
    }
}
