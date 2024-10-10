using System;
using calculadora;

namespace calculadora
{
    class MainClass
    {
        static void Main(string[] args)
        {
            var infi = new infija();
            var prefi = new prefija();

           Console.WriteLine("Ingrese una expresión infija:");
            string infixExpression = Console.ReadLine();
         
                double infixResult = infi.Evaluate(infixExpression);
                Console.WriteLine($"Resultado infijo: {infixResult}");
            
           

            Console.WriteLine("Ingrese una expresión prefija:");
            string prefixExpression = Console.ReadLine();
           
                double prefixResult = prefi.Evaluate(prefixExpression);
                Console.WriteLine($"Resultado prefijo: {prefixResult}");
            }
           
        }
    }

