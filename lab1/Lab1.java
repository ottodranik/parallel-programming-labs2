/**
 * Lab1. JAVA threads
 * 
 * NOTES!
 * Example Project - https://github.com/howtoprogram/Kafka-MultiThread-Java-Example
 * Runnable OR Thread - https://www.journaldev.com/1016/java-thread-example
 * Thread member inside Runnable class - https://stackoverflow.com/questions/20311909/thread-member-inside-runnable-class 
 * Slow thread because of Random - https://stackoverflow.com/questions/30699631/basic-java-threading-4-threads-slower-than-non-threading
 * Joining Threads in Java - https://www.geeksforgeeks.org/joining-threads-in-java/
 * Runnable vs. Callable in Java - https://www.baeldung.com/java-runnable-callable
 * Dot and cross vector product - https://www.geeksforgeeks.org/program-dot-product-cross-product-two-vector/
 * Matrix Sorting - https://www.geeksforgeeks.org/sort-matrix-row-wise-column-wise/
 * How check proccessor cores in JAVA - https://bmwieczorek.wordpress.com/2015/11/02/java-monitoring-cpu-and-system-load-of-multi-threaded-application-via-operatingsystemmxbean/
 * Problems with CpuLoad function from jdk - https://stackoverflow.com/a/41265267
 * JAVA Map and HashMap usage - https://stackoverflow.com/questions/31591037/hashmaps-getvalue-returns-object
 */

package lab1;

import lab1.Matrix;
import java.lang.management.*;
import java.lang.reflect.*;

class Lab1 {
  private static String threadNameF1 = "thread1";
  private static String threadNameF2 = "thread2";
  private static String threadNameF3 = "thread3";
  private static String threadNameF4 = "thread4";

  public static void main(String[] args) throws java.lang.Exception {
    Thread.currentThread().setName("Java MainThread");
    System.out.println(Thread.currentThread().getName() + " started...\n");
    OperatingSystemMXBean operatingSystemMXBean = ManagementFactory.getOperatingSystemMXBean();

    /** 
     * No threads block
     */
    strictCalculations();
  
    /** 
     * Multithread block
     */
    long start = 0, finish = 0, delta = 0;

    System.out.println(Thread.currentThread().getName() + " THREAD calculations started...");

    start = System.nanoTime();

    F1 f1 = new F1(Lab1.threadNameF1);  // А=В*(МС+MZ)+E*MM-МО
    F2 f2 = new F2(Lab1.threadNameF2);  // МА=МВ*MО+МС*МХ-MM)
    F3 f3 = new F3(Lab1.threadNameF3);  // MА=MD*(MT+MZ)-(ME*MM)
    F4 f4 = new F4(Lab1.threadNameF4);  // E=В*МС+D*MM
    
    Thread t1 = new Thread(f1); // NOTE: А=В*(МС+MZ)+E*MM-МО
    Thread t2 = new Thread(f2); // NOTE: МА=МВ*MО+МС*МХ-MM    
    Thread t3 = new Thread(f3); // NOTE: MА=MD*(MT+MZ)-(ME*MM)
    Thread t4 = new Thread(f4); // NOTE: E=В*МС+D*MM

    t1.start();
    t2.start();
    t3.start();
    t4.start();

    try {
      t1.join();
      t2.join();
      t3.join();
      t4.join();
    } catch ( Exception e) {
      System.out.println("Interrupted");
    }

    showCpuLoadingTillEnd(operatingSystemMXBean, t1, t2, t3, t4);

    finish = System.nanoTime();
    System.out.println();
    System.out.println(Thread.currentThread().getName() + " THREAD calculations finished with time: " + Logger.getTime(finish - start));
    System.out.println();
    System.out.println(Thread.currentThread().getName() + " run is over.");
  }

