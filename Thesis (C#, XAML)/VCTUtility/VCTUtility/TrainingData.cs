using System;
using System.Collections.Generic;

namespace VCTUtility
{
    public class TrainingData
    {
        public string tag;
        private int attributeCount = 20; //Få sitt värde från annat håll? Detta blir hur många InputNodes som finns
        private int count = 0;
        private bool increase = true;
        private double min = -1, max = 1;
        private double tMin, tMax;
        public double[] dAtt;

        public Dictionary<string, double> attributes; //Göra om de till double då desesa inte är mindre än 1?

        public TrainingData(string tag, double[,] coef) {
            this.tag = tag;
            dAtt = new double[coef.Length];
            for (int i = 0; i < coef.GetLength(0); i++) {
                for (int j = 0; j < coef.GetLength(1); j++) {
                    dAtt[i * coef.GetLength(1) + j] = coef[i, j];
                }
            }
        }
        public TrainingData(string tag, double[] coef)
        { //Typically, input values are normalized so as to fall between 0.0 and 1.0 (Använda Sigmoind funktionen på inputs?)
            this.tag = tag;
            //double min = double.PositiveInfinity;
            //double max = double.NegativeInfinity;
            dAtt = coef;
            attributes = new Dictionary<string, double>();
            //System.Diagnostics.Debug.WriteLine("----- " + tag + " -----");
            foreach (double co in coef)
            {
                //if (co != coef[0]) {
                //    
                //}
                if (co <= tMin) {
                    tMin = co;
                }
                else if (co >= tMax) {
                    tMax = co;
                }

                if (count < attributeCount)
                {
                    //attributes.Add("IN-" + count.ToString(), ReLU(co));
                    //attributes.Add("IN-" + count.ToString(), MarkusConvert(co));
                    double inCo = co;
                    //inCo = MarkusConvert(co);
                    //if (inCo > 1) {
                    //    inCo = 1;
                    //} else if (inCo < -1) {
                    //    inCo = -1;
                    //}
                    attributes.Add("IN-" + count.ToString(), inCo);
                    //System.Diagnostics.Debug.WriteLine(inCo);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Need more attributes!");
                    break;
                }
                count++;
            }
            //System.Diagnostics.Debug.WriteLine("min: " + tMin + " max: " + tMax);
        }

        private double ReLU(double x) {
            return Math.Max(0, x);// x < 0 ? 0 : x;
        }

        private double MarkusConvert(double coef) {
            //double stepSize = 0.02;
            double midStep = 0.0083;
            double largeStep = 0.04;
            double oSet = 0;
            int count = 0;
            for (int i = 0; i < 100; i++) {
                double stepSize = midStep;
                if (i <= 20) {
                    stepSize = largeStep;
                    count = i;
                    oSet = 0;
                } else if (i > 80) {
                    stepSize = largeStep;
                    oSet = 60 * midStep;
                    count = i - 60;
                } else {
                    stepSize = midStep;
                    oSet = 20 * largeStep;
                    count = i - 20;
                }
                if (coef <= min + oSet + (count * stepSize)) {
                    double d = (double)i / 100;
                    double co = d;
                    return co;
                }
            }
            return 1;
        }

        private double MarkusCoTwo(double co) {
            co += 0.6f;
            if (co > 1) {
                co = 1;
            } else if (co < 0) {
                co = 0;
            }
            return co;
        }
    }
}
