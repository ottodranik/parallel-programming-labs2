package lab1;

public class F4 implements Runnable {

  public static int count = 0; // количество созданных потоков
  String name; // имя потока
  Matrix B = new Matrix(13);
  Matrix D = new Matrix(13);
  Matrix MM = new Matrix(13, 13);
  Matrix MC = new Matrix(13, 13);
  Matrix f4 = new Matrix(13, 13);

  public F4(String name, Matrix B, Matrix MC, Matrix D, Matrix MM) {
    this.name = name.toUpperCase();
    this.B = B;
    this.MC = MC;
    this.D = D;
    this.MM = MM;
  }

  public F4(String name) {
    this.name = name.toUpperCase();
    B.fillRandomValues();
    D.fillRandomValues();
    MM.fillRandomValues();
    MC.fillRandomValues();
  }

  // O = (P+R)*(MS*MT)
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
      f4 = Matrix.add(
        Matrix.multiply(MC, B),
        Matrix.multiply(MM, D)
      );
      finish = System.nanoTime();
    } catch (Exception e) {
      // Throwing an exception
      System.out.println("  Exception is caught");
    }
    Logger.end(
      "  Thread " + Thread.currentThread().getId() + " named '" + Thread.currentThread().getName() + "' is over with data: ",
      "  Thread " + Thread.currentThread().getId() + " named '" + Thread.currentThread().getName() + "' is over with time: " + Logger.getTime(finish - start),
      f4
    );
  }
}