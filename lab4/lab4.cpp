/**
 * Lab5. OPENMP library with
 * 
 * NOTES!
 * !!OPENMP HOW TO!! - https://bisqwit.iki.fi/story/howto/openmp/
 * !!OPENMP THREADS!! - http://jakascorner.com/blog/2016/05/omp-sections.html
 * Install gcc with openmp lib - https://github.com/microsoft/LightGBM/issues/3
 * Usage openmp lib - https://helloacm.com/simple-tutorial-with-openmp-how-to-use-parallel-block-in-cc-using-openmp/
 * How to build cpp file with gcc (not g++) with (-lstdc++) - https://stackoverflow.com/a/3206195
 * Matrix in heap - https://stackoverflow.com/questions/1403150/how-do-you-dynamically-allocate-a-matrix
 * Vector of vectors for matrix - https://stackoverflow.com/questions/12375591/vector-of-vectors-to-create-matrix
 * C++ classes and imports - https://stackoverflow.com/a/12734036
 * C++ vector init - https://www.techiedelight.com/initialize-two-dimensional-vector-cpp/
 * C++ classes tutorial - http://www.cplusplus.com/doc/tutorial/classes/
 * Executable build file - https://www.taniarascia.com/how-to-create-and-use-bash-scripts/
 * Work with time clock - https://en.cppreference.com/w/cpp/chrono/time_point
 * 
 * 
 * Simple explanation!
 * GCC implements this by creating a magic function and moving the associated code
 * into that function, so that all the variables declared within that block become
 * local variables of that function (and thus, locals to each thread).
 * ICC, on the other hand, uses a mechanism resembling fork(), and does not create 
 * a magic function. Both implementations are, of course, valid, and semantically identical.
 * 
 * 
 * Common usage!
 * 0. pragma omp simd - multiple calculations will be performed simultaneously by the processor
 * 1. pragma omp parallel - creates a team of threads
 * 2. pragma omp for - splits the for-loop so that each thread in the current team
 * 3. pragma omp parallel for - like 1 and 2 the same time
 * 4. pragma omp parallel for if(parallelism_enabled) - like 3, but with condition
 * 5. pragma omp parallel num_threads(3) - set amount of threads
 * 6. pragma omp sections (pragma omp parallel sections) - create sections for parallel
 * 6. pragma omp section - indicates section with NO parallel inside
 */

#include <iostream>
#include <vector>
#include <thread>
#include <omp.h>
#include "../shared/Matrix.hpp"
using namespace std;
using namespace std::chrono;

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
  
  begin = steady_clock::now();
  #pragma omp parallel
  {
    #pragma omp sections
    {
      #pragma omp section
      {
        F1Procedure(MC, MZ, B, MM, MO, E);
      }

      #pragma omp section
      {
        F2Procedure(MB, MO, MC, MX, MM);
      }

      #pragma omp section
      {
        F3Procedure(MD, MT, MZ, ME, MM);
      }

      #pragma omp section
      {
        F4Procedure(MC, B, MM, D);
      }
    }
  }
  cout << endl;
  logTime(begin, "OMP THREAD execution finished with time: ");

  return 0;
}

// А=В*(МС+MZ)+E*(MM-МО)
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

// МА=МВ*MО+МС*МХ-MM)
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

// MА=MD*(MT+MZ)-(ME*MM)
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

// E=В*МС+D*MM
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
  #pragma omp critical (cout)
    cout << name << " starts on thread with number: " << std::this_thread::get_id() << endl;
}

void loggerEnd(string name, Matrix matrix) {
  #pragma omp critical (cout)
  {
    cout << endl;
    cout << name << " finished on thread with number: " << std::this_thread::get_id() << endl;
  }
  #pragma omp critical (cout)
    matrix.displayMatrix();
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
  cout << endl;
}
