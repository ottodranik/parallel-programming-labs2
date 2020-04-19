package lab1;

public class F3 implements Runnable {

  public static int count = 0; // количество созданных потоков
  String name; // имя потока
  Matrix MD = new Matrix(13, 13);
  Matrix ME = new Matrix(13, 13);
  Matrix MM = new Matrix(13, 13);
  Matrix MT = new Matrix(13, 13);
  Matrix MZ = new Matrix(13, 13);
  Matrix f3 = new Matrix(13, 13);

  public F3(String name, Matrix MD, Matrix MT, Matrix MZ, Matrix ME, Matrix MM) {
    this.name = name.toUpperCase();
    this.MD = MD;
    this.ME = ME;
    this.MM = MM;
    this.MT = MT;
    this.MZ = MZ;
  }

  public F3(String name) {
    this.name = name.toUpperCase();
    MD.fillRandomValues();
    ME.fillRandomValues();
    MM.fillRandomValues();
    MT.fillRandomValues();
    MZ.fillRandomValues();
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
      f3 = Matrix.subtract(
        Matrix.multiply(
          MD,
          Matrix.add(MT, MZ)
        ),
        Matrix.multiply(ME, MM)
      );
      finish = System.nanoTime();
    } catch (Exception e) {
      // Throwing an exception
      System.out.println("  Exception is caught");
    }
    Logger.end(
      "  Thread " + Thread.currentThread().getId() + " named '" + Thread.currentThread().getName() + "' is over with data: ",
      "  Thread " + Thread.currentThread().getId() + " named '" + Thread.currentThread().getName() + "' is over with time: " + Logger.getTime(finish - start),
      f3
    );
  }
}