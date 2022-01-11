using System;

namespace VCTUtility
{
    public class Matrix
    {
        public int Rows { get { return rowCount; } set { rowCount = value; } }
        public int Colums { get { return columnCount; } set { columnCount = value; } }


        private int rowCount;
        private int columnCount;
        private double[][] matrixValues; //prolly rename?
        public Matrix subMatrix;

        public Matrix(int rows, int columns)//Construct an "rows" by "columns" matrix filled with 0.0
        {
            rowCount = rows;
            columnCount = columns;

            if (matrixValues == null)
            {
                matrixValues = new double[rowCount][];

                for (int i = 0; i < matrixValues.GetLength(0); i++)
                {
                    matrixValues[i] = new double[columnCount];
                }
            }
        }

        public Matrix(double[] values, int rows)
        {
            rowCount = rows;
            columnCount = (rowCount != 0 ? values.Length / rows : 0);

            if (values.Length != rows * columnCount) { throw new ArgumentException("Array length must be matrixValues multiple of rows."); }

            matrixValues = new double[rowCount][];

            for (int i = 0; i < rowCount; i++)
            {
                matrixValues[i] = new double[columnCount];

                for (int j = 0; j < columnCount; j++)
                {
                    //System.Diagnostics.Debug.WriteLine("Values: " + values[i + j * rowCount]);
                    matrixValues[i][j] = values[i] * Math.Pow(10, 6);
                }
            }
        }

        public Matrix(double[][] matrixArray, int rows, int columns)
        {
            //if (matrixArray.Length > rows * columns) {
            //    throw new ArgumentException("Invalid matrix array length"); //if we can't match up the rows and colums, break.
            //}

            ////foreach (double[] dArr in matrixArray) {
            ////    System.Diagnostics.Debug.WriteLine("Test: " + dArr.GetLength(0));
            ////}

            //rowCount = rows;
            //columnCount = columns;
            //matrixValues = new float[rows][];

            //for (int i = 0; i < rows; i++) //set sonnematrix to the size of the rows and colums (these will be added /removed depending on what operation is done)
            //{
            //    matrixValues[i] = new float[columns];
            //}

            //// looping through matrix 1 rows  
            //for (int i = 0; i < matrixArray.GetLength(0); i++) {
            //    // for each matrix 1 row, loop through matrix 2 columns  
            //    for (int j = 0; j < matrixValues.GetLength(0); j++) {
            //        // loop through matrix 1 columns to calculate the dot product  
            //        for (int k = 0; k < matrixArray.GetLength(0); k++) {
            //            //System.Diagnostics.Debug.WriteLine("Test: " + matrixArray[i][k]);
            //            matrixValues[i][j] += matrixArray[i][k] * matrixArray[k][i]; //Detta fungerar så länge rows = colums annars blir det problem!
            //        }
            //    }
            //}

            this.matrixValues = matrixArray;
            this.rowCount = rows;
            this.columnCount = columns;
        }

        public Matrix(double[][] matrixArray, int rows)
        { //Kommer hit om colums endast är 1 
            int colums = 1;
            rowCount = rows;
            columnCount = colums;

            matrixValues = new double[rows][];

            for (int i = 0; i < rows; i++)
            {
                matrixValues[i] = new double[colums];
            }

            for (int i = 0; i < rows; i++)
            {
                matrixValues[i][0] = matrixArray[i][0];
            }
        }

        public void SetSubMatrix(int r0, int r1, int c0, int c1) //r0 = row begning, r1 = row en, c0 colum begining, c1 colum end
        {
            //retrun submatrix from sent in index postiotions
            double[][] resultMatrix = new double[r1][];

            for (int i = 0; i < resultMatrix.GetLength(0); i++)
            {
                resultMatrix[i] = new double[columnCount];
            }

            for (int i = 0; i < r1; i++)
            {
                //System.Diagnostics.Debug.WriteLine("Test: " + matrixValues[r0 + i]);
                Array.Copy(matrixValues[r0 + i], c0, resultMatrix[i], 0, 1);
            }

            subMatrix = new Matrix(resultMatrix, r1);
        }

        /// <summary>Get matrixData submatrix.</summary>
		/// <param name="r0">Initial row index</param>
		/// <param name="r1">Final row index</param>
		/// <param name="c0">Initial column index</param>
		/// <param name="c1">Final column index</param>
		/// <returns>matrixData(r0:r1,c0:c1)</returns>
		/// <exception cref="IndexOutOfRangeException">Submatrix indices</exception>
        public void GetMatrix(int r0, int r1, int c0, int c1)
        {
            subMatrix = new Matrix(r1 - r0 + 1, c1 - c0 + 1);
            var b = subMatrix.GetArray();

            try
            {
                for (int i = r0; i <= r1; i++)
                {
                    for (int j = c0; j <= c1; j++)
                    {
                        b[i - r0][j - c0] = matrixValues[i][j];
                    }
                }
            }
            catch (Exception)
            {

                throw new Exception("Submatrix indices");
            }
            subMatrix = new Matrix(b, b.GetLength(0));
        }

