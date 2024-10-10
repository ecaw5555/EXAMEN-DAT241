def fibonacci(n):
   serie = [0, 1]
   if n <= 0:
        return []
   elif n == 1:
        return [0]
   elif n == 2:
        return [0, 1]

   for i in range(2, n): 
        fiboparte = serie[i - 1] + serie[i - 2]
        serie.append(fiboparte)
   return serie  
n = int(input("ingrese el n:"))

res = fibonacci(n)
print(res)  
