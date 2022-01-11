using System.Diagnostics;
using System;
using System.Collections;
using System.Drawing;
using Microsoft.VisualBasic;
using System.Data;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace VCTUtility {
    class Recorder {
        [DllImport("winmm.dll", EntryPoint = "mciSendStringA", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int record(string lpstrCommand, string lpstrReturnString, int uReturnLength, int hwndCallback);

        int sampleRate;
        public Recorder(int sampelRate) {
            this.sampleRate = sampleRate;
        }

        public void StartRec() {
            //timer1.Enabled = true;
            //timer1.Start();
            record("open new Type waveaudio Alias recsound", "", 0, 0);
            record("record recsound", "", 0, 0);
        }

        public void StopRec() {
            //timer1.Stop();
            //timer1.Enabled = false;
            record("save recsound d:\\mic.wav", "", 0, 0);
            record("close recsound", "", 0, 0);
        }

        public void Play() {
            
        }
    }


}