        private double[][] CopyArray(double[][] source)
        {
            var len = source.Length;
            var dest = new double[len][];

            for (var x = 0; x < len; x++)
            {
                var inner = source[x];
                var ilen = inner.Length;
                var newer = new double[ilen];
                Array.Copy(inner, newer, ilen);
                dest[x] = newer;
            }

            return dest;
        }

        public void Set(int rows, int columns, double value)
        {
            matrixValues[rows][columns] = value;
        }

        public Matrix Times(double scaler)
        {
            var X = new Matrix(rowCount, columnCount);
            var C = X.GetArray();
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    C[i][j] = scaler * matrixValues[i][j];
                }
            }
            return X;
        }

        /// <summary>Linear algebraic matrix multiplication, matrixValues * B</summary>
		/// <param name="otherMatrix">another matrix</param>
		/// <returns>Matrix product, matrixValues * otherMatrix</returns>
		/// <exception cref="ArgumentException">Matrix inner dimensions must agree.</exception>
        /// Bcolj is the column from the other matrix, Arowi is the row of this matrix
        public Matrix Times(Matrix otherMatrix)
        { //Kommer in som Matrix
            if (otherMatrix.subMatrix.rowCount != columnCount)
                throw new ArgumentException("Matrix inner dimensions must agree.");


            ////var X = new Matrix(otherMatrix.subMatrix.matrixValues, rowCount, otherMatrix.subMatrix.columnCount);
            //var X = otherMatrix.subMatrix; //X är denns subMatrix         
            //var C = otherMatrix.subMatrix.GetArray();

            var X = new Matrix(rowCount, otherMatrix.subMatrix.columnCount);
            var C = X.GetArray();
            var Bcolj = new double[columnCount];

            for (int j = 0; j < otherMatrix.subMatrix.columnCount; j++)
            {

                for (int k = 0; k < columnCount; k++)
                {
                    Bcolj[k] = otherMatrix.subMatrix.matrixValues[k][j];
                }

                for (int i = 0; i < rowCount; i++)
                {
                    var Arowi = matrixValues[i];
                    double s = 0.0f;

                    for (int k = 0; k < columnCount; k++)
                    {
                        s += Arowi[k] * Bcolj[k];
                    }

                    C[i][j] = s;
                }
            }
            X.subMatrix = new Matrix(C, C.Length);
            return X;
        }

        /// <summary>Multiply matrixData matrix by matrixData scalar in place, matrixData = s*matrixData</summary>
		/// <returns>replace matrixData by s*matrixData</returns>
		public Matrix TimesEquals(double scalar)
        {
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    matrixValues[i][j] = scalar * matrixValues[i][j];
                }
            }
            return this;
        }

        /// <summary>
        /// Returns the matrix values in a double jagged array of this matrix
        /// </summary>
        /// <returns></returns>
        public double[][] GetArray()
        {
            return matrixValues;
        }

        /// <summary>Make matrixValues one-dimensional column packed copy of the internal array.</summary>
		/// <returns>Matrix elements packed in matrixValues one-dimensional array by columns.</returns>
		public double[] GetColumnPackedCopy()
        {
            var vals = new double[rowCount * columnCount];
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    vals[i + j * rowCount] = matrixValues[i][j];
                }
            }
            return vals;
        }

        /// <summary>
		/// X.thrunkAtLowerBoundariy(). All values smaller than the given one are set
		/// to this lower boundary.
		/// </summary>
		/// <param name="value">Lower boundary value</param>
		public void ThrunkAtLowerBoundary(double @value)
        {
            for (int i = 0; i < matrixValues.Length; i++)
                for (int j = 0; j < matrixValues[i].Length; j++)
                {
                    if (matrixValues[i][j] < @value)
                        matrixValues[i][j] = @value;
                }
        }

        /// <summary>
		/// X.logEquals() calculates the natural logarithem of each element of the
		/// matrix. The result is stored in this matrix object again.
		/// </summary>
		public void LogEquals()
        {
            for (int i = 0; i < matrixValues.Length; i++)
                for (int j = 0; j < matrixValues[i].Length; j++)
                    matrixValues[i][j] = Math.Log(matrixValues[i][j]);
        }

    }
}
