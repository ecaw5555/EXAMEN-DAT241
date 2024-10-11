#include <stdio.h>
#include <omp.h>

int main() {
    unsigned long long a = 0, b = 1, c;  
    int n;

    printf("Introduce el número de términos de la serie Fibonacci: ");
    scanf("%d", &n);

    printf("Serie de Fibonacci: ");

    #pragma omp parallel private(c) shared(a, b)
    {
        #pragma omp single
        {
            for (int i = 0; i < n; i++) {
                if (i == 0) {
                    #pragma omp critical
                    {
                        printf("%llu ", a);
                    }
                } else if (i == 1) {
                    #pragma omp critical
                    {
                        printf("%llu ", b);
                    }
                } else {
                    #pragma omp critical
                    {
                        c = a + b;
                        printf("%llu ", c);
                        a = b;
                        b = c;
                    }
                }
            }
        }
    }

    printf("\n");
    return 0;
}
