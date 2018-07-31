using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TublyAdventures.Objects.Entities;
using TublyAdventures.Objects.Tiles;

namespace TublyAdventures.Global
{
    public class MapBuilder
    {
        public Dictionary<char, string> CharToObjectDictionary;
        public Dictionary<string, SubTexture> TextureDictionary;
        private Game _game;
        private Texture2D _texture;

        public MapBuilder(Game game, Texture2D spriteSheet, Dictionary<string, SubTexture> textureDictionary)
        {
            TextureDictionary = textureDictionary;
            _game = game;
            _texture = spriteSheet;

            CharToObjectDictionary = new Dictionary<char, string>()
            {
                { 'G', "WaveGrass_Square" },
                { 'M', "WaveGrass_MudSquare" },
                { 'B', "WaveCandY_Round" },


            };
        }

        public GameObject BuildGameObject(char key)
        {
            if (!CharToObjectDictionary.ContainsKey(key))
            {
                if(key == 'E')
                {
                    return new Monster(_game);
                }
                return null;
            }

            var textureKey = CharToObjectDictionary[key];
            var subtexture = TextureDictionary[textureKey];

            if(key == 'B')
            {
                return new BouncyTile(_game, _texture, subtexture.SourceBoundary);
            }

            return new Tile(_game, _texture, subtexture.SourceBoundary);
        }
    }
}
