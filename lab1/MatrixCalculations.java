package lab1;

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

  private static MatrixCalculations SINGLE_INSTANCE = null;

  private MatrixCalculations() {
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

  public static MatrixCalculations getInstance() {
    if (SINGLE_INSTANCE == null) {  
      synchronized(MatrixCalculations.class) {
        SINGLE_INSTANCE = new MatrixCalculations();
      }
    }
    return SINGLE_INSTANCE;
  }

  public Matrix f1() throws NotEqualLengthsOfMatrixException {
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

  public Matrix f2() throws NotEqualLengthsOfMatrixException {
    return Matrix.subtract(
      Matrix.add(
        Matrix.multiply(MB, MO),
        Matrix.multiply(MC, MX)
      ),
      MM
    );
  }

  public Matrix f3() throws NotEqualLengthsOfMatrixException {
    return Matrix.subtract(
      Matrix.multiply(
        MD,
        Matrix.add(MT, MZ)
      ),
      Matrix.multiply(ME, MM)
    );
  }

  public Matrix f4() throws NotEqualLengthsOfMatrixException {
    return Matrix.add(
      Matrix.multiply(MC, B),
      Matrix.multiply(MM, D)
    );
  }
  
}