using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace TublyAdventures.Global
{
    public class XMLParser
    {
        public static TextureAtlas ParseAtlasFile(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(TextureAtlas));
            TextureAtlas atlas = null;
            using (XmlReader reader = XmlReader.Create(new StringReader(File.ReadAllText(@filePath))))
            {
                atlas = (TextureAtlas)serializer.Deserialize(reader);
            }
            return atlas;
        }
    }
}
