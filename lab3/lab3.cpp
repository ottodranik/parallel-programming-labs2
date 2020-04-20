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
 *
 * Mistake # 5: Not protecting shared data or shared resources with a critical section (eg. mutex)
 * https://www.acodersjourney.com/top-20-cplusplus-multithreading-mistakes/
 */

#include <iostream>
#include <vector>
#include <thread>
#include "../shared/Matrix.hpp"
// #include "MatrixCalculations.hpp"
using namespace std;
using namespace std::chrono;

std::mutex mu;

Matrix F1Procedure(Matrix MC, Matrix MZ, Matrix B, Matrix MM, Matrix MO, Matrix E);
Matrix F2Procedure(Matrix MB, Matrix MO, Matrix MC, Matrix MX, Matrix MM);
Matrix F3Procedure(Matrix MD, Matrix MT, Matrix MZ, Matrix ME, Matrix MM);
Matrix F4Procedure(Matrix MC, Matrix B, Matrix MM, Matrix D);
void noThreadsCalcs(Matrix MC, Matrix MZ, Matrix B, Matrix MM, Matrix MO, Matrix E, Matrix MB, Matrix MX, Matrix MD, Matrix MT, Matrix ME, Matrix D, steady_clock::time_point begin);
void logTime(steady_clock::time_point begin, string msg);
void loggerStart(string name);
void loggerEnd(string name, Matrix matrix);

int main() {

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

  B.fillRandomValues();
  D.fillRandomValues();
  E.fillRandomValues();
  MB.fillRandomValues();
  MC.fillRandomValues();
  MD.fillRandomValues();
  ME.fillRandomValues();
  MM.fillRandomValues();
  MO.fillRandomValues();
  MT.fillRandomValues();
  MX.fillRandomValues();
  MZ.fillRandomValues();

  steady_clock::time_point begin = steady_clock::now();

  // noThreadsCalcs(MC, MZ, B, MM, MO, E, MB, MX, MD, MT, ME, D, begin);
  
  // Native THREADs calculations
  begin = steady_clock::now();

  std::thread t1(F1Procedure, MC, MZ, B, MM, MO, E);
  std::thread t2(F2Procedure, MB, MO, MC, MX, MM);
  std::thread t3(F3Procedure, MD, MT, MZ, ME, MM);
  std::thread t4(F4Procedure, MC, B, MM, D);

  t1.join();
  t2.join();
  t3.join();
  t4.join();

  logTime(begin, "Native THREAD execution finished with time: ");

  return 0;
}

Matrix F1Procedure(Matrix MC, Matrix MZ, Matrix B, Matrix MM, Matrix MO, Matrix E) {
  loggerStart("F1");
  Matrix res = Matrix::add(
    Matrix::multiply(
      Matrix::add(MC, MZ),
      B
    ),
    Matrix::multiply(
      Matrix::subtract(MM, MO),
      E
    )
  );
  loggerEnd("F1", res);
  return res;
}

// MF = k*MG - h*MK*ML
Matrix F2Procedure(Matrix MB, Matrix MO, Matrix MC, Matrix MX, Matrix MM) {
  loggerStart("F2");
  Matrix res = Matrix::subtract(
    Matrix::add(
      Matrix::multiply(MB, MO),
      Matrix::multiply(MC, MX)
    ),
    MM
  );
  loggerEnd("F2", res);
  return res;
}

// MF = k*MG - h*MK*ML
Matrix F3Procedure(Matrix MD, Matrix MT, Matrix MZ, Matrix ME, Matrix MM) {
  loggerStart("F3");
  Matrix res = Matrix::subtract(
    Matrix::multiply(
      MD,
      Matrix::add(MT, MZ)
    ),
    Matrix::multiply(ME, MM)
  );
  loggerEnd("F3", res);
  return res;
}

// MF = k*MG - h*MK*ML
Matrix F4Procedure(Matrix MC, Matrix B, Matrix MM, Matrix D) {
  loggerStart("F4");
  Matrix res = Matrix::add(
    Matrix::multiply(MC, B),
    Matrix::multiply(MM, D)
  );
  loggerEnd("F4", res);
  return res;
}

void logTime(steady_clock::time_point begin, string msg) {
  steady_clock::time_point end = steady_clock::now();
  duration<double, std::milli> duration = (end - begin);
  cout << msg << duration.count() << "\n\n";
}

void loggerStart(string name) {
  mu.lock();
  cout << name << " starts on thread with number: " << std::this_thread::get_id() << endl;
  mu.unlock();
}

void loggerEnd(string name, Matrix matrix) {
  mu.lock();
  cout << name << " finished on thread with number: " << std::this_thread::get_id() << endl;
  matrix.displayMatrix();
  mu.unlock();
}

/**
 * DIRECT calculations without threads
 */
void noThreadsCalcs(Matrix MC, Matrix MZ, Matrix B, Matrix MM, Matrix MO, Matrix E, Matrix MB, Matrix MX, Matrix MD, Matrix MT, Matrix ME, Matrix D, steady_clock::time_point begin) {
  F1Procedure(MC, MZ, B, MM, MO, E);
  cout << endl;

  F2Procedure(MB, MO, MC, MX, MM);
  cout << endl;

  F3Procedure(MD, MT, MZ, ME, MM);
  cout << endl;

  F4Procedure(MC, B, MM, D);
  cout << endl;
  logTime(begin, "DIRECT execution finished with time: ");
}
