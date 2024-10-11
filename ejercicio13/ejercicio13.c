#include <stdio.h>
#include <mpi.h>
#include <string.h>

int main(int argc, char *argv[]) {
    int proceso, cantidad;
    int vectora;

    MPI_Init(&argc, &argv);
    MPI_Comm_rank(MPI_COMM_WORLD, &proceso);
    MPI_Comm_size(MPI_COMM_WORLD, &cantidad);

    if (cantidad != 3) {
        if (proceso == 0) {
            printf("Este programa requiere exactamente 3 procesos.\n");
        }
        MPI_Finalize();
        return 1;
    }

    if (proceso == 0) {
        printf("Introduce la cantidad de elementos: ");
        scanf("%d", &vectora);
        
        char elementos[vectora][10];
        printf("Introduce %d elementos:\n", vectora);
        for (int i = 0; i < vectora; i++) {
            scanf("%s", elementos[i]);
        }
        for (int i = 1; i < cantidad; i++) {
            MPI_Send(&vectora, 1, MPI_INT, i, 0, MPI_COMM_WORLD);
            MPI_Send(elementos, vectora * 10, MPI_CHAR, i, 1, MPI_COMM_WORLD);
        }
    } else {
        MPI_Recv(&vectora, 1, MPI_INT, 0, 0, MPI_COMM_WORLD, MPI_STATUS_IGNORE);
        char vectorloc[vectora][10];
        MPI_Recv(vectorloc, vectora * 10, MPI_CHAR, 0, 1, MPI_COMM_WORLD, MPI_STATUS_IGNORE);
        
        printf("Proceso %d: ", proceso);
        for (int i = (proceso - 1); i < vectora; i += 2) {
            printf("%s ", vectorloc[i]);
        }
        printf("\n");
    }

    MPI_Finalize();
    return 0;
}

