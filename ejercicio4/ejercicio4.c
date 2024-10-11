#include <stdio.h>
double PiIt(int ite) {
    double pi = 0.0;
    int ope = 1;
    for (int i = 0; i < ite; i++) {
        pi += (ope * (1.0 / (2 * i + 1)));
        ope *= -1;
    }
    pi *= 4;
    return pi;
}
double PiRec(int ite, int ope, int i, double sum) {
    if (i >= ite) return sum;
    return PiRec(ite, -ope, i + 1, sum + ope * (4.0 / (2 * i + 1)));
}

int main() {
    int ite = 100000;
    double piI = PiIt(ite);
    double piR = PiRec(ite, 1, 0, 0.0);
    printf("Pi con iterativo, con %d iteraciones): %f\n", ite, piI);
    printf("Pi (Recursivo, con %d iteraciones): %f\n", ite, piR);
    return 0;
}

