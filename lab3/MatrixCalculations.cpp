/**
 * MatrixCalculations class. NATIVE C++ std::thread (no Win32)
 *
 */

#include <iostream>
#include <vector>
#include <thread>
// #include "../shared/Matrix.hpp"
#include "MatrixCalculations.hpp"
using namespace std;

MatrixCalculations::MatrixCalculations() {
  // A(13);
  // B(13);
  // B.displayMatrix();
  // D(13);
  // E(13);
  MA(13, 13);
  MB(13, 13);
  MC(13, 13);
  MD(13, 13);
  ME(13, 13);
  MM(13, 13);
  MO(13, 13);
  MT(13, 13);
  MX(13, 13);
  MZ(13, 13);

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
}

Matrix MatrixCalculations::F1Procedure() {
  cout << "F1 starts on thread with number: " << std::this_thread::get_id() << endl;
  Matrix res = Matrix::add(
    Matrix::multiply(
      Matrix::add(this->MC, this->MZ),
      this->B
    ),
    Matrix.multiply(
      Matrix.subtract(this->MM, this->MO),
      this->E
    )
  );
  cout << "F1 finished on thread with number: " << std::this_thread::get_id() << endl;
  // res.displayMatrix();
  return res;
}

// MF = k*MG - h*MK*ML
Matrix MatrixCalculations::F2Procedure() {
  cout << "F2 starts on thread with number: " << std::this_thread::get_id() << endl;
  Matrix res = Matrix::subtract(
    Matrix::add(
      Matrix::multiply(this->MB, this->MO),
      Matrix::multiply(this->MC, this->MX)
    ),
    this->MM
  );
  cout << "F2 finished on thread with number: " << std::this_thread::get_id() << endl;
  // res.displayMatrix();
  return res;
}

// MF = k*MG - h*MK*ML
Matrix MatrixCalculations::F3Procedure() {
  cout << "F3 starts on thread with number: " << std::this_thread::get_id() << endl;
  Matrix res = Matrix::subtract(
    Matrix::multiply(
      this->MD,
      Matrix::add(this->MT, this->MZ)
    ),
    Matrix::multiply(this->ME, this->MM)
  );
  cout << "F3 finished on thread with number: " << std::this_thread::get_id() << endl;
  // res.displayMatrix();
  return res;
}

// MF = k*MG - h*MK*ML
Matrix MatrixCalculations::F4Procedure() {
  cout << "F4 starts on thread with number: " << std::this_thread::get_id() << endl;
  Matrix res = Matrix::add(
    Matrix::multiply(this->MC, this->B),
    Matrix::multiply(this->MM, this->D)
  );
  cout << "F4 finished on thread with number: " << std::this_thread::get_id() << endl;
  // res.displayMatrix();
  return res;
}
