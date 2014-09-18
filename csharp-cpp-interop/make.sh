g++ -shared -fPIC -o native.so native.cpp

mcs -unsafe -out:main.exe ./*.cs