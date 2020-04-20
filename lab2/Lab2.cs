/**
 * Lab2. C# threads
 * 
 * NOTES!
 * Array Initialization - https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/arrays/multidimensional-arrays
 * ThreadStart Delegate - https://docs.microsoft.com/en-us/dotnet/api/system.threading.threadstart?view=netframework-4.8
 * !!Thread Class!! - https://docs.microsoft.com/en-us/dotnet/api/system.threading.thread?view=netframework-4.8
 * Move from Java to C# - https://medium.com/better-programming/java-to-c-c-to-java-f766c9f659c4
 * C# CPU usage - https://medium.com/@jackwild/getting-cpu-usage-in-net-core-7ef825831b8b
 * SEND CALLBACK TO CLASS -> https://stackoverflow.com/a/9931890
 */

using System;
using System.Threading;
using System.Diagnostics;

namespace Lab2App {
  public class Lab2 {
    private static String threadNameF1 = "thread1";
    private static String threadNameF2 = "thread2";
    private static String threadNameF3 = "thread3";
    private static String threadNameF4 = "thread4";
    
    public static void Main() {
      Thread.CurrentThread.Name = "C# MainThread";
      Console.WriteLine(Thread.CurrentThread.Name + " started...\n");
      var currentProcessName = Process.GetCurrentProcess().ProcessName;
      PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", currentProcessName);

      /** 
       * No threads block
       */
      // strictCalculations();

      /** 
       * Multithread block
       * The constructor for the Thread class requires a ThreadStart 
       * delegate that represents the method to be executed on the thread.
       */
      long start = 0, finish = 0, delta = 0;

      Console.WriteLine("Main thread THREAD calculations started...");

      start = Logger.nanoTime();

      MatrixCalculations matrixCalc = MatrixCalculations.Instance;
      
      // А=В*(МС+MZ)+E*(MM-МО)
      ExecFunc f1 = new ExecFunc(Lab2.threadNameF1);
      f1.calcMethod += new ExecFunc.calcMethodEventHandler(matrixCalc.f1);

      // МА=МВ*MО+МС*МХ-MM)
      ExecFunc f2 = new ExecFunc(Lab2.threadNameF2);
      f2.calcMethod += new ExecFunc.calcMethodEventHandler(matrixCalc.f2);

      // MА=MD*(MT+MZ)-(ME*MM)
      ExecFunc f3 = new ExecFunc(Lab2.threadNameF3);
      f3.calcMethod += new ExecFunc.calcMethodEventHandler(matrixCalc.f3);

      // E=В*МС+D*MM
      ExecFunc f4 = new ExecFunc(Lab2.threadNameF4);
      f4.calcMethod += new ExecFunc.calcMethodEventHandler(matrixCalc.f4);

      Thread t1 = new Thread(new ThreadStart(f1.run));
      Thread t2 = new Thread(new ThreadStart(f2.run));
      Thread t3 = new Thread(new ThreadStart(f3.run));
      Thread t4 = new Thread(new ThreadStart(f4.run));

      t1.Priority = ThreadPriority.Lowest;
      t2.Priority = ThreadPriority.Normal;
      t3.Priority = ThreadPriority.Highest;
      t4.Priority = ThreadPriority.Highest;

      t1.Start();
      t2.Start();
      t3.Start();
      t4.Start();

      try {
        t1.Join();
        t2.Join();
        t3.Join();
        t4.Join();
      } catch (ThreadInterruptedException e) {
        Console.WriteLine("Interrupted");
      }

      // showCpuLoadingTillEnd(cpuCounter, t1, t2, t3, t4);

      finish = Logger.nanoTime();
      Console.WriteLine();
      Console.WriteLine(Thread.CurrentThread.Name + " THREAD calculations finished with time: " + Logger.getTime(finish - start));
      Console.WriteLine();
      Console.WriteLine(Thread.CurrentThread.Name + " run is over.");
    }

    /** 
     * Calculations without threads 
     */
    private static void strictCalculations() {
      long start = 0, finish = 0, delta = 0;

      MatrixCalculations matrixCalc = MatrixCalculations.Instance;

      Console.WriteLine(Thread.CurrentThread.Name + " DIRECT calculations started...");
      start = Logger.nanoTime();
      // А=В*(МС+MZ)+E*(MM-МО)
      Matrix f1 = matrixCalc.f1();
      finish = Logger.nanoTime();
      Console.WriteLine("  F1 finished with time: " + Logger.getTime(finish - start));
      Logger.displayMatrix("F1", f1);
      Console.WriteLine();
      
      delta = Logger.nanoTime();
      // МА=МВ*MО+МС*МХ-MM)
      Matrix f2 = matrixCalc.f2();
      finish = Logger.nanoTime();
      Console.WriteLine("  F2 finished with time: " + Logger.getTime(finish - delta));
      Logger.displayMatrix("F2", f2);
      Console.WriteLine();

      delta = Logger.nanoTime();
      // MА=MD*(MT+MZ)-(ME*MM)
      Matrix f3 = matrixCalc.f3();
      finish = Logger.nanoTime();
      Console.WriteLine("  F3 finished with time: " + Logger.getTime(finish - delta));
      Logger.displayMatrix("F3", f3);
      Console.WriteLine();

      delta = Logger.nanoTime();
      // E=В*МС+D*MM
      Matrix f4 = matrixCalc.f4();
      finish = Logger.nanoTime();
      Console.WriteLine("  F4 finished with time: " + Logger.getTime(finish - delta));
      Logger.displayMatrix("F4", f4);
      Console.WriteLine();

      finish = Logger.nanoTime();
      Console.WriteLine("Main thread DIRECT calculations finished with time: " + Logger.getTime(finish - start));
      Console.WriteLine();
      Console.WriteLine();
    }

    private static void showCpuLoadingTillEnd(
      PerformanceCounter cpuCounter,
      Thread t1,
      Thread t2,
      Thread t3,
      Thread t4
    ) {
      try {
        while (
          t1.IsAlive
          || t2.IsAlive
          || t3.IsAlive
          || t4.IsAlive
        ) {
          // Console.WriteLine("Main thread will be alive till the child threads is live...");
          getAndPrintCpuLoad(cpuCounter);
          Thread.Sleep(1001);
          showCpuLoadingTillEnd(cpuCounter, t1, t2, t3, t4);
        }
      } catch (ThreadInterruptedException e) {
        Console.WriteLine("Main thread interrupted: " + e.Message);
      }
    }

    private static void getAndPrintCpuLoad(PerformanceCounter cpuCounter) {
      Console.WriteLine("  | CPU load: " + cpuCounter.NextValue() + "%");
    }
  }
}