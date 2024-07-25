using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using GameContent.Services.SaveLoadService.Abstract;
using UnityEngine;

namespace GameContent.Services.SaveLoadService.BinarySaveLoadSystem
{
    public class BinarySaveLoadSystem : ISaveLoadSystem
    {
        private readonly string _filePath;
        private readonly BinaryFormatter _binaryFormatter;

        public BinarySaveLoadSystem()
        {
            _filePath = Application.persistentDataPath + "/SaveCommand.dat";
            _binaryFormatter = GetFormatter();
        }
        
        public void Save(SaveData data)
        {
            using (var fileStream = File.Create(_filePath))
            {
                _binaryFormatter.Serialize(fileStream, data);
            }
        }

        public SaveData Load()
        {
            SaveData saveData;

            using (var fileStream = File.Open(_filePath, FileMode.OpenOrCreate))
            {
                var loadedData = _binaryFormatter.Deserialize(fileStream);
                saveData = (SaveData)loadedData;
            }

            return saveData;
        }

        private BinaryFormatter GetFormatter()
        {
            var formatter = new BinaryFormatter();
            var surrogateSelector = new SurrogateSelector();
            
            surrogateSelector.AddSurrogate(typeof(Vector3), new StreamingContext(StreamingContextStates.All),
                new Vector3Serializer());
            surrogateSelector.AddSurrogate(typeof(Quaternion), new StreamingContext(StreamingContextStates.All),
                new QuaternionSerializer());

            formatter.SurrogateSelector = surrogateSelector;
            return formatter;
        }
    }
}