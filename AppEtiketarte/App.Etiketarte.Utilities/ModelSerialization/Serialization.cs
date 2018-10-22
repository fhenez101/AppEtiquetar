using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace App.Etiketarte.Utilities.ModelSerialization
{
    public class Serialization
    {
        public static string Serialize<T>(T obj)
        {
            var ms = new MemoryStream();
            new DataContractJsonSerializer(obj.GetType()).WriteObject(ms, obj);
            return Encoding.UTF8.GetString(ms.ToArray());
        }

        public static T Deserialize<T>(string json) where T : new()
        {
            var obj = new T();
            var ms = new MemoryStream(Encoding.Unicode.GetBytes(json));
            obj = (T)new DataContractJsonSerializer(obj.GetType()).ReadObject(ms);
            ms.Close();
            return obj;
        }
    }
}