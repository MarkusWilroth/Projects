using MathNet.Numerics;
using MathNet.Numerics.IntegralTransforms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace VCTUtility {

    public class MarkusMFCC {
        private int sampleRate, windowSize, nFilt;
        private double frameSize, frameStride, lowFreq, highFreq;
        private int frameLength, frameStep;
        private int nfft = 512;
        private double preEmp = 0.97;
        /* 1. Pre Emphasis
         * 2. Framing
         * 3. Hamming Window
         * 4. FFT
         * 5. Filter Banks
        */
        public MarkusMFCC(int sampleRate, int windowSize, double frameSize, double frameStride, double lowFreq, double highFreq, int nfft, int nFilt) {
            this.sampleRate = sampleRate;
            this.windowSize = windowSize;
            this.frameSize = frameSize; //Should be 0.025 (25ms)
            this.frameStride = frameStride; //Should be 0.01
            this.lowFreq = lowFreq;
            this.highFreq = highFreq;
            this.nFilt = nFilt;
            this.nfft = nfft;

            frameLength = (int)(frameSize * sampleRate);
            frameStep = (int)(frameStride * sampleRate);
        }

        private double[,] MagCalc(Complex[,] fft) {
            int row = fft.GetLength(0);
            int colm = fft.GetLength(1);

            double[,] magFrames = new double[row, colm];
            for (int i = 0; i < row; i++) {
                for (int j = 0; j < colm; j++) {
                    magFrames[i, j] = Complex.Abs(fft[i, j]);
                }
            }
            return magFrames;
        }

        private double[,] PowCalc(double[,] magFrames) {
            int row = magFrames.GetLength(0);
            int colm = magFrames.GetLength(1);

            double[,] powFrames = new double[row, colm];
            for (int i = 0; i < row; i++) {
                for (int j = 0; j < colm; j++) {
                    powFrames[i, j] = ((double)1 / nfft) * Math.Pow(magFrames[i, j], 2);
                }
            }
            return powFrames;
        }

        public double[,] GetMFCC(short[] input) {
            input = EmptyRemoval(input);
            float[] preEmpArr = PreEmphasis(input);
            double[,] frames = Framing2(preEmpArr);
            HammingWindow(frames);
            //Complex[,] cfft = FFT.CFFT(frames);
            Complex[,] yfft = FFT.YFFT(frames);
            double[,] magFrames = MagCalc(yfft);
            double[,] powFrames = PowCalc(magFrames);
            double[,] fBank = FilterBanks(powFrames);
            double[,] mfcc = DCT(fBank);
            mfcc = MFitAlg(mfcc);
            mfcc = MCVM(mfcc);
            return mfcc;
        }

        private double[,] MCVM(double[,] mfcc) {
            int row = mfcc.GetLength(0);
            int colm = mfcc.GetLength(1);

            for (int i = 0; i < row; i++) {
                double[] tempArr = new double[colm];
                for (int j = 0; j < colm; j++) {
                    tempArr[j] = mfcc[i, j];
                }
                tempArr = CMVNormalization(tempArr);
                for (int j = 0; j < colm; j++) {
                    mfcc[i, j] = tempArr[j];
                }
            }
            return mfcc;
        }

        private double[,] MFitAlg(double[,] mfcc) {
            int row = mfcc.GetLength(0);
            int colm = mfcc.GetLength(1);
            int targetSize = 4;
            int stepSize = row / targetSize;

            double[,] resArr = new double[targetSize, colm];
            for (int i = 0; i < targetSize; i++) {
                double[] tempArr = new double[colm];
                int iStep = i * stepSize;
                for (int k = iStep; k < iStep + stepSize; k++) {
                    for (int j = 0; j < colm; j++) {
                        tempArr[j] += mfcc[k, j];
                    }
                }

                for (int j = 0; j < tempArr.Length; j++) {
                    tempArr[j] /= stepSize;
                    resArr[i, j] = tempArr[j];
                }
            }


            return resArr;
        }

        private double[,] DCT(double[,] fBank) {
            int row = fBank.GetLength(0);
            int colm = fBank.GetLength(1);
            int mfccColm = 12;
            double[,] resArr = new double[row, mfccColm];

            for (int i = 0; i < row; i++) {
                double[] dctArr = new double[colm];
                for (int j = 0; j < colm; j++) {
                    dctArr[j] = fBank[i, j];
                }
                double[] tempArr = Transform(dctArr, mfccColm);
                for (int k = 0; k < mfccColm; k++) {
                    resArr[i, k] = tempArr[k];
                }
            }
            return resArr;
        }

        public double[] Transform(double[] vector, int resLength) {
            if (vector == null)
                throw new NullReferenceException();
            double[] result = new double[resLength];
            double factor = Math.PI / resLength;
            for (int i = 0; i < resLength; i++) {
                double sum = 0;
                for (int j = 0; j < vector.Length; j++)
                    sum += vector[j] * Math.Cos((j + 0.5) * i * factor);
                result[i] = sum;
            }
            return result;
        }

        /* y(t) = x(t) - preEmp * x(t-1)
         *  - y(t) Emphasized Signal 
         *  - x(t) is the signal at time t
         */
        private float[] PreEmphasis(short[] input) {
            float[] preEmpArr = new float[input.Length];
            int lastIn = 0;
            for (int i = 0; i < input.Length; i++) {
                preEmpArr[i] = (float)(input[i] - preEmp * lastIn);
                lastIn = input[i];
            }
            return preEmpArr;
        }

        private double[,] Framing2(float[] input) {
            int sinLength = input.Length;
            double abs = Math.Abs((double)(sinLength - frameLength) / frameStep);
            double numFrames = Math.Ceiling(abs);
            double padSignLen = numFrames * frameStep + frameLength;
            //double[] z = new double[(int)(padSignLen - sinLength)];
            double[] padSignal = new double[(int)padSignLen];
            for (int i = 0; i < sinLength; i++) {
                padSignal[i] = input[i];
            }
            for (int i = sinLength; i < padSignal.Length; i++) {
                padSignal[i] = 0;
            }
            double[] frameLArr = Arange(0, frameLength);
            double[,] tileOne = Tile(frameLArr, numFrames, 1);
            double[] numStepArr = Arange(0, numFrames * frameStep, frameStep);
            double[,] tileTwo = Tile(numStepArr, frameLength, 1);
            tileTwo = Transpose(tileTwo);
            double[,] indecies = MatrixAddition(tileOne, tileTwo);
            double[,] frames = Slice(padSignal, indecies);
            return frames;
        }
        
        private void HammingWindow(double[,] frames) {
            for (int i = 0; i < frames.GetLength(0); i++) {
                for (int j = 0; j < frames.GetLength(1); j++) {
                    frames[i, j] *= 0.54 - 0.46 * Math.Cos((2 * Math.PI * i) / (frameLength - 1));
                }
            }
        }

        private double[,] FFT2(double[,] frames) {

            return frames;
        }

        private double[,] FilterBanks(double[,] powFrames) {
            lowFreq = 0;
            highFreq = HzToMel(sampleRate / 2);
            double[] melPoints = GetMelPoints();
            double[] hzPoints = new double[melPoints.Length];
            for (int i = 0; i < hzPoints.Length; i++) {
                hzPoints[i] = MelToHz(melPoints[i]);
            }

            double[] arr = new double[hzPoints.Length];
            for (int i = 0; i < arr.Length; i++) {
                //arr[i] = (nfft + 1) * hzPoints[i] / sampleRate;
                arr[i] = 400 * hzPoints[i] / sampleRate;
            }
            double[] bin = FloorArray(arr);
            //double[] bin = arr;
            double[,] fBank = new double[nFilt, 200];
           // double[,] fBank = new double[nFilt, (int)Math.Floor((double)(nfft / 2) + 1)];
            for (int i = 1; i < nFilt + 1; i++) {
                int min = (int)bin[i - 1];
                int cen = (int)bin[i];
                int end = (int)bin[i + 1];

                for (int j = min; j < cen; j++) {
                    fBank[i - 1, j] = (j - bin[i - 1]) / (bin[i] - bin[i - 1]);
                }
                for (int j = cen; j < end; j++) {
                    fBank[i - 1, j] = (bin[i + 1] - j) / (bin[i + 1] - bin[i]);
                }
            }
            double[,] fBankT = Transpose(fBank);
            double[,] filtBank = Dot(powFrames, fBankT);
            float replace = 2.220446049250313e-16f;
            filtBank = Where(0, replace, filtBank);
            filtBank = BankLog(20, filtBank);
            filtBank = MeanFiltBank(filtBank, 0);
            return filtBank;
        }

        private double[,] BankLog(int amount, double[,] arr) {
            for (int i = 0; i < arr.GetLength(0); i++) {
                for (int j = 0; j < arr.GetLength(1); j++) {
                    arr[i, j] = amount * Math.Log10(arr[i, j]);
                }
            }
            return arr;
        }

        private double[,] MeanFiltBank(double[,] arr, int axis) {
            double mean = MeanCalc(arr, axis);
            for (int i = 0; i < arr.GetLength(0); i++) {
                for (int j = 0; j < arr.GetLength(1); j++) {
                    arr[i, j] -= mean + 1e-8f;
                }
            }
            return arr;
        }

        private double MeanCalc(double[,] arr, int axis) {
            double sum = 0;
            foreach (double d in arr) {
                sum += d;
            }
            double mean = sum / arr.Length;
            return mean;
        }


        private double[] GetMelPoints() {
            double[] melPoints = new double[nFilt + 2];
            double step = highFreq / (nFilt + 1);

            for (int i = 0; i < melPoints.Length; i++) {
                melPoints[i] = i * step;
            }
            return melPoints;
        }



        #region Operations
        private double HzToMel(double f) {
            double mel = 2595 * Math.Log10(1 + (f / 700));
            return mel;
        }

        private double MelToHz(double mel) {
            double f = 700 * (Math.Pow(10, (mel / 2595)) - 1);
            return f;
        }
        private double[,] Dot(double[,] A, double[,] B) { //Matrix multiplication?
            int rA = A.GetLength(0);
            int cA = A.GetLength(1);
            int rB = B.GetLength(0);
            int cB = B.GetLength(1);
            double temp = 0;
            double[,] kHasil = new double[rA, cB];
            if (cA != rB) {
                Console.WriteLine("matrik can't be multiplied !!");
                return null;
            }
            else {
                for (int i = 0; i < rA; i++) {
                    for (int j = 0; j < cB; j++) {
                        temp = 0;
                        for (int k = 0; k < cA; k++) {
                            temp += A[i, k] * B[k, j];
                        }
                        kHasil[i, j] = temp;
                    }
                }
                return kHasil;
            }
        }

        private double[,] Where(int condition, float replaceValue, double[,] arr) {
            for (int i = 0; i < arr.GetLength(0); i++) {
                for (int j = 0; j < arr.GetLength(1); j++) {
                    if (arr[i,j] == condition) {
                        arr[i, j] = replaceValue;
                    }
                }
            }
            return arr;
        }

        private double[,] Transpose(double[,] matrix) {
            int w = matrix.GetLength(0);
            int h = matrix.GetLength(1);

            double[,] result = new double[h, w];
            for (int i = 0; i < w; i++) {
                for (int j = 0; j < h; j++) {
                    result[j, i] = matrix[i, j];
                }
            }
            return result;
        }

        private double[] FloorArray(double[] arr) {
            for (int i = 0; i < arr.Length; i++) {
                arr[i] = Math.Floor(arr[i]);
            }
            return arr;
        }


        private void DotArr(double[,] fBank) {

        }
        private static double[,] Slice(double[] arr, double[,] matrix) {
            int ml = matrix.GetLength(0);
            int mr = matrix.GetLength(1);
            double[,] slice = new double[ml, mr];

            for (int i = 0; i < ml; i++) {
                for (int j = 0; j < mr; j++) {
                    slice[i, j] = arr[(int)matrix[i, j]];
                }
            }
            return slice;
        }


        private double[,] MatrixAddition(double[,] m1, double[,] m2) {
            double[,] result = new double[m1.GetLength(0), m1.GetLength(1)];

            for (int i = 0; i < m1.GetLength(0); i++) {
                for (int j = 0; j < m1.GetLength(1); j++) {
                    result[i, j] = m1[i, j] + m2[i, j];
                }
            }
            return result;
        }

        private double[] Arange(int start, int end) {
            int size = end - start;
            double[] arr = new double[size];
            for (int i = 0; i < size; i++) {
                arr[i] = i + start;
            }
            return arr;
        }

        private double[] Arange(int start, double end, int step) {
            //int offset = start % step; //Fel men spelar inen roll om start är 0
            int size = (int)end;
            double[] arr = new double[size];
            for (int i = start; i < size; i++) {
                arr[i] = start + i * step;
            }
            return arr;
        }

        private double[,] Tile(double[] arr, double m, int r) { //r is how many rows, m is how many times
            double[,] tArr = new double[(int)m, arr.Length * (int)r];
            for (int i = 0; i < m; i++) {
                for (int k = 0; k < r; k++) {
                    for (int j = 0; j < tArr.GetLength(1); j++) {
                        tArr[i, j + k * tArr.GetLength(1)] = arr[j];
                    }
                }
            }
            return tArr;
        }
        #endregion
        private double[] CMVNormalization(double[] mfcc) {
            double mean = 0;
            double variance = 0.0f;
            double[] nMfcc = new double[mfcc.Length];
            mfcc[0] = mfcc.Average();
            mean = mfcc.Sum() / mfcc.Count();
            variance = GetVariance(mfcc);

            nMfcc = mfcc.Select(c => c = (c - mean) / variance).ToArray(); //Kan kanske Math abs Här men enligt dokumentationen så ska det ske i NN:en men sigmoid 

            return nMfcc;
        }

        private double GetVariance(double[] values) {
            double sumOfSquares = 0;
            double avg = values.Average();

            var squaresQuery = values.Select(val => val = Math.Pow((val - avg), 2));
            sumOfSquares = squaresQuery.Sum();

            return Math.Sqrt(sumOfSquares / (values.Count() - 1));
        }

        #region RemoveEmpty
        private short[] EmptyRemoval(short[] bufferInput) {
            //int noiseStart = FrontRemoval(bufferInput);
            int noiseStart = 2000;
            int firstEmpty = BackRemoval(bufferInput);

            if (firstEmpty != -1) {
                short[] newInput = new short[firstEmpty - noiseStart];
                for (int i = noiseStart; i < firstEmpty; i++) {
                    newInput[i - noiseStart] = bufferInput[i];
                }
                return newInput;
            }
            return bufferInput;
        }

        private int FrontRemoval(short[] bufferInput) {
            int startPoint = 100; //Kan noise starta innan dess? Hitta en finare algorithm (Vill inte behöva göra undantag för de 6 första "klick" värderna)
            int emptyStartCont = 3;
            int noiseStartCont = 10;
            int noiseCounter = 0;
            int emptyCounter = 0;
            int noiseStart = 0;
            int[] emptyNoises = new int[] { 0, 1, -1, -65535, 65535, -65536, 65536, 131071 };

            for (int i = startPoint; i < bufferInput.Length; i++) {
                bool isEmpty = false;
                for (int j = 0; j < emptyNoises.Length; j++) {
                    if (bufferInput[i] == emptyNoises[j]) {
                        emptyCounter++;
                        isEmpty = true;
                        break;
                    }
                }
                if (!isEmpty) {
                    if (emptyCounter >= emptyStartCont) {
                        emptyCounter = 0;
                        noiseStart = i;
                        noiseCounter = 1; //Börjar om noiseCounter
                    }
                    else {
                        noiseCounter++;
                        if (noiseCounter >= noiseStartCont) {
                            return noiseStart;
                        }
                    }
                }
            }
            return noiseStart;
        }

        private int BackRemoval(short[] bufferInput) {
            int startPoint = bufferInput.Length / 2;
            int counterStart = 100;
            int emptyCounter = 0;
            int firstEmpty = -1;


            for (int i = startPoint; i < bufferInput.Length; i++) {
                if (bufferInput[i] == 0) {
                    if (firstEmpty == -1) {
                        firstEmpty = i;
                    }
                    emptyCounter++;
                    if (emptyCounter >= counterStart) {
                        break;
                    }
                }
                else if (firstEmpty != -1) {
                    firstEmpty = -1;
                    emptyCounter = 0;
                }
            }
            return firstEmpty;
        }
        #endregion

        #region oldCode
        //private double[] Framing(double[] input) {
        //    int frameMassLength = input.Length;
        //    if (frameMassLength % frameLength != 0) { //pads it out with zeros
        //        int mgn = (int)(frameMassLength / frameLength);
        //        int offset = frameMassLength - mgn * frameLength;
        //        int nrToIncrease = frameLength - offset;

        //        double[] newInput = new double[frameMassLength + nrToIncrease];
        //        for (int i = 0; i < frameLength; i++) {
        //            newInput[i] = input[i];
        //        }
        //        for (int i = frameLength; i < newInput.Length; i++) {
        //            newInput[i] = 0;
        //        }
        //        input = newInput;
        //        //fix a new doubleArray that is padded with zeros!
        //    }
        //    double[] frameMass = new double[input.Length]; //Have to make suer input.length is even

        //    for (int i = 0; i < frameMass.Length / frameLength; i++) {
        //        for (int j = 0; j < frameLength; j++) {
        //            frameMass[(i * frameLength) + j] = input[(i * frameStep) + j];
        //        }
        //    }
        //    return frameMass;
        //}
        ///* Hamming Window w(n) = 0.54 - 0.46cos((2*pi*n) / N - 1)
        // *  - N is the windowLength
        // */

        //private void HammingWindow(double[] input) {
        //    for (int i = 0; i < input.Length; i++) {
        //        input[i] *= 0.54 - 0.46 * Math.Cos((2 * Math.PI * i) / (frameLength - 1));
        //    }
        //}
        //private double[] FFT(double[] frameMass) {
        //    double[] fftOut = new double[frameMass.Length]; //output in double
        //    double[] fft = new double[frameMass.Length]; //here is where the results will be stored
        //    Complex[] fftComplex = new Complex[frameMass.Length]; //FFT outputs complexes so we'll capture them with this one

        //    for (int i = 0; i < frameMass.Length; i++) {
        //        fftComplex[i] = new Complex(frameMass[i], 0.0);
        //    }

        //    //FFT TRANSFORM HERE
        //    //FourierTransform.FFT(fftComplex, FourierTransform.Direction.Forward);

        //    for (int i = 0; i < fftComplex.GetLength(0); i++) {
        //        if (fftComplex[i].IsNaN())
        //            if (i - 1 < 0)
        //                fftComplex[i] = fftComplex[i + 1];
        //            else
        //                fftComplex[i] = fftComplex[i - 1];
        //    }

        //    Fourier.Forward(fftComplex, FourierOptions.Matlab);

        //    for (int i = 0; i < fftComplex.GetLength(0); i++) {
        //        fft[i] = fftComplex[i].Magnitude; //fix conversion to float 
        //    }

        //    for (int i = 0; i < fft.Length; i++) {
        //        fftOut[i] = Convert.ToSingle(fft[i]);
        //    }

        //    return fftOut;
        //}
        #endregion
    }
}
