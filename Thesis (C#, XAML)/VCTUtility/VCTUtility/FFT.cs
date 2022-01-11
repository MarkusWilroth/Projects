using System;
using System.Numerics;
using System.Linq;
using System.Diagnostics;

namespace VCTUtility {
    public static class FFT {

        /* rfft(a, n=None, axis=-1, norm=None):
    a : array_like
        Input array
    n : int, optional
        Number of points along transformation axis in the input to use.
        If `n` is smaller than the length of the input, the input is cropped.
        If it is larger, the input is padded with zeros. If `n` is not given,
        the length of the input along the axis specified by `axis` is used.
    axis : int, optional
        Axis over which to compute the FFT. If not given, the last axis is
        used.
    norm : {"backward", "ortho", "forward"}, optional

     out : complex ndarray
 */
        /* a = asarray(a)
            if n is None:
                n = a.shape[axis]
            inv_norm = _get_forward_norm(n, norm)
            output = _raw_fft(a, n, axis, True, True, inv_norm)
            return output
         */
        public static Complex[,] RFFT(double[,] arr, int n, int axis, string norm) {
            if (n == 0) {

            }
            int invNorm = GetForwardNorm(n, norm);
            return RawFFT(arr, n, axis, true, true, invNorm);
        }

        

        private static int GetForwardNorm(int n, string norm) {
            if (n < 1) {
                //Raise error!
            }

            if (n == 0 || norm == "backward") {
                return 1;
            }
            else if (norm == "ortho") {
                return ((int)Math.Sqrt(n));
            }
            else {
                return n;
            }
        }

        public static Complex[,] CFFT(double[,] samples) {
            int row = samples.GetLength(0);
            int colm = samples.GetLength(1);
            Complex[,] resSamp = new Complex[row, colm];
            for (int i = 0; i < row; i++) {
                Complex[] cSamp = new Complex[colm];
                for (int j = 0; j < colm; j++) {
                    cSamp[j] = samples[i,j];
                }

               cSamp = RecFFT(cSamp);
                for (int j = 0; j < colm; j++) {
                    resSamp[i, j] = cSamp[j];
                } 
            }
            return resSamp;
        }

        private static Complex[] RecFFT(Complex[] cSamp) {
            
            int N = cSamp.Length;

            if (N == 1) {
                return cSamp;
            }

            int M = N / 2;
            Complex[] xEven = new Complex[M];
            Complex[] xOdd = new Complex[M];
            for (int i = 0; i != M; i++) {
                xEven[i] = cSamp[2 * i];
                xOdd[i] = cSamp[2 * i + 1];
            }
            Complex[] fEven = RecFFT(xEven);
            Complex[] fOdd = RecFFT(xOdd);

            //End Recursion
            int n = N / 2;
            Complex[] freqBins = new Complex[n*2];
            for (int k = 0; k != n; k++) {
                Complex cmplx = Complex.FromPolarCoordinates(1, -2 * Math.PI * k / N) * fOdd[k];
                freqBins[k] = fEven[k] + cmplx;
                freqBins[k + M] = fEven[k] - cmplx;
            }
            return freqBins;
        }

        private static Complex[,] RawFFT(double[,] arr, int n, int axis, bool isReal, bool isForward, int invNorm) {
            int ndim = 2;
            axis = NormalizeAxis(axis, ndim); //Arrayen har alltid här två dimensioner

            double fct = 1 / invNorm;

            if (axis == ndim - 1) {
                Complex[,] retArr = PfiExe(arr, isReal, isForward, fct);
                return retArr;
            } else {
                arr = SwapAxes(arr, axis, -1);                              //a = swapaxes(a, axis, -1)
                Complex[,] retArr = PfiExe(arr, isReal, isForward, fct);    //r = pfi.execute(a, is_real, is_forward, fct)
                retArr = SwapAxes(retArr, axis, -1);                        //r = swapaxes(r, axis, -1)
                return retArr;
            }
        }

        private static double[,] SwapAxes(double[,] arr, int axis, int x) {
            return null;
        }

        private static Complex[,] SwapAxes(Complex[,] cArr, int axis, int x) {
            return null;
        }

        private static Complex[,] PfiExe(double[,] arr, bool isReal, bool isForward, double fct) {
            return null;
        }

        private static int NormalizeAxis(int axis, int ndim) { //Vet inte vad som ska hända här men om axis är -1 och ndim = 2 så ska det vara 1 som lämnas tillbaka!
            if (axis == -1 && ndim == 2) {
                return 1;
            } else {
                System.Diagnostics.Debug.WriteLine("NormalizeAxis error! Axis: " + axis + " ndim: " + ndim);
                return 1;
            }
            
        }

        private static int Shape(double[,] arr, int axis) {
            return -1;
        }

        public static int BitReverse(int n, int bits) {
            int reversedN = n;
            int count = bits - 1;

            n >>= 1; //Vad händer här?
            while(n>0) {
                reversedN = (reversedN << 1) | (n & 1); //vad händer här?
                count--;
                n >>= 1;
            }
            return ((reversedN << count) & ((1 << bits) - 1)); //Vad gör <<?
        }

        public static Complex[,] YFFT(double[,] samples) {
            int row = samples.GetLength(0);
            int colm = samples.GetLength(1);
            Complex[,] resSamp = new Complex[row, colm];
            for (int i = 0; i < row; i++) {
                Complex[] cSamp = new Complex[colm];
                for (int j = 0; j < colm; j++) {
                    cSamp[j] = samples[i, j];
                }

                cSamp = MidGround(cSamp);
                for (int j = 0; j < colm; j++) {
                    resSamp[i, j] = cSamp[j];
                }
            }
            return resSamp;
        }

        private static Complex[] XFFT(Complex[] buffer) {
            //if false
            int bits = (int)Math.Log(buffer.Length, 2);

            //for (int i = 0; i < buffer.Length / 2; i++) {
            //    int swapPos = BitReverse(i, bits);
            //    var temp = buffer[i];
            //    buffer[i] = buffer[swapPos];
            //    buffer[swapPos] = temp;
            //}
            //else
            for (int i = 1; i < buffer.Length; i++) {
                int swapPos = BitReverse(i, bits);
                if (swapPos > i) {
                    var temp = buffer[i];
                    buffer[i] = buffer[swapPos];
                    buffer[swapPos] = temp;
                }
            }

            for (int N = 2; N <= buffer.Length; N <<=1) {
                for (int i = 0; i < buffer.Length; i += N) {
                    for (int k = 0; k < N / 2; k++) {

                        int evenIndex = i + k;
                        int oddIndex = i + k + (N / 2);
                        var even = buffer[evenIndex];
                        var odd = buffer[oddIndex];

                        double term = -2 * Math.PI * k / (double)N;
                        Complex exp = new Complex(Math.Cos(term), Math.Sin(term)) * odd;

                        buffer[evenIndex] = even + exp;
                        buffer[oddIndex] = even - exp;
                    }
                }
            }
            return buffer;
        }

        private static Complex[] MidGround(Complex[] arr) {
            DCFFT(arr);
            return arr;
        }

        public static void DCFFT(Complex[] arr) {
            int n = arr.Length;
            if (n <= 1) {
                return;
            }
            //Divide
            Complex[] even = Slice(arr, 0, n / 2, 2);
            Complex[] odd = Slice(arr, 1, n / 2, 2);

            //Conquer
            DCFFT(even);
            DCFFT(odd);

            for (int k = 0; k < n/2; ++k) {
                double term = -2 * Math.PI * k / (double)n;
                Complex t = new Complex(Math.Cos(term), Math.Sin(term)) * odd[k];

                arr[k] = even[k] + t;
                arr[k + n / 2] = even[k] - t;
            }
        }

        private static Complex[] Slice(Complex[] arr, int start, int size, int stride) {
            Complex[] newArr = new Complex[size];
            for (int i = 0; i < size; i++) {
                newArr[i] = arr[start + stride * i];
            }
            return newArr;
        }
    }
}
/*def _raw_fft(a, n, axis, is_real, is_forward, inv_norm):
    axis = normalize_axis_index(axis, a.ndim)
    if n is None:
        n = a.shape[axis]

    fct = 1/inv_norm

    if a.shape[axis] != n:
        s = list(a.shape)
        index = [slice(None)]*len(s)
        if s[axis] > n:
            index[axis] = slice(0, n)
            a = a[tuple(index)]
        else:
            index[axis] = slice(0, s[axis])
            s[axis] = n
            z = zeros(s, a.dtype.char)
            z[tuple(index)] = a
            a = z

    if axis == a.ndim-1:
        r = pfi.execute(a, is_real, is_forward, fct)
    else:
        a = swapaxes(a, axis, -1)
        r = pfi.execute(a, is_real, is_forward, fct)
        r = swapaxes(r, axis, -1)
    return r
 */ 