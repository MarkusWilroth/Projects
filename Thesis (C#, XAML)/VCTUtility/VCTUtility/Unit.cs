using System;

namespace VCTUtility {
    [Serializable]
    public class Unit { //The Unit that exists in a layer

        public float bias; //The Unit bias
        public float uIn, uOut; //The connected in and out unit
        public float err;
        public float[] connectedWeights; //The connected Weights from preLayer
        public string tag = "-";

        private double min = -0.5, max = 0.5; //Range for the random

        public Unit(int preLayers) {
            Random rand = new Random();
            //bias = (float)(min + (rand.NextDouble() * (max - min)));
            bias = 0;

            if (preLayers > 0) { //Checks if it is a input unit
                connectedWeights = new float[preLayers];

                for (int i = 0; i < connectedWeights.Length; i++) {
                    rand = new Random();
                    connectedWeights[i] = (float)(min + (rand.NextDouble() * (max - min)));
                }
            }
        }

        public void GetTag(string tag) {
            this.tag = tag;
        }
    }
}
