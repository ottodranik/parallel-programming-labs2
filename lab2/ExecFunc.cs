using System;
using System.Threading;

namespace Lab2App {
  public class ExecFunc {

    public static int count = 0; // количество созданных потоков
    String name; // имя потока
    Matrix resultMatrix = new Matrix(0, 0);
    MatrixCalculations matrixCalc = MatrixCalculations.Instance;
    Logger logger = Logger.Instance;
    public delegate Matrix calcMethodEventHandler();
    public event calcMethodEventHandler calcMethod;

    public ExecFunc(String name) {
      this.name = name.ToUpper();
    }

    public void run() {
      Thread.CurrentThread.Name = this.name;
      long start = 0, finish = 0;
      logger.start(
        "  Thread " + Thread.CurrentThread.ManagedThreadId + " named '" + Thread.CurrentThread.Name + "' is running..."
      );
      lock(this) {
      try {
        // Displaying the thread that is running
        start = Logger.nanoTime();
        resultMatrix = calcMethod();
        finish = Logger.nanoTime();
      } catch (Exception e) {
        // Throwing an exception
        Console.WriteLine("  Exception is caught: " + e.Message);
      }
      }
      logger.end(
        "  Thread " + Thread.CurrentThread.Name + " is over with data: ",
        "  Thread " + Thread.CurrentThread.Name + " is over with time: " + Logger.getTime(finish - start),
        resultMatrix
      );
    }
  }
}