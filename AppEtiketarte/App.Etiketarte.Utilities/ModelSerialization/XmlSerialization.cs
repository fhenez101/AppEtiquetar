using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace App.Etiketarte.Utilities.ModelSerialization
{
    public class XmlSerialization
    {
        public static string XmlToList(string xmlPath)
        {
            var list = new List<string>();

            if (File.Exists(xmlPath))
            {
                using (var reader = new XmlTextReader(xmlPath))
                {
                    while (reader.Read())
                    {
                        switch (reader.NodeType)
                        {
                            case XmlNodeType.Text:
                                list.Add(reader.Value);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }

            return string.Join(",", list);
        }
    }
}
