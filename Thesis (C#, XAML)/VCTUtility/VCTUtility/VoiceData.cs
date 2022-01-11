using System;
using System.Collections.Generic;

namespace VCTUtility
{
    [Serializable]
    public class VoiceData
    {
        public string speakerName;
        public List<byte[]> bufferList;

        public VoiceData(string speakerName, List<byte[]> bufferList)
        {
            this.speakerName = speakerName;
            this.bufferList = bufferList;
        }
    }
}
