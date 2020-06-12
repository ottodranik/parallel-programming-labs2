using System;
using System.Threading;

namespace Lab2App {
  public class MatrixCalculations {

    Matrix A = new Matrix(10);
    Matrix B = new Matrix(10);
    Matrix D = new Matrix(10);
    Matrix E = new Matrix(10);

    Matrix MA = new Matrix(10, 10);
    Matrix MB = new Matrix(10, 10);
    Matrix MC = new Matrix(10, 10);
    Matrix MD = new Matrix(10, 10);
    Matrix ME = new Matrix(10, 10);
    Matrix MM = new Matrix(10, 10);
    Matrix MO = new Matrix(10, 10);
    Matrix MT = new Matrix(10, 10);
    Matrix MX = new Matrix(10, 10);
    Matrix MZ = new Matrix(10, 10);

    private static MatrixCalculations instance = null;
    private static readonly object padlock = new object();

    MatrixCalculations() {
      B.fillRandomValues();
      D.fillRandomValues();
      E.fillRandomValues();

      MB.fillRandomValues();
      MC.fillRandomValues();
      MD.fillRandomValues();
      ME.fillRandomValues();
      MM.fillRandomValues();
      MO.fillRandomValues();
      MT.fillRandomValues();
      MX.fillRandomValues();
      MZ.fillRandomValues();
    }

    public static MatrixCalculations Instance {
      get {
        lock(padlock) {
          if (instance == null) {
            instance = new MatrixCalculations();
          }
          return instance;
        }
      }
    }

    public Matrix f1() {
      return Matrix.add(
        Matrix.multiply(
          Matrix.add(MC, MZ),
          B
        ),
        Matrix.multiply(
          Matrix.subtract(MM, MO),
          E
        )
      );
    }

    public Matrix f2() {
      return Matrix.subtract(
        Matrix.add(
          Matrix.multiply(MB, MO),
          Matrix.multiply(MC, MX)
        ),
        MM
      );
    }

    public Matrix f3() {
      return Matrix.subtract(
        Matrix.multiply(
          MD,
          Matrix.add(MT, MZ)
        ),
        Matrix.multiply(ME, MM)
      );
    }

    public Matrix f4() {
      return Matrix.add(
        Matrix.multiply(MC, B),
        Matrix.multiply(MM, D)
      );
    }
    
  }
}