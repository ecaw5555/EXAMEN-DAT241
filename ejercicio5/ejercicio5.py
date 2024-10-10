import multiprocessing

def calculapi(ini, fin):
    suma = 0
    for k in range(ini, fin):
        suma += ((-1) ** k) / (2 * k + 1)
    return suma

if __name__ == "__main__":
    ter = 1000000
    numproce = 3
    tam = ter // numproce

    with multiprocessing.Pool(processes=numproce) as pool:
        resultados = []
        for i in range(numproce):
            ini = i * tam
            fin = (i + 1) * tam
            resultados.append(pool.apply_async(calculapi, (ini, fin)))
        partes_pi = []
        for resultado in resultados:
            partes_pi.append(resultado.get())

    pi_estimado = 4 * sum(partes_pi)
    print("Estimación de pi es: ", pi_estimado)
    print("Procesadores:", numproce)
