package lab1;

public class MatrixCalculations {

  public static int count = 0; // количество созданных потоков
  String name; // имя потока
  Matrix B = new Matrix(13);
  Matrix E = new Matrix(13);
  Matrix MC = new Matrix(13, 13);
  Matrix MZ = new Matrix(13, 13);
  Matrix MM = new Matrix(13, 13);
  Matrix MO = new Matrix(13, 13);
  Matrix f1 = new Matrix(13, 13);

  public F1(String name, Matrix B, Matrix MC, Matrix MZ, Matrix E, Matrix MM, Matrix MO) {
    this.name = name.toUpperCase();
    this.B = B;
    this.E = E;
    this.MC = MC;
    this.MZ = MZ;
    this.MM = MM; 
    this.MO = MO;
  }

  public F1(String name) {
    this.name = name.toUpperCase();
    B.fillRandomValues();
    E.fillRandomValues();
    MC.fillRandomValues();
    MM.fillRandomValues();
    MO.fillRandomValues();
    MZ.fillRandomValues();
  }

  // ME = (A*SORT(C)) *(MA*ME+MD)
  @Override
  public void run() {
    long start = 0, finish = 0;
    Thread.currentThread().setName(this.name);
    Logger.start(
      "  Thread " + Thread.currentThread().getId() + " named '" + Thread.currentThread().getName() + "' is running..."
    );
    try {
      // Displaying the thread that is running
      start = System.nanoTime();
      f1 = Matrix.add(
        Matrix.multiply(
          Matrix.add(MC, MZ),
          B
        ),
        Matrix.multiply(
          Matrix.subtract(MM, MO),
          E
        )
      );
      finish = System.nanoTime();
    } catch (Exception e) {
      // Throwing an exception
      System.out.println("  Exception is caught");
    }
    Logger.end(
      "  Thread " + Thread.currentThread().getId() + " named '" + Thread.currentThread().getName() + "' is over with data: ",
      "  Thread " + Thread.currentThread().getId() + " named '" + Thread.currentThread().getName() + "' is over with time: " + Logger.getTime(finish - start),
      f1
    );
  }
}