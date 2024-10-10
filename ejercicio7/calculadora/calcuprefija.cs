using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculadora
{
    internal class calcuprefija
    {
        public double Evaluar(string expresion)
        {
            var tokens = expresion.Split(' ');
            Array.Reverse(tokens);
            return Evaluate(tokens);
        }

        private double Evaluate(string[] tokens)
        {
            var stack = new Stack<double>();

            foreach (var token in tokens)
            {
                if (double.TryParse(token, out var number))
                {
                    stack.Push(number);
                }
                else
                {
                    var a = stack.Pop();
                    var b = stack.Pop();
                    stack.Push(EjecutarOperacion(a, b, token));
                }
            }

            return stack.Pop();
        }

        private double EjecutarOperacion(double a, double b, string operador)
        {
            return operador switch
            {
                "+" => a + b,
                "-" => a - b,
                "*" => a * b,
                "/" => a / b,
                _ => throw new InvalidOperationException("Operador no válido")
            };
        }
    }
}
