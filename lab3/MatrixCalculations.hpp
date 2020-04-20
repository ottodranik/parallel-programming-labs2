/**
 * Lab4. NATIVE C++ std::thread (no Win32)
 * 
 * NOTES!
 * Native C++ threads tutorial - https://solarianprogrammer.com/2011/12/16/cpp-11-thread-tutorial/
 * Not priority in C++11 - https://stackoverflow.com/a/18884946
 * About thread join - https://stackoverflow.com/a/52307406
 * 
 * 
 * Common usage!
 * std::thread(call_from_thread, param1, param2)
 */

// #include <iostream>
// #include <vector>
// #include <thread>
#include "../shared/Matrix.hpp"
// using namespace std;

class MatrixCalculations {
  public:
    MatrixCalculations();
    Matrix F1Procedure();
    Matrix F2Procedure();
    Matrix F3Procedure();
    Matrix F4Procedure();
  private:
    Matrix A(13);
    Matrix B(13);
    Matrix D(13);
    Matrix E(13);
    Matrix MA(13, 13);
    Matrix MB(13, 13);
    Matrix MC(13, 13);
    Matrix MD(13, 13);
    Matrix ME(13, 13);
    Matrix MM(13, 13);
    Matrix MO(13, 13);
    Matrix MT(13, 13);
    Matrix MX(13, 13);
    Matrix MZ(13, 13);
};
