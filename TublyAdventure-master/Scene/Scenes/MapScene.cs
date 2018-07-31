using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TublyAdventures.Global;
using TublyAdventures.UI;

namespace TublyAdventures.Scene
{
    public class MapScene : BaseScene
    {
        /// <summary>
        /// TextureAtlas for GameObjects used in scene.
        /// </summary>
        private TextureAtlas _textureAtlas;

        /// <summary>
        /// Sprite sheet for GameObject's in scene.
        /// </summary>
        private Texture2D _texture;

        /// <summary>
        /// Dictionary to hold key identifiable GameObjects loaded from atlas.
        /// </summary>
        private Dictionary <string, SubTexture> TextureDictionary;

        /// <summary>
        /// Player instance in this playable scene.
        /// </summary>
        private Player Player;

        /// <summary>
        /// Path of this loaded map in directory.
        /// </summary>
        private string _mapPath;

        private float _gravity = 0.4f;

        /// <summary>
        /// Base scene object for playable maps.
        /// </summary>
        /// <param name="game">Current game instance.</param>
        /// <param name="spriteSheet">String path to sprite sheet.</param>
        /// <param name="spriteXml">String path to XML document for atlas.</param>
        public MapScene(Game game, string spriteSheet, string spriteXml, string mapPath)
        {
            //New dictionary that holds sub-textures
            TextureDictionary = new Dictionary<string, SubTexture>();
            //Parse texture atlas from XML string
            _textureAtlas = XMLParser.ParseAtlasFile(spriteXml);
            //Load image that holds these textures
            _texture = game.Content.Load<Texture2D>(spriteSheet);

            //Add all sub-textures in atlas to a dictionary for later reference
            foreach(var t in _textureAtlas.Textures)
            {
                TextureDictionary.Add(t.Name, t);
            }

            _mapPath = mapPath;

            LoadMap(game, mapPath);
        }
        /// <summary>
        /// Main scene loop for updates to component collections.
        /// </summary>
        /// <param name="gameTime">Instantiated game instace 'GameTime' property.</param>
        public override void Update(GameTime gameTime)
        {
            if (CheckKey(Keys.P))
            {
                _manager.StartScene("StartMenu");
            }

            if (Player.IsDead)
            {
                _manager.Restart("MapScene");
            }

            UpdateMapObjects();
        }

        /// <summary>
        /// Update the objects in this map scene with neighbor checking and applying gravity.
        /// </summary>
        private void UpdateMapObjects()
        {
            //Loop all game objects on the map
            foreach (var mapObject in GameObjects.ToList())
            {
                //Apply gravity to object
                if (!mapObject.IsStatic)
                    mapObject.ApplyForce(0, _gravity);

                //Check this object against all other game objects nearby
                foreach (var familyObject in GameObjects.Where(x => x.Boundary.Intersects(mapObject.NeighborBoundary)).ToList())
                {
                    //Dont check this object against itself
                    if (familyObject == mapObject)
                        continue;

                    //If this object no longer is a game component, e.g. has been disposed, then remove it from collection
                    if (!_manager.Game.Components.Contains(familyObject))
                        GameObjects.Remove(familyObject);

                    //Check is this family object is a valid neighbor of the map object
                    mapObject.ValidateAsNeighbor(familyObject);
                }
            }
        }

        /// <summary>
        /// Start this playable scene.
        /// </summary>
        /// <param name="game">Current game instance.</param>
        public override void Start(Game game)
        {
            //Base start must be called first, so scene '_manager' is set
            base.Start(game);
            //Add start menu scene to this map scene
            _manager.AddScene("StartMenu", new StartMenuScene(game));
        }

        /// <summary>
        /// End this map scene.
        /// </summary>
        public override void End()
        {
            //Stop and remove start menu scene before ending
            _manager.EndAndRemoveScene("StartMenu");
            base.End();
        }

        /// <summary>
        /// Restart this playable scene. This reloads all components -- including the player.
        /// </summary>
        public override void Restart()
        {
            //Base restart must be called first, so components can be removed before loading new ones.
            base.Restart();
            //Load new map objects and start scene
            LoadMap(_manager.Game, _mapPath);
            _manager.StartScene("MapScene");
        }

        /// <summary>
        /// Loads the components to this playable scene, and loads a new player instance.
        /// </summary>
        /// <param name="game">Current game instance.</param>
        /// <param name="mapPath">String path of the map being read from directory.</param>
        private void LoadMap(Game game, string mapPath)
        {
            //Create new player object and add to scene components
            Player = new Player(game);
            GameObjects.Add(Player);

            //Default position is (1,1) --> (64, 64)
            var yCount = 1;
            var xCount = 1;

            MapBuilder builder = new MapBuilder(game, _texture, TextureDictionary);

            //Reads text map file line by line
            using (StreamReader rdr = new StreamReader(File.OpenRead(mapPath)))
            {
                while(rdr.Peek() > 0)
                {
                    var row = rdr.ReadLine();

                    //Foreach character in the read line, build the game object it represents (See 'MapBuilder.cs')
                    foreach (var c in row.ToCharArray())
                    {
                        var gameObject = builder.BuildGameObject(c);
                        if(gameObject != null)
                        {
                            //Each character represents it's location times 64 (Since tiles are assumed 64x64)
                            gameObject.X = 64 * xCount;
                            gameObject.Y = 64 * yCount;
                            GameObjects.Add(gameObject);
                        }
                        xCount++;
                    }
                    xCount = 1;
                    yCount++;
                }
            }
        }
    }
}
