using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TublyAdventures.UI;

namespace TublyAdventures.Scene
{
    public class SceneManager : GameComponent
    {
        /// <summary>
        /// Collection of 'Scene' objects stored in this instance.
        /// </summary>
        public Dictionary<string, BaseScene> GameScenes { get; set; }

        /// <summary>
        /// Scenes currently running in scene manager.
        /// </summary>
        public Dictionary<string, BaseScene> OpenScenes { get; set; }

        /// <summary>
        /// Manages the game scenes displayed to the user.
        /// </summary>
        /// <param name="game">Current game instance.</param>
        public SceneManager(Game game) : base(game)
        {
            game.Components.Add(this);
            GameScenes = new Dictionary<string, BaseScene>();
            OpenScenes = new Dictionary<string, BaseScene>();
            Game.Services.AddService(this);
        }

        /// <summary>
        /// Add a new scene to the manager collection.
        /// </summary>
        /// <param name="sceneKey">String key used to identify new scene.</param>
        /// <param name="scene">Scene object to be stored in collection.</param>
        public void AddScene(string sceneKey, BaseScene scene)
        {
            GameScenes.Add(sceneKey, scene);
        }

        public void Restart(string sceneKey)
        {
            OpenScenes.Remove(sceneKey);
            GameScenes[sceneKey].Restart();
        }

        /// <summary>
        /// Start a scene in the collection.
        /// </summary>
        /// <param name="sceneKey">Key to identify scene in collection.</param>
        public void StartScene(string sceneKey)
        {
            //GameScenes has a collection item with this key
            if (GameScenes.ContainsKey(sceneKey))
            {
                //A scene currently running does NOT contain this key
                if (!OpenScenes.ContainsKey(sceneKey))
                {
                    var scene = GameScenes[sceneKey];
                    scene.Start(Game);
                    OpenScenes.Add(sceneKey, scene);
                }
            }
        }

        /// <summary>
        /// End the current scene being displayed and managed.
        /// </summary>
        public void EndScene(string sceneKey)
        {
            OpenScenes.Remove(sceneKey);
            GameScenes[sceneKey].End();
        }

        /// <summary>
        /// Add a new scene to the manager collection.
        /// </summary>
        /// <param name="sceneKey">String key used to identify new scene.</param>
        /// <param name="scene">Scene object to be stored in collection.</param>
        public void AddSceneAndStart(string sceneKey, BaseScene scene)
        {
            GameScenes.Add(sceneKey, scene);
            StartScene(sceneKey);
        }

        /// <summary>
        /// End the speciified scene and remove it from collection.
        /// </summary>
        public void EndAndRemoveScene(string sceneKey)
        {
            EndScene(sceneKey);
                GameScenes.Remove(sceneKey);
        }

        /// <summary>
        /// Component update override.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            foreach (var s in OpenScenes.Values.ToList())
            {
                s.Update(gameTime);
            }
            base.Update(gameTime);
        }
    }
}
