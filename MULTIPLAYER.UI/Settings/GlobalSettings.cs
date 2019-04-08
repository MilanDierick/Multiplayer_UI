using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.XPath;
using JetBrains.Annotations;
using MULTIPLAYER.UI.Attributes;
using MULTIPLAYER.UI.Extensions;

// TODO: Implement 'TryLoadSettings'

namespace MULTIPLAYER.UI.Settings
{
    [PublicAPI]
    public class GlobalSettings
    {
        [CanBeNull] [Setting] public string Setting1 { get; set; }
        [CanBeNull] [Setting] public string Setting2 { get; set; }
        [CanBeNull] [Setting] public string Setting3 { get; set; }
        [CanBeNull] [Setting] public string Setting4 { get; set; }
        [CanBeNull] [Setting] public string Setting5 { get; set; }

        private readonly string _fileName = "GlobalSettings.xml";

        /// <summary>
        /// Default constructor that initializes database settings
        /// </summary>
        [PublicAPI]
        public GlobalSettings()
        {
            try
            {
                LoadSettingsFromXmlFile();
            }
            catch (FileNotFoundException exception)
            {
                Console.WriteLine(exception);

                Setting1 = " ";
                Setting2 = " ";
                Setting3 = " ";
                Setting4 = " ";
                Setting5 = " ";

                SaveSettingsToXmlFile();
            }
        }

        [PublicAPI]
        public void LoadSettingsFromXmlFile()
        {
            XDocument tmp = XDocument.Load(Environment.CurrentDirectory + @"\" + _fileName);
            XPathNavigator xPathNavigator = tmp.CreateNavigator();

            foreach (PropertyInfo propertyInfo in GetType().GetProperties())
            {
                string tmp2 = GetStringValues(xPathNavigator, "//GlobalSettings/" + propertyInfo.Name);
                if (Attribute.IsDefined(propertyInfo, typeof(SettingAttribute)))
                {
                    propertyInfo.SetValue(this, tmp2, null);
                }
            }
        }

        [PublicAPI]
        public void SaveSettingsToXmlFile()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(GetType());

            this.TrimStringProperties();

            using (TextWriter textWriter = new StreamWriter(Environment.CurrentDirectory + @"\" + _fileName))
            {
                xmlSerializer.Serialize(textWriter, this);
            }
        }

        private static string GetStringValues(XPathNavigator navigator, string xpath)
        {
            StringBuilder sb = new StringBuilder();
            XPathNodeIterator nodesIterator = navigator.Select(xpath);
            while (nodesIterator.MoveNext())
                sb.Append($"{nodesIterator.Current.Value}");
            return sb.ToString();
        }
    }
}