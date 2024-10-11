#include <stdio.h>

int suma(int a, int b) {
    return a + b;
}

int resta(int a, int b) {
    return a - b;
}

int multiplicacion(int a, int b) {
    return a * b;
}

float division(int a, int b) {
    if (b != 0) {
        return (float)a / b;
    } else {
        printf("Error: \n");
        return 0;
    }
}

int main() {
    int a, b;
    printf("Ingrese a numero: ");
    scanf("%d", &a);
    printf("Ingrese b numero: ");
    scanf("%d", &b);

    printf("Suma: %d\n", suma(a, b));
    printf("Resta: %d\n", resta(a, b));
    printf("Multiplicacion: %d\n", multiplicacion(a, b));
    printf("Division: %.2f\n", division(a, b));

    return 0;
}


