using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TublyAdventures.UI;

namespace TublyAdventures.Scene
{
    public class MenuScene : BaseScene
    {
        /// <summary>
        /// Game instance this scene in running within.
        /// </summary>
        protected Game _game;

        /// <summary>
        /// Menu item collection.
        /// </summary>
        public List<MenuItem> Items { get; set; }

        /// <summary>
        /// The index of the selected item in collection.
        /// </summary>
        public int SelectedIndex { get; set; }

        /// <summary>
        /// Sprite font of this menus items.
        /// </summary>
        protected SpriteFont _itemFontSprite;

        /// <summary>
        /// Base menu scene for managing item collections.
        /// </summary>
        /// <param name="game">Current game instance.</param>
        public MenuScene(Game game)
        {
            _game = game;

            //This is here to prevent prior scene keystrokes on this new scene
            _previousKeyboardState = Keyboard.GetState();
        }

        /// <summary>
        /// Build the items in collection to their positions
        /// </summary>
        /// <param name="items">Collection of menu items.</param>
        /// <param name="position">Position this item build starts.</param>
        public void BuildMenu(List<MenuItem> items, Vector2 position)
        {
            var height = items[0].Height;
            int index = 0;
            foreach (var i in items)
            {
                i.Position = new Vector2(position.X, position.Y + (height * index));
                index++;
                AddComponent(i);
            }

        }

        /// <summary>
        /// Update method for this menu scene.
        /// </summary>
        /// <param name="gameTime">Current game instance game time.</param>
        public override void Update(GameTime gameTime)
        {
            CheckInput();
            _previousKeyboardState = Keyboard.GetState();
            base.Update(gameTime);
        }

        /// <summary>
        /// Checks the keyboard inputs this scene recognizes.
        /// </summary>
        private void CheckInput()
        {
            //Move item selection up 1 index
            if (CheckKey(Keys.Down))
            {
                if (SelectedIndex < Items.Count - 1)
                {
                    Items[SelectedIndex].IsSelected = false;
                    SelectedIndex++;
                    Items[SelectedIndex].IsSelected = true;
                }
            }

            //Move item selection down 1 index
            if (CheckKey(Keys.Up))
            {
                if (SelectedIndex > 0)
                {
                    Items[SelectedIndex].IsSelected = false;
                    SelectedIndex--;
                    Items[SelectedIndex].IsSelected = true;
                }
            }

            //Selected item has been invoked
            if (CheckKey(Keys.Enter))
            {
                Items[SelectedIndex].Execute.Invoke();
            }
        }
    }
}
