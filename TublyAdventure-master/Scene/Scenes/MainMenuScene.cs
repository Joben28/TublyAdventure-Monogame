using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TublyAdventures.UI;

namespace TublyAdventures.Scene
{
    public class MainMenuScene : MenuScene
    {   
        /// <summary>
        /// Main menu scene for this... well... games main menu.
        /// </summary>
        /// <param name="game">Current game instance.</param>
        public MainMenuScene(Game game) : base (game)
        {
            //Initial values
            _itemFontSprite = game.Content.Load<SpriteFont>("UI/Fonts/MainMenuItem");
            Items = new List<MenuItem>();

            LoadOptions(game);
        }

        /// <summary>
        /// Load the menu items in to this menu scene.
        /// </summary>
        /// <param name="game">Current game instance.</param>
        private void LoadOptions(Game game)
        {
            //Create default start options, and then options for map files
            Items.Add(new MenuItem(game, _itemFontSprite, "Start Game") { Execute = StartGame });
            MapSelectionOptions(game);

            //Build the menu for display
            BuildMenu(Items, Game1.Camera.CenterPosition(Items[0].Width / 2, 150));

            //Select first item by default
            Items[0].IsSelected = true;
        }

        /// <summary>
        /// Start the game at 'Map1' map.
        /// </summary>
        private void StartGame()
        {
            _manager.EndAndRemoveScene("MainMenu");
            _manager.AddSceneAndStart("MapScene", new MapScene(_game, "Tiles/BlobTiles/blob_Tiles", "Content/XMLDocuments/blob_Tiles.xml", "Textmaps/map1.txt"));
        }
        /// <summary>
        /// Start a new map scene.
        /// </summary>
        /// <param name="textMapPath">String of text map file path.</param>
        private void Start(string textMapPath)
        {
            _manager.EndAndRemoveScene("MainMenu");
            _manager.AddSceneAndStart("MapScene", new MapScene(_game, "Tiles/BlobTiles/blob_Tiles", "Content/XMLDocuments/blob_Tiles.xml", textMapPath));
        }

        /// <summary>
        /// Searches map directory to create menu options for text map files.
        /// </summary>
        /// <param name="game">Current game instance.</param>
        private void MapSelectionOptions(Game game)
        {
            var files = DirSearch(@"Content/Textmaps");
            foreach (var f in files)
            {
                Items.Add(new MenuItem(game, _itemFontSprite, f) { Execute = delegate { Start(f); } });
            }
        }

        /// <summary>
        /// Search directory for text map files.
        /// </summary>
        /// <param name="directoryPath">Path of directory to search for text map files.</param>
        /// <returns>Collection of text map string paths.</returns>
        private List<String> DirSearch(string directoryPath)
        {
            List<String> files = new List<String>();
            try
            {
                foreach (string f in Directory.GetFiles(directoryPath))
                {
                    if(!f.Contains("Readme"))
                        files.Add(f);
                }
                foreach (string d in Directory.GetDirectories(directoryPath))
                {
                    files.AddRange(DirSearch(d));
                }
            }
            catch (System.Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }

            return files;
        }
    }
}
