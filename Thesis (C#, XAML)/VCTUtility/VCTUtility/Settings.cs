using System;

namespace VCTUtility {
    [Serializable]
    public class Settings {

        public static Settings settings;
        #region MFCC
        public int sampleRate = 8000;
        public int windowSize = 512;
        public int nrOfCoef = 20;
        public bool useFirstCoef = true;
        public int minFreq = 20;
        public int maxFreq = 8000;
        public int nrOfFilters = 40;
        #endregion

        #region CreateWindow
        public int voicePCount = 10;
        public int recPCount = 20;
        #endregion

        public Settings() {

        }

        public Settings(int sampleRate, int windowSize, int nrOfCoef, bool useFirstCoef, int minFreq, int maxFreq, int nrOfFilters, int voicePCount, int recPCount) {
            this.sampleRate = sampleRate;
            this.windowSize = windowSize;
            this.nrOfCoef = nrOfCoef;
            this.useFirstCoef = useFirstCoef;
            this.minFreq = minFreq;
            this.maxFreq = maxFreq;
            this.nrOfFilters = nrOfFilters;
            this.voicePCount = voicePCount;
            this.recPCount = recPCount;
        }

        public Settings(Settings settings) {
            sampleRate = settings.sampleRate;
            windowSize = settings.windowSize;
            nrOfCoef = settings.nrOfCoef;
            useFirstCoef = settings.useFirstCoef;
            minFreq = settings.minFreq;
            maxFreq = settings.maxFreq;
            nrOfFilters = settings.nrOfFilters;
            voicePCount = settings.voicePCount;
            recPCount = settings.recPCount;
        }
    }
}
