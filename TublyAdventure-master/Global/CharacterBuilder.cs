using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TublyAdventures.Objects.Animation;

namespace TublyAdventures.Global
{
    public static class CharacterBuilder
    {
        public static AnimationAtlas BuildCharacter(string colorString)
        {
            var animation = new AnimationAtlas();
            var atlas = XMLParser.ParseAtlasFile("Content/XMLDocuments/blob_Characters.xml");
            var textures = new Dictionary<string, SubTexture>();

            foreach (var t in atlas.Textures)
            {
                textures.Add(t.Name, t);
            }

            AnimationSlide right = new AnimationSlide();
            right.AddTextureToSlide(textures[colorString + "_Right1"]);
            right.AddTextureToSlide(textures[colorString + "_Right2"]);
            right.AddTextureToSlide(textures[colorString + "_Right3"]);
            right.DefaultTexture = textures[colorString + "_Right1"];

            AnimationSlide left = new AnimationSlide();
            left.AddTextureToSlide(textures[colorString + "_Left1"]);
            left.AddTextureToSlide(textures[colorString + "_Left2"]);
            left.AddTextureToSlide(textures[colorString + "_Left3"]);
            left.DefaultTexture = textures[colorString + "_Left1"];

            animation.AddSlide("Right", right);
            animation.AddSlide("Left", left);
            animation.CurrentSlide = animation.Slides["Right"];

            return animation;
        }
    }
}
