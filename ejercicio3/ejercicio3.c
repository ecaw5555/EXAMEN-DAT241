#include <stdio.h>

void suma(int *a, int *b, int *tot) {
    *tot = *a + *b;
}

void resta(int *a, int *b, int *tot) {
    *tot = *a - *b;
}

void multiplicacion(int *a, int *b, int *tot) {
    *tot = *a * *b;
}

void division(int *a, int *b, float *tot) {
    if (*b != 0) {
        *tot = (float)*a / *b;
    } else {
        printf("Error:\n");
        *tot = 0;
    }
}

int main() {
    int a, b;
    int totint;
    float totfloat;

    printf("Ingrese a numero: ");
    scanf("%d", &a);
    printf("Ingrese b numero: ");
    scanf("%d", &b);

    suma(&a, &b, &totint);
    printf("Suma: %d\n", totint);

    resta(&a, &b, &totint);
    printf("Resta: %d\n", totint);

    multiplicacion(&a, &b, &totint);
    printf("Multiplicacion: %d\n", totint);

    division(&a, &b, &totfloat);
    printf("Division: %.2f\n", totfloat);

    return 0;
}


