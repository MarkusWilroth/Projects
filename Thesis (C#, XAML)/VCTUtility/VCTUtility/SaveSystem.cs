using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace VCTUtility {
    public static class SaveSystem {

        public static void Save(string path, List<CommandData> cDataList, Network network) {
            //NetworkData nData = new NetworkData(null, null, null, null, Settings.settings);
            //if (network != null) {
            //    nData = new NetworkData(network.inLayers, network.hLayerOne, network.hLayerTwo, network.outLayers, Settings.settings);
            //    SaveANN(path + "-ANN", nData);
            //}

            //SaveData sData = new SaveData(cDataList, nData);

            //if (path != null) {
            //    BinaryFormatter formatter = new BinaryFormatter();
            //    FileStream stream = new FileStream(path, FileMode.Create);
            //    formatter.Serialize(stream, sData);
            //    stream.Close();
            //}
        }

        private static void SaveANN(string path, NetworkData nData) {
            if (path != null) {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Create);
                formatter.Serialize(stream, nData);
                stream.Close();
            }
        }

        public static SaveData Load(string path) {
            SaveData sData = null;

            if (File.Exists(path)) {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                if (stream.Length != 0) {
                    sData = formatter.Deserialize(stream) as SaveData;
                }

                stream.Close();
                return sData;
            }
            else {
                Console.WriteLine("Voice file not found in " + path);
                return null;
            }
        }

        public static NetworkData LoadANN(string path) {
            NetworkData nData = null;

            if (File.Exists(path)) {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                if (stream.Length != 0) {
                    nData = formatter.Deserialize(stream) as NetworkData;
                }

                stream.Close();
                return nData;
            }
            else {
                Console.WriteLine("Network file not found in " + path);
                return null;
            }
        }
    }
}
