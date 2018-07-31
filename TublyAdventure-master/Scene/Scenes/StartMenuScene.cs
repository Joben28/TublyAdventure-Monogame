using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TublyAdventures.UI;

namespace TublyAdventures.Scene
{
    public class StartMenuScene : MenuScene
    {
        /// <summary>
        /// Start menu scene for players when in a loaded map scene.
        /// </summary>
        /// <param name="game">Current game instance.</param>
        public StartMenuScene(Game game) : base(game)
        {
            _itemFontSprite = game.Content.Load<SpriteFont>("UI/Fonts/MainMenuItem");
            LoadMenuItems(game);
        }

        /// <summary>
        /// Load the menu item collection to window.
        /// </summary>
        /// <param name="game">Current game instance.</param>
        private void LoadMenuItems(Game game)
        {
            Items = new List<MenuItem>()
            {
                new MenuItem(game, _itemFontSprite, "Return to Main Menu") { Execute = ReturnToMainMenu },
                new MenuItem(game, _itemFontSprite, "Restart") { Execute = RestartGame },
                new MenuItem(game, _itemFontSprite, "Item 3") {  },
            };

            BuildMenu(Items, Vector2.Zero);

            Items[0].IsSelected = true;
        }

        /// <summary>
        /// Restart this current game (Assumed to be in a 'MapScene')
        /// </summary>
        private void RestartGame()
        {
            End();
            _manager.Restart("MapScene");
        }

        /// <summary>
        /// Return the the main menu scene.
        /// </summary>
        private void ReturnToMainMenu()
        {
            _manager.EndAndRemoveScene("MapScene");
            _manager.AddSceneAndStart("MainMenu", new MainMenuScene(_game));
        }
    }
}
