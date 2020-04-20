using System;
using System.Collections;

/**
 * Example - https://java.mazurok.com/?p=2509
 */
namespace Lab2App {
  public class Matrix {

    int n, m;
    int[,] mainMatrix;

    public Matrix(int n, int m) {
      this.n = n;
      this.m = m;
      this.mainMatrix = new int[this.n, this.m];
    }

    public Matrix(int n) {
      this.n = n;
      this.m = 1;
      this.mainMatrix = new int[this.n, 1];
    }

    public Matrix(int[,] paramMatrix) {
      this.n = paramMatrix.GetLength(0);
      this.m = paramMatrix.GetLength(1);
      this.mainMatrix = paramMatrix;
    }

    public Matrix(int[] vector) {
      this.n = vector.Length;
      this.m = 1;
      this.mainMatrix = new int[1, this.n];
      for (int i = 0; i < this.n; i++) {
        this.mainMatrix[0, i] = vector[i];
      }
      this.mainMatrix = transpone(this.mainMatrix);
    }

    public int getElement(int n, int m) {
      return this.mainMatrix[n, m];
    }

    public int[] getElement(int n) {
      int[] tmpArr = new int[this.mainMatrix.GetLength(1)];
      for (int i = 0; i < tmpArr.Length; i++) {
        tmpArr[i] = this.mainMatrix[n, i];
      }
      return tmpArr;
    }

    public void setElement(int value, int n, int m) {
      this.mainMatrix[n, m] = value;
    }

    public void setElement(int[] value, int n) {
      for (int i = 0; i < this.mainMatrix.GetLength(1); i++) {
        this.mainMatrix[n, i] = value[i];
      }
    }

    public int getVerticalLength() {
      return this.mainMatrix.GetLength(0);
    }

    public int getHorizontalLength() {
      return this.mainMatrix.GetLength(1);
    }

    public void fillRandomValues() {
      Random rand = new Random();

      for (int i = 0; i < this.n; i++) {
        for (int j = 0; j < this.m; j++) {
          this.mainMatrix[i, j] = rand.Next(100);
        }
      }
    }

    public void displayMatrix() {
      for (int i = 0; i < this.n; i++) {
        for (int j = 0; j < this.m; j++) {
          Console.Write(this.mainMatrix[i, j] + " ");
        }
        Console.WriteLine();
      }
    }

    public static int[,] transpone(int[,] paramMatrix) {
      int[,] tmpMatrix = new int[paramMatrix.GetLength(1), paramMatrix.GetLength(0)];
      for (int i = 0; i < paramMatrix.GetLength(1); i++) {
        for (int j = 0; j < paramMatrix.GetLength(0); j++) {
          tmpMatrix[i, j] = paramMatrix[j, i];
        }
      }
      return tmpMatrix;
    }

    public static Matrix transpone(Matrix paramMatrix) {
      Matrix tmpMatrix = new Matrix(paramMatrix.m, paramMatrix.n);
      for (int i = 0; i < paramMatrix.m; i++) {
        for (int j = 0; j < paramMatrix.n; j++) {
          tmpMatrix.setElement(paramMatrix.getElement(j, i), i, j);
        }
      }
      return tmpMatrix;
    }

    public static Matrix add(Matrix first, Matrix second) {
      if (
        first.getVerticalLength() != second.getVerticalLength()
        || first.getHorizontalLength() != second.getHorizontalLength()
      ) {
        throw new NotEqualLengthsOfMatrixException();
      }
      Matrix tmpMatrix = new Matrix(first.getVerticalLength(), second.getHorizontalLength());
      for (int i = 0; i < tmpMatrix.getVerticalLength(); i++) {
        for (int j = 0; j < tmpMatrix.getHorizontalLength(); j++) {
          tmpMatrix.setElement(first.getElement(i, j) + second.getElement(i, j), i, j);
        }
      }
      return tmpMatrix;
    }

    public static Matrix subtract(Matrix first, Matrix second) {
      if (
        first.getVerticalLength() != second.getVerticalLength()
        || first.getHorizontalLength() != second.getHorizontalLength()
      ) {
        throw new NotEqualLengthsOfMatrixException();
      }
      Matrix tmpMatrix = new Matrix(first.getVerticalLength(), second.getHorizontalLength());
      for (int i = 0; i < tmpMatrix.getVerticalLength(); i++) {
        for (int j = 0; j < tmpMatrix.getHorizontalLength(); j++) {
          tmpMatrix.setElement(first.getElement(i, j) - second.getElement(i, j), i, j);
        }
      }
      return tmpMatrix;
    }

