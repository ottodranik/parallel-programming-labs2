package lab1;

public class F2 implements Runnable {

  public static int count = 0; // количество созданных потоков
  String name; // имя потока
  Matrix MB = new Matrix(13, 13);
  Matrix MC = new Matrix(13, 13);
  Matrix MM = new Matrix(13, 13);
  Matrix MO = new Matrix(13, 13);
  Matrix MX = new Matrix(13, 13);
  Matrix f2 = new Matrix(13, 13);

  public F2(String name, Matrix MB, Matrix MO, Matrix MC, Matrix MX, Matrix MM) {
    this.name = name.toUpperCase();
    this.MB = MB;
    this.MO = MO;
    this.MC = MC;
    this.MX = MX;
    this.MM = MM;
  }

  public F2(String name) {
    this.name = name.toUpperCase();
    MB.fillRandomValues();
    MC.fillRandomValues();
    MM.fillRandomValues();
    MO.fillRandomValues();
    MX.fillRandomValues();
  }

  // MF = k*MG - h*MK*ML
  @Override
  synchronized public void run() {
    long start = 0, finish = 0;
    Thread.currentThread().setName(this.name);
    Logger.start(
      "  Thread " + Thread.currentThread().getId() + " named '" + Thread.currentThread().getName() + "' is running..."
    );
    try {
      // Displaying the thread that is running
      start = System.nanoTime();
      f2 = Matrix.subtract(
        Matrix.add(
          Matrix.multiply(MB, MO),
          Matrix.multiply(MC, MX)
        ),
        MM
      );
      finish = System.nanoTime();
    } catch (Exception e) {
      // Throwing an exception
      System.out.println("  Exception is caught");
    }
    Logger.end(
      "  Thread " + Thread.currentThread().getId() + " named '" + Thread.currentThread().getName() + "' is over with data: ",
      "  Thread " + Thread.currentThread().getId() + " named '" + Thread.currentThread().getName() + "' is over with time: " + Logger.getTime(finish - start),
      f2
    );
  }
}