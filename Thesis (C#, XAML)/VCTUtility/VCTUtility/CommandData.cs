using System;
using System.Collections.Generic;

namespace VCTUtility
{
    [Serializable]
    public class CommandData
    { //Har allt som krävs för att återskapa ett kommand
        public string id; //Fungerar som id?
        public string text, command, tag;
        public List<VoiceData> voiceDataList;

        public CommandData(string id, string text, string command, string tag, List<VoiceData> voiceDataList)
        {
            this.id = id;
            this.text = text;
            this.command = command;
            this.tag = tag;
            this.voiceDataList = voiceDataList;
        }
    }
}