  /** 
   * Calculations without threads 
   */
  private static void strictCalculations() throws java.lang.Exception {
    long start = 0, finish = 0, delta = 0;

    MatrixCalculations matrixCalc = MatrixCalculations.getInstance();

    System.out.println(Thread.currentThread().getName() + " DIRECT calculations started...");
    start = System.nanoTime();
    // А=В*(МС+MZ)+E*(MM-МО)
    Matrix f1 = matrixCalc.f1();
    finish = System.nanoTime();
    System.out.println("  F1 finished with time: " + Logger.getTime(finish - start));
    Logger.displayMatrix("F1", f1);
    System.out.println();
    
    delta = System.nanoTime();
    // МА=МВ*MО+МС*МХ-MM)
    Matrix f2 = matrixCalc.f2();
    finish = System.nanoTime();
    System.out.println("  F2 finished with time: " + Logger.getTime(finish - delta));
    Logger.displayMatrix("F2", f2);
    System.out.println();
    
    delta = System.nanoTime();
    // MА=MD*(MT+MZ)-(ME*MM)
    Matrix f3 = matrixCalc.f3();
    finish = System.nanoTime();
    System.out.println("  F3 finished with time: " + Logger.getTime(finish - delta));
    Logger.displayMatrix("F3", f3);
    System.out.println();

    delta = System.nanoTime();
    // E=В*МС+D*MM
    Matrix f4 = matrixCalc.f4();
    finish = System.nanoTime();
    System.out.println("  F4 finished with time: " + Logger.getTime(finish - delta));
    Logger.displayMatrix("F4", f4);

    System.out.println();
    finish = System.nanoTime();
    System.out.println(Thread.currentThread().getName() + " DIRECT calculations finished with time: " + Logger.getTime(finish - start));
    System.out.println();
    System.out.println();
  }

  private static void showCpuLoadingTillEnd(
    OperatingSystemMXBean operatingSystemMXBean,
    Thread t1,
    Thread t2,
    Thread t3,
    Thread t4
  ) {
    try {
      while (
        t1.isAlive()
        || t2.isAlive()
        || t3.isAlive()
        || t4.isAlive()
      ) {
        getAndPrintCpuLoad(operatingSystemMXBean);
        Thread.sleep(1001);
        showCpuLoadingTillEnd(operatingSystemMXBean, t1, t2, t3, t4);
      }
    } catch (InterruptedException e) {
      System.out.println(Thread.currentThread().getName() + " interrupted");
    }
  }

  private static void getAndPrintCpuLoad(OperatingSystemMXBean mxBean) {
    // need to use reflection as the impl class is not visible
    String str = "";
    System.out.println(str);
    for (Method method: mxBean.getClass().getDeclaredMethods()) {
      method.setAccessible(true);
      String methodName = method.getName();
      if (
        methodName.startsWith("get")
        && methodName.contains("Cpu")
        && methodName.contains("Load")
        && Modifier.isPublic(method.getModifiers())
      ) {
        Object value;
        try {
          value = method.invoke(mxBean);
        } catch (Exception e) {
          value = e;
        }
        System.out.println("  " + methodName + " = " + value);
      }
    }
    System.out.println(str);
  }

  /**
   * One more way to start Thread with Runnable class inside
   */
  private static void createAndStartTestWorker(
    String threadName
    // CyclicBarrier cyclicBarrier //use barrier to start all workers at the same time as main thread
  ) {
    new Thread(() -> {
      Thread.currentThread().setName(threadName);
      String thName = Thread.currentThread().getName();
      try {
        // cyclicBarrier.await();
        long counter = (Thread.currentThread().getId() % 10) * 3;
        System.out.println(counter + " <- current counter");
        for (long i = 0L; i < 19999999999L * counter; i++) { // 6s
          // Thread 100% time as RUNNABLE, taking 1/(n cores) of JVM/System overall CPU
        }
        System.out.println(thName + " finished");
      } catch (Exception e) {
        e.printStackTrace();
      }
    }).start();
  }
 
}