    public static Matrix multiply(Matrix first, Matrix second) {
      if (first.getHorizontalLength() != second.getVerticalLength()) {
        throw new NotEqualLengthsOfMatrixException();
      }
      Matrix tmpMatrix;
      int n = first.getVerticalLength();
      int m = second.getHorizontalLength();
      int o = second.getVerticalLength();
      int[,] tmpArr = new int[n, m];
      for (int i = 0; i < n; i++) {
        for (int j = 0; j < m; j++) {
          for (int k = 0; k < o; k++) {
            tmpArr[i, j] += first.getElement(i, k) * second.getElement(k, j);
          }
        }
      }
      tmpMatrix = new Matrix(tmpArr);
      return tmpMatrix;
    }

    public static Matrix multiplyOnValue(Matrix matrix, int value) {
      Matrix tmpMatrix;
      int n = matrix.getVerticalLength();
      int m = matrix.getHorizontalLength();
      int[,] tmpArr = new int[n, m];
      for (int i = 0; i < n; i++) {
        for (int j = 0; j < m; j++) {
          tmpArr[i, j] = matrix.getElement(i, j) * value;
        }
      }
      tmpMatrix = new Matrix(tmpArr);
      return tmpMatrix;
    }

    // Function to sort each row of the matrix
    static Matrix sortByRow(Matrix matrix, int n, int m) {
      int[] tmpArray = new int[m];
      Matrix tmpMatrix = new Matrix(n, m);
      for (int i = 0; i < n; i++) {
        // sorting row number 'i'
        Array.Copy(matrix.getElement(i), tmpArray, tmpArray.Length);
        Array.Sort(tmpArray);
        tmpMatrix.setElement(tmpArray, i);
      }
      return tmpMatrix;
    }

    // Function to sort the matrix row-wise and column-wise
    public static Matrix sort(Matrix matrix) {
      // sort rows of mat[][]
      int n = matrix.getVerticalLength();
      int m = matrix.getHorizontalLength();
      Matrix tmpMatrix = sortByRow(matrix, n, m);

      // get transpose of mat[][]
      tmpMatrix = transpone(tmpMatrix);

      // again sort rows of mat[][]
      tmpMatrix = sortByRow(tmpMatrix, m, n);

      // again get transpose of mat[][]
      tmpMatrix = transpone(tmpMatrix);

      // generate Matrix class
      return tmpMatrix;
    }

    // Function to find cross product of two vector array
    public static Matrix vectorCrossProductMatrix(Matrix first, Matrix second) {
      if (
        first.getVerticalLength() != second.getVerticalLength()
        || first.getHorizontalLength() != second.getHorizontalLength()
      ) {
        throw new NotEqualLengthsOfMatrixException();
      }
      int[] vectorA = transpone(first).getElement(0);
      int[] vectorB = transpone(second).getElement(0);
      Matrix tmpMatrix = new Matrix(vectorA.Length, 1);
      int[,] crossProductVector = new int[1, vectorA.Length];

      crossProductVector[0,0] = vectorA[1] * vectorB[2] - vectorA[2] * vectorB[1];
      crossProductVector[0,1] = -(vectorA[0] * vectorB[2] - vectorA[2] * vectorB[0]);
      crossProductVector[0,2] = vectorA[0] * vectorB[1] - vectorA[1] * vectorB[0];
      crossProductVector = transpone(crossProductVector);

      // generate Matrix class
      tmpMatrix = new Matrix(crossProductVector);
      return tmpMatrix;
    }

    // Function that return dot product of two vector array
    public static int vectorDotProduct(Matrix first, Matrix second) {
      if (
        first.getVerticalLength() != second.getVerticalLength()
        || first.getHorizontalLength() != second.getHorizontalLength()
      ) {
        throw new NotEqualLengthsOfMatrixException();
      }
      int[] vectorA = transpone(first).getElement(0);
      int[] vectorB = transpone(second).getElement(0);

      int product = 0;

      // Loop for calculate dot product
      for (int i = 0; i < vectorA.Length; i++) {
        product = product + vectorA[i] * vectorB[i];
      }
      return product;
    }

  }
}