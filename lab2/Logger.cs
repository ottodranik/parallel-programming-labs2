using System;
using System.Diagnostics;

namespace Lab2App {
  public class Logger {

    private static Logger instance = null;
    private static readonly object padlock = new object();

    Logger() {}

    public static Logger Instance {
      get {
        lock(padlock) {
          if (instance == null) {
            instance = new Logger();
          }
          return instance;
        }
      }
    }

    /**
     * https://stackoverflow.com/a/14728867
     */
    public static long nanoTime() {
      long nano = 10000L * Stopwatch.GetTimestamp();
      nano /= TimeSpan.TicksPerMillisecond;
      nano *= 100L;
      return nano;
    }

    public static long getTime(long nanoValue) {
      return nanoValue/1000000;
    }

    public void start(String message) {
      lock(this) {
        Console.WriteLine();
        Console.WriteLine(message);
      }
    }

    public void end(String startMessage, String endMessage, Matrix MATRIX) {
      lock(this) {
        Console.WriteLine();
        Console.WriteLine(startMessage);
        // MATRIX.displayMatrix();
        Console.WriteLine(endMessage);
      }
    }

    public static void displayMatrix(String fnName, Matrix M) {
      Console.WriteLine("  " + fnName + " finished with data: ");
      // M.displayMatrix();
    }
  }
}