using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TublyAdventures
{
    /// <summary>
    /// Atlas which holds sub-texture models for animation sprite sheets.
    /// </summary>
    [XmlRoot("TextureAtlas")]
    public class TextureAtlas
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }

        public List<SubTexture> Textures { get; set; }
    }
}
