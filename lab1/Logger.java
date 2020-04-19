package lab1;

class Logger {

  /**
   * NanoTime
   * https://stackoverflow.com/questions/180158/how-do-i-time-a-methods-execution-in-java
   */
  public static long getTime(long nanoValue) {
    return nanoValue/1000000;
  }

  synchronized public static void start(String message) {
    System.out.println();
    System.out.println(message);
  }

  synchronized public static void end(String startMessage, String endMessage, Matrix MATRIX) {
    System.out.println();
    System.out.println(startMessage);
    // MATRIX.displayMatrix();
    System.out.println(endMessage);
  }

  public static void displayMatrix(String fnName, Matrix M) {
    System.out.println("  " + fnName + " finished with data: ");
    // M.displayMatrix();
  }
}
