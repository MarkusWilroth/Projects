using System;

namespace VCTUtility {
    [Serializable]
    public class NetworkData {
        public Unit[] inputLayers, hiddenLayerOne, hiddenLayerTwo, outputLayers;
        public Settings settings;

        public NetworkData(Unit[] inputLayers, Unit[] hiddenLayerOne, Unit[] hiddenLayerTwo, Unit[] outputLayers, Settings settings) {
            this.inputLayers = inputLayers;
            this.hiddenLayerOne = hiddenLayerOne;
            this.hiddenLayerTwo = hiddenLayerTwo;
            this.outputLayers = outputLayers;

            this.settings = settings;
        }
    }
}
