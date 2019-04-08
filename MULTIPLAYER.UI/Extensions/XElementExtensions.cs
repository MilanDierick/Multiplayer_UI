using System.Xml;
using System.Xml.Linq;
using JetBrains.Annotations;

namespace MULTIPLAYER.UI.Extensions
{
    [PublicAPI]
    public static class XElementExtension
    {
        public static string InnerXml(this XElement element)
        {
            XmlReader reader = element.CreateReader();
            reader.MoveToContent();
            return reader.ReadInnerXml();
        }
    }
}