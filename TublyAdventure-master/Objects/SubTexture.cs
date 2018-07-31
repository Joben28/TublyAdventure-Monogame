using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TublyAdventures
{
    /// <summary>
    /// Object model for a texture location and size in a sprite sheet.
    /// </summary>
    public class SubTexture
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlAttribute("X")]
        public int X { get; set; }

        [XmlAttribute("Y")]
        public int Y { get; set; }

        [XmlAttribute("Width")]
        public int Width { get; set; }

        [XmlAttribute("Height")]
        public int Height { get; set; }

        [XmlIgnore]
        public Rectangle SourceBoundary
        {
            get
            {
                return new Rectangle(X, Y, Width, Height);
            }
        }
    }
}
