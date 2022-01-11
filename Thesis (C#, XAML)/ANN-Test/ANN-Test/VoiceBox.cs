using System;
using System.Collections.Generic;
using System.Text;
using VCTUtility;

namespace ANN_Test {
    public class VoiceBox {
        public List<byte[]> bufferList; //Om det endast är recording som ska sparas här kan vi ha det i en byte[] List annars borde vi skapa en strukt eller class
        private int completed, maxRec;
        private string speakerName;
        public bool isExpanded;

        public VoiceBox(int maxRec) {
            this.maxRec = maxRec;
            bufferList = new List<byte[]>();
        }

        public VoiceBox(int maxRec, VoiceData voiceData) { //Needed?
            this.maxRec = maxRec;
            speakerName = voiceData.speakerName;
            bufferList = voiceData.bufferList;
        }

        public void GetSpeaker(string speaker) {
            speakerName = speaker;
        }

        public bool AddRec(byte[] buffer) {
            if (completed < maxRec) {
                bufferList.Add(buffer);
                completed++;

                if (completed >= maxRec) { //Är den full ska den returna false så att Add knappen tas bort
                    return false;
                } else {
                    return true;
                }
            } else {
                System.Diagnostics.Debug.WriteLine("ERROR! Can't add more...");
                return false;
            }
        }

        public byte[] GetBuffer(int bufferNr) {
            return bufferList[bufferNr];
        }

        public bool DeleteRec(int recNr) {
            if (recNr < bufferList.Count) {
                bufferList.RemoveAt(recNr);
                completed--;
                return true;
            } else {
                System.Diagnostics.Debug.WriteLine("ERROR! Can't find rec: " + recNr);
                return false;
            }
        } 

        public void Expand() {
            if (isExpanded) {
                isExpanded = false;
            } else {
                isExpanded = true;
            }
        }

        public void DeExpand() {
            isExpanded = false;
        }

        public int GetOffset() {
            int offset = bufferList.Count * 20 + 15;
            return offset;
        }
    }
}
