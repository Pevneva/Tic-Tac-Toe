using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Scrips.DataSaving
{
    public static class SaveManager
    {
        public static void Save<T>(string key, T data)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/" + key + ".dat");
            bf.Serialize(file, data);
            file.Close();
        }

        public static T Load<T>(string key) where T : new()
        {
            if (File.Exists(Application.persistentDataPath + "/" + key + ".dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/" + key + ".dat", FileMode.Open);
                T data = (T) bf.Deserialize(file);
                file.Close();
                return data;
            }

            return new T();
        }
        
        public static void ResetData(string key) //todo
        {
            if (File.Exists(Application.persistentDataPath + "/" + key + ".dat"))
                File.Delete(Application.persistentDataPath + "/" + key + ".dat");
        }
    }
}