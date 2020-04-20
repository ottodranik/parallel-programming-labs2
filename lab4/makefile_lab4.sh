#!/bin/bash
# gcc-9 -fopenmp -fconcepts -lstdc++ lab5.cpp ../shared/Matrix.cpp -o lab5
clang++ -Xpreprocessor -fopenmp -lomp -lstdc++ -std=c++11 lab4.cpp ../shared/Matrix.cpp -o lab4