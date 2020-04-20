using System;
using System.Threading;

namespace Lab2App {
  public class MatrixCalculations {

    Matrix A = new Matrix(888);
    Matrix B = new Matrix(888);
    Matrix D = new Matrix(888);
    Matrix E = new Matrix(888);

    Matrix MA = new Matrix(888, 888);
    Matrix MB = new Matrix(888, 888);
    Matrix MC = new Matrix(888, 888);
    Matrix MD = new Matrix(888, 888);
    Matrix ME = new Matrix(888, 888);
    Matrix MM = new Matrix(888, 888);
    Matrix MO = new Matrix(888, 888);
    Matrix MT = new Matrix(888, 888);
    Matrix MX = new Matrix(888, 888);
    Matrix MZ = new Matrix(888, 888);

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