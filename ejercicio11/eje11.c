#include <mpi.h>
#include <stdio.h>
#include <stdlib.h>

int main(int argc, char** argv) {
    int procesador, size, n, i;
    int *vec1 = NULL, *vec2 = NULL, *suma_local = NULL, *suma_global = NULL;

    MPI_Init(&argc, &argv);
    MPI_Comm_rank(MPI_COMM_WORLD, &procesador);
    MPI_Comm_size(MPI_COMM_WORLD, &size);


    if (procesador == 0) {
        scanf("%d", &n);
        vec1 = (int*)malloc(n * sizeof(int));
        vec2 = (int*)malloc(n * sizeof(int));
        suma_global = (int*)malloc(n * sizeof(int));
        printf("Ingrese elementos de vec1:\n");
        for (i = 0; i < n; i++) {
            scanf("%d", &vec1[i]);
        }
        printf("Ingrese elementos de vec2:\n");
        for (i = 0; i < n; i++) {
            scanf("%d", &vec2[i]);
        }
        MPI_Bcast(&n, 1, MPI_INT, 0, MPI_COMM_WORLD);
        MPI_Bcast(vec1, n, MPI_INT, 0, MPI_COMM_WORLD);
        MPI_Bcast(vec2, n, MPI_INT, 0, MPI_COMM_WORLD);
    } else {
        MPI_Bcast(&n, 1, MPI_INT, 0, MPI_COMM_WORLD);
        vec1 = (int*)malloc(n * sizeof(int));
        vec2 = (int*)malloc(n * sizeof(int));
        MPI_Bcast(vec1, n, MPI_INT, 0, MPI_COMM_WORLD);
        MPI_Bcast(vec2, n, MPI_INT, 0, MPI_COMM_WORLD);
    }

    suma_local = (int*)malloc(n * sizeof(int));
    for (i = 0; i < n; i++) {
        suma_local[i] = 0;
    }

    if (procesador == 1) {
        for (i = 1; i < n; i += 2) {
            suma_local[i] = vec1[i] + vec2[i];
        }
    }

    if (procesador == 2) {
        for (i = 0; i < n; i += 2) {
            suma_local[i] = vec1[i] + vec2[i];
        }
    }

    MPI_Reduce(suma_local, suma_global, n, MPI_INT, MPI_SUM, 0, MPI_COMM_WORLD);

    if (procesador == 0) {
        printf("resultado:\n");
        for (i = 0; i < n; i++) {
            printf("%d ", suma_global[i]);
        }
        printf("\n");
    }

    free(vec1);
    free(vec2);
    free(suma_local);
    if (procesador == 0) {
        free(suma_global);
    }

    MPI_Finalize();
    return 0;
}

