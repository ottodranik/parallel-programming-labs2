package lab1;

public class F4 implements Runnable {

  public static int count = 0; // количество созданных потоков
  String name; // имя потока
  Matrix f4 = new Matrix(0, 0);
  MatrixCalculations matrixCalc = MatrixCalculations.getInstance();

  public F4(String name) {
    this.name = name.toUpperCase();
  }

  // E=В*МС+D*MM
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
      f4 = matrixCalc.f4();
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