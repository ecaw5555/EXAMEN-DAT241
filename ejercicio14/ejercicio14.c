#include <stdio.h>
#include <stdlib.h>
#include <mpi.h>

int main(int argc, char *argv[]) {
    int procesador, cantidad;
    int N;
    int *A = NULL, *B = NULL, *C = NULL;

    MPI_Init(&argc, &argv);
    MPI_Comm_rank(MPI_COMM_WORLD, &procesador);
    MPI_Comm_size(MPI_COMM_WORLD, &cantidad);

    if (procesador == 0) {
   
        scanf("%d", &N);

        A = malloc(N * N * sizeof(int));
        B = malloc(N * N * sizeof(int));
        C = malloc(N * N * sizeof(int));

        printf("Ingrese A:\n");
        for (int i = 0; i < N; i++) {
            for (int j = 0; j < N; j++) {
                scanf("%d", &A[i * N + j]);
            }
        }

        printf("Ingrese  B:\n");
        for (int i = 0; i < N; i++) {
            for (int j = 0; j < N; j++) {
                scanf("%d", &B[i * N + j]);
            }
        }
    }

    MPI_Bcast(&N, 1, MPI_INT, 0, MPI_COMM_WORLD);

    if (procesador != 0) {
        A = malloc(N * N * sizeof(int));
        B = malloc(N * N * sizeof(int));
    }
    C = malloc(N * N * sizeof(int));

    MPI_Bcast(A, N * N, MPI_INT, 0, MPI_COMM_WORLD);
    MPI_Bcast(B, N * N, MPI_INT, 0, MPI_COMM_WORLD);

    int filas_por_proceso = N / cantidad;
    int inicio_fila = procesador * filas_por_proceso;
    int fin_fila = (procesador == cantidad - 1) ? N : inicio_fila + filas_por_proceso;

    for (int i = inicio_fila; i < fin_fila; i++) {
        for (int j = 0; j < N; j++) {
            C[i * N + j] = 0;
            for (int k = 0; k < N; k++) {
                C[i * N + j] += A[i * N + k] * B[k * N + j];
            }
        }
    }

    if (procesador == 0) {
        for (int i = 1; i < cantidad; i++) {
            int inicio = i * filas_por_proceso;
            int fin = (i == cantidad - 1) ? N : inicio + filas_por_proceso;
            MPI_Recv(&C[inicio * N], (fin - inicio) * N, MPI_INT, i, 0, MPI_COMM_WORLD, MPI_STATUS_IGNORE);
        }

        printf("Res:\n");
        for (int i = 0; i < N; i++) {
            for (int j = 0; j < N; j++) {
                printf("%d ", C[i * N + j]);
            }
            printf("\n");
        }
    } else {
        MPI_Send(&C[inicio_fila * N], (fin_fila - inicio_fila) * N, MPI_INT, 0, 0, MPI_COMM_WORLD);
    }

    free(A);
    free(B);
    free(C);

    MPI_Finalize();
    return 0;
}

