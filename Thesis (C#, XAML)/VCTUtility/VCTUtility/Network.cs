using System;
using System.Collections.Generic;

namespace VCTUtility {
    public class Network {
        private int min = -1, max = 1; // Ska min vara 0?
        private int inNr, hOneNr, outNr; //How many units per layer
        private int hTwoNr;
        private float lRate = 0.001f; //Learning rate (mellan 0-1) - Thumb rule l = 1 / t where t is the number of iterations through the training set so far
        public Unit[] inLayers, hLayerOne, outLayers; //The layers that holds the units
        public Unit[] hLayerTwo;
        private int terminCount = 10000; //Hur många gånger den ska köra innan den känner sig klar
        private int batchSize, maxBSize = 60, minBSize = 1; //Stroleken på batch

        public Network(List<string> tags, DataTuple dTuple) {
            SetLayerSize(dTuple.trainingList[0].dAtt.Length, tags.Count); //Sets the size of the layers (how many nodes per layer)
            SpawnNodes(tags); //Creates the nodes with the weights and biases!

            batchSize = dTuple.trainingList.Count / 100; //Får fram stroleken på varje batch
            if (batchSize > maxBSize) {
                batchSize = maxBSize;
            }
            else if (batchSize < minBSize) {
                batchSize = minBSize;
            }

            CreateNetwork(dTuple);
        }

        private void SetLayerSize(int inNr, int outNr) {
            this.inNr = inNr; //In borde alltid vara 20 om inte MFCC ändras (Ändras MFCC ändras denna automatiskt!)
            this.outNr = outNr; 
            
            hOneNr = inNr / 3 * 2 + outNr; //Kanske inte ska vara samma algorithm för både första och andra?
            if (hOneNr >= inNr * 2) { //Hidden layer borde inte vara dubbelt så stor som input layer
                hOneNr = inNr * 2 - 1;
            }

            hTwoNr = hOneNr / 3 * 2 + outNr;
            if (hTwoNr >= hOneNr * 2) {
                hTwoNr = hOneNr * 2 - 1;
            }
        }

        private void SpawnNodes(List<string> tags) { //Sets the weights and biases
            inLayers = new Unit[inNr];
            hLayerOne = new Unit[hOneNr];
            hLayerTwo = new Unit[hTwoNr];
            outLayers = new Unit[outNr];

            for (int i = 0; i < inNr; i++) {
                inLayers[i] = new Unit(0);
            }
            for (int i = 0; i < hOneNr; i++) {
                hLayerOne[i] = new Unit(inNr);
            }

            for (int i = 0; i < hTwoNr; i++) {
                hLayerTwo[i] = new Unit(hOneNr);
            }
            for (int i = 0; i < outNr; i++) {
                outLayers[i] = new Unit(hTwoNr);
                outLayers[i].GetTag(tags[i]);
            }
        }

        public Network(NetworkData nData) {
            inLayers = nData.inputLayers;
            hLayerOne = nData.hiddenLayerOne;
            hLayerTwo = nData.hiddenLayerTwo;
            outLayers = nData.outputLayers;
        }

        private void CreateNetwork(DataTuple dTuple) {
            Console.WriteLine("Creating the network...");
            //InisializeLayers(); //Initialize all weights and biases in network
            System.Diagnostics.Debug.WriteLine("Layers Inisialized...");
            bool isDone = false;
            int count = 1;
            int procent = terminCount / 100;
            int currentProcent = procent;
            int progress = 0;
            int batchCount = 0;

            while (!isDone) {
                dTuple.trainingList = SuffleData(dTuple.trainingList);
                foreach (TrainingData tData in dTuple.trainingList) {
                    SetInValue(tData.dAtt);

                    PrograteForward(0); //Forward propagation (Gets the result) - 0 is the acc in this stage it is not important!
                    BackPropagate(tData.tag); //Backpropagation (Sets the error)
                    UpdateNetwork(); //Updates the weights and biases

                    //batchCount++;
                    //if (batchCount >= 1) { //Ska vara batchSize när den väl fungerar istället för 1 men något är fel...
                    //    batchCount = 0;
                    //    UpdateNetwork();
                    //}
                }
                //if (batchCount > 0) { //Går in här ifall Det inte har uppdaterat nätverket för de senaste träningsdatan
                //    batchCount = 0;
                //    UpdateNetwork();
                //}

                if (count >= terminCount) {
                    isDone = true;
                    //System.Diagnostics.Debug.WriteLine("Progress: 100%");
                }
                else {
                    count++;
                    //lRate = (float)1 / (((float)10 + count) / 10);
                    //lRate = (float)1 / count;
                    //if (lRate < 0.001f) {
                    //    lRate = 0.001f;
                    //}

                    if (count >= currentProcent) {
                        currentProcent += procent;
                        progress++;
                        if (progress % 5 == 0) {
                            System.Diagnostics.Debug.WriteLine("Progress: " + progress + "%");
                        }
                    }
                }
            }
            System.Diagnostics.Debug.WriteLine("Network Creation completed!");
        }

        public string GetResult(Dictionary<string, double> attributes, double acc) { //Input här ska vara attributes (ska finnas en inputUnit för varje attribute)
            SetInValue(attributes); //Skickar in attributes
            PrograteForward(0);
            string result = GetMove(0);
            return result;
        }

        public List<TrainingData> SuffleData(List<TrainingData> dataList) {
            Random rnd = new Random();
            int n = dataList.Count;
            while (n > 1) {
                n--;
                int k = rnd.Next(n + 1);
                TrainingData value = dataList[k];
                dataList[k] = dataList[n];
                dataList[n] = value;
            }
            return dataList;
        }

        private void SetInValue(double[] dAtt) {
            for (int i = 0; i < dAtt.Length; i++) {
                inLayers[i].uIn = (float)dAtt[i];
            }
        }

        private void SetInValue(Dictionary<string, double> attributes) { //Får in attributes - Byta namn till något mer passande
            int counter = 0;
            foreach (float value in attributes.Values) {
                inLayers[counter].uIn = value;
                counter++;
            }
        }

        //private void InisializeLayers() { //Create 
        //    foreach (Unit unit in inLayers) {
        //        Random rand = new Random();
        //        unit.uIn = (float)(min + rand.NextDouble() * (max - min));
        //        //Unit annResult = PrograteForward();
        //    }
        //}

        private void PrograteForward(double acc) {
            //Forward propate
            foreach (Unit unit in inLayers) { //Sets the output in the inputLayers - This don't change so is not needed to be called all the time?
                unit.uOut = unit.uIn;
            }
            //Sigmoid(inputLayers, hiddenLayerOne);
            //Sigmoid(hiddenLayerOne, hiddenLayerTwo);
            //Sigmoid(hiddenLayerTwo, outputLayers); //Output
            ReLU(inLayers, hLayerOne);
            ReLU(hLayerOne, hLayerTwo);
            //LeakyReLU(inLayers, hLayerOne);
            //LeakyReLU(hLayerOne, hLayerTwo);
            SoftMax(hLayerTwo, outLayers);
        }

        private void SetInput(Unit[] uIArr, Unit[] uJArr) { //compute the net input of unit uj with respect to the previous layer, ui
            foreach (Unit uJ in uJArr) {
                float sum = 0; //Ij =  Sum i (wij * Oi) + bJ
                for (int i = 0; i < uJ.connectedWeights.Length; i++) { //går igenom alla wij
                    sum += uIArr[i].uOut * uJ.connectedWeights[i];
                }
                //if (sum > 1000) {
                //    System.Diagnostics.Debug.WriteLine("Hmmm.. sum = " + sum);
                //}
                uJ.uIn = sum + uJ.bias;
            }
        }

        private void Sigmoid(Unit[] preLayer, Unit[] layer) { //Old, don't use! 
            SetInput(preLayer, layer);
            foreach (Unit unit in layer) {
                unit.uOut = (float)(1 / (1 + Math.Exp(-unit.uIn))); //Sigmoid - f(x) = 1 / (1 + e^-x)
            }
        }

        private float DerSigmoid(float x) {
            float result = x * (1 - x);
            return result;
        }

        private void ReLU(Unit[] uIArr, Unit[] uJArr) { //For hidden layer - f(x) = max(x,0)
            SetInput(uIArr, uJArr); //Sets the input to each node in the layer using the output from preLayer and the weights and biases
            foreach (Unit unit in uJArr) {
                unit.uOut = Math.Max(0, unit.uIn);
            }
        }

        private float DerReLu(float x) {
            float result = 0;
            if (x > 0) {
                result = 1;
            }
            return result;
        }

        private void LeakyReLU(Unit[] preLayer, Unit[] layer) {
            SetInput(preLayer, layer); //Sets the input to each node in the layer using the output from preLayer and the weights and biases
            foreach (Unit unit in layer) {
                unit.uOut = Math.Max(0.1f * unit.uIn, unit.uIn);
            }
        }

        private float DerLeakyReLU(float x) {
            float result = 0.1f;
            if (x > 0) {
                result = 1;
            }
            return result;
        }

        private void SoftMax(Unit[] uIArr, Unit[] uJArr) { //softmax = e^zi / Sum(e^zk)
            SetInput(uIArr, uJArr);
            //double[] expValues = new double[uJArr.Length];

            double maxIn = 0; //Gets the biggest node
            foreach (Unit unit in uJArr) {
                maxIn = Math.Max(maxIn, unit.uIn);
            }

            double sum = 0;
            for (int i = 0; i < uJArr.Length; i++) {
                //if (uJArr[i].uIn > 400 || uJArr[i].uIn < -400) {
                //    System.Diagnostics.Debug.WriteLine("soft in: " + sum);
                //}
                sum += Math.Exp(uJArr[i].uIn);
            }
            foreach (Unit uj in uJArr) {
                uj.uOut = (float)(Math.Exp(uj.uIn) / sum);
            }

            //double sum = 0;
            //for (int i = 0; i < uJArr.Length; i++) {
            //    //double expValue = Math.Exp(layer[i].uIn - maxIn);
            //    //expValues[i] = expValue;
            //    sum += Math.Exp(uJArr[i].uIn - maxIn);
            //}
            //double scale = 0;
            //scale = Math.Exp()

            //double baseSum = 0;
            //foreach (double dValue in expValues) {
            //    baseSum += dValue;
            //}

            //double con = maxIn + Math.Log(sum);
            //for (int i = 0; i < uJArr.Length; i++) {
            //    uJArr[i].uOut = (float)Math.Exp(uJArr[i].uIn - con);
            //}

            //for (int i = 0; i < layer.Length; i++) {
            //    layer[i].uOut = ((float)expValues[i] / (float)baseSum);
            //}
        }

        private float DerSoftmax(float resultP) { //For the highest predicted outputNode, the first or the target?
            float dP = resultP * (1 - resultP);
            return dP;
        }

        private float DerSoftmax (float predictA, float predictB) { //For all the others
            float dP = -predictA * predictB;
            return dP;
        }


        private void BackPropagate(string targetTag) {
            SoftmaxErrorCalc(outLayers, targetTag);
            ReLUErrorCalc(hLayerTwo, outLayers);
            ReLUErrorCalc(hLayerOne, hLayerTwo);
        }

        private void SoftmaxErrorCalc(Unit[] unitArr, string targetTag) {
            float predictA = 0;
            float dA;
            float predictB;

            string result = GetMove(0);
            ////Sets the predictA with the target value
            //foreach (Unit unit in unitArr) {
            //    if (unit.tag == targetTag) {
            //        predictA = unit.uOut;
            //        dA = DerSoftmax(predictA);
            //        float tOj = 1 - unit.uOut;
            //        unit.err = dA * tOj;
            //        break;
            //    }
            //}

            foreach (Unit unit in unitArr) {
                predictB = unit.uOut;
                dA = DerSoftmax(unit.uOut);
                float targetValue = 0;
                if (targetTag == unit.tag) {
                    targetValue = 1;
                    if (result == targetTag) {
                        System.Diagnostics.Debug.WriteLine("Success!");
                    } else {
                        System.Diagnostics.Debug.WriteLine("Failure! Target: " + targetTag + " Result: " + result);
                    }
                }
                float tOj = targetValue - unit.uOut;
                unit.err = dA * tOj;
            }
        }

        private void ReLUErrorCalc(Unit[] uJArr, Unit[] uKArr) { //uKArr is the next higher layer
            for (int i = 0; i < uJArr.Length; i++) { //Errj = DA * Summan av errk * wjk
                float sum = 0;
                foreach (Unit uK in uKArr) {
                    sum += uK.connectedWeights[i] * uK.err;
                }
                float uOut = uJArr[i].uOut;
                float dA = DerReLu(uOut);
                uJArr[i].err = dA * sum;
                //if (double.IsNaN(hlayerTwo[i].err) || double.IsInfinity(hlayerTwo[i].err)) {
                //    System.Diagnostics.Debug.WriteLine("REEE");
                //}
                //hiddenLayerTwo[i].err += DerSigmoid(uOut) * sum; //For Sigmoid
            }
        }

        private void ResetError() {
            //foreach (Unit unit in inputLayers) {
            //    unit.err = 0;
            //}
            foreach (Unit unit in hLayerOne) {
                unit.err = 0;
            }
            //foreach (Unit unit in hLayerTwo) {
            //    unit.err = 0;
            //}
            foreach (Unit unit in outLayers) {
                unit.err = 0;
            }
        }

        private void UpdateNetwork() {
            //Goes through all weights in network
            foreach (Unit unit in outLayers) {
                WeightUpdate(unit, hLayerTwo);
            }
            foreach (Unit unit in hLayerTwo) {
                WeightUpdate(unit, hLayerOne);
            }
            foreach (Unit unit in hLayerOne) {
                WeightUpdate(unit, inLayers);
            }
            //Goes through all the biases in network
            foreach (Unit unit in outLayers) {
                BiasUpdate(unit);
            }

            foreach (Unit unit in hLayerTwo) {
                BiasUpdate(unit);
            }
            foreach (Unit unit in hLayerOne) {
                BiasUpdate(unit);
            }
            //ResetError();
            //foreach (Unit unit in inputLayers) { //Ska den ändras? Input units har ingen error value?
            //    BiasUpdate(unit);
            //}
        }

        private void WeightUpdate(Unit uJ, Unit[] uIArr) { //Ui och Uj - wij är weight från i till j så i är preLayer och j är Layer
            for (int i = 0; i < uJ.connectedWeights.Length; i++) {
                //float dWeight = lRate * unitI.err * unitJArr[i].uOut; //Weight increment - Change err to be the higher connected (l * err(j) * O(i))
                float dWeight = lRate * uJ.err * uIArr[i].uOut; //Ska vara uJ.err * wij (Som också finns i uJ...)
                uJ.connectedWeights[i] += dWeight; //Weight update
                if (double.IsNaN(uJ.connectedWeights[i]) || double.IsInfinity(uJ.connectedWeights[i])) {
                    System.Diagnostics.Debug.WriteLine("REEE");
                }
            }
        }

        private void ReLUWeightUpdate(Unit uJ) {
            for (int i = 0; i < uJ.connectedWeights.Length; i++) {
                //float dWeight = lRate * unitI.err * unitJArr[i].uOut; //Weight increment - Change err to be the higher connected (l * err(j) * O(i))
                float newWeight = uJ.connectedWeights[i] - lRate * uJ.err;
                uJ.connectedWeights[i] = newWeight; //Weight update
                if (double.IsNaN(uJ.connectedWeights[i]) || double.IsInfinity(uJ.connectedWeights[i])) {
                    System.Diagnostics.Debug.WriteLine("REEE");
                }
                //if (unitI.connectedWeights[i] < 0) {
                //    unitI.connectedWeights[i] = 0;
                //}
            }
        }

        private void BiasUpdate(Unit uJ) { //Kommer in som Uj
            float dBias = lRate * uJ.err; //Bias increment
            uJ.bias += dBias; //Bias update
            //if (unit.bias < 0) {
            //    unit.bias = 0;
            //}
        }

        private string GetMove(double acc) {
            //int resultNr = 0;
            string result = null;
            double closest = double.MinValue;
            //if (!isVal) {
            //    accLimit = 0;
            //} else {
            //    accLimit = 0.8f;
            //}

            for (int i = 0; i < outLayers.Length; i++) {
                if (outLayers[i].uOut > closest) {
                    //resultNr = i;
                    closest = outLayers[i].uOut;
                    result = outLayers[i].tag;
                }
            }
            if (closest < acc) { //If the best result is not accuret
                result = null; //byt till null? Lättar att använda...
            }
            return result;
        }

        private Unit CreateUnit(int prelayers) { //Delete! Move bias to Unit - Onödi!
            Unit unit = new Unit(prelayers);
            return unit;
        }
    }
}