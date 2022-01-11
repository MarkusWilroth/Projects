using System;
using System.Collections.Generic;

namespace VCTUtility
{
    [Serializable]
    public class SaveData
    {

        public List<CommandData> cDataList;
        public NetworkData savedNetwork;

        public SaveData(List<CommandData> commandDataList, NetworkData savedNetwork)
        {
            this.cDataList = commandDataList;
            this.savedNetwork = savedNetwork;
        }
    }
}
