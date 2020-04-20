using System;
using System.Threading;

namespace Lab2App {
  [Obsolete("Not used any more", true)]
  public class F1 {

    public static int count = 0; // количество созданных потоков
    String name; // имя потока
    Matrix f1 = new Matrix(0, 0);
    MatrixCalculations matrixCalc = MatrixCalculations.Instance;
    Logger logger = Logger.Instance;

    public F1(String name) {
      this.name = name.ToUpper();
    }

    public void run() {
      Thread.CurrentThread.Name = this.name;
      long start = 0, finish = 0;
      logger.start(
        "  Thread " + Thread.CurrentThread.ManagedThreadId + " named '" + Thread.CurrentThread.Name + "' is running..."
      );
      try {
        // Displaying the thread that is running
        start = Logger.nanoTime();
        f1 = matrixCalc.f1();
        finish = Logger.nanoTime();
      } catch (Exception e) {
        // Throwing an exception
        Console.WriteLine("  Exception is caught: " + e.Message);
      }
      logger.end(
        "  Thread " + Thread.CurrentThread.Name + " is over with data: ",
        "  Thread " + Thread.CurrentThread.Name + " is over with time: " + Logger.getTime(finish - start),
        f1
      );
    }
  }
}