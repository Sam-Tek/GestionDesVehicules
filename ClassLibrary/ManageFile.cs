using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ClassLibrary
{
    static public class ManageFile<T>
    {
        static public List<T> GetFile(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
                List<T> listvehicle = new List<T>();
                try
                {
                    listvehicle = serializer.Deserialize(fs) as List<T>;
                }
                catch (Exception)
                {
                    Console.WriteLine("Vous avez pas de véhicule\nPas de problème aujourd'hui ça c'est cadeau\n *** 4 nouveaux véhicules *** \n\n");
                }
                return listvehicle;
            }
        }

        static public void SetFile(string path, List<T> data)
        {
            using (FileStream fs = new FileStream(path, FileMode.Truncate, FileAccess.ReadWrite))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
                try
                {
                    serializer.Serialize(fs, data);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
