using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculadora
{
    public class calcuinfijo
    
    {
        public double Evaluar(string expresion)
        {
            var postfija = InfixToPostfix(expresion);
            return EvaluatePostfix(postfija);
        }

        private Queue<string> InfixToPostfix(string expresion)
        {
            var output = new Queue<string>();
            var operadores = new Stack<string>();
            var tokens = expresion.Split(' ');

            foreach (var token in tokens)
            {
                if (double.TryParse(token, out _))
                {
                    output.Enqueue(token);
                }
                else if (token == "(")
                {
                    operadores.Push(token);
                }
                else if (token == ")")
                {
                    while (operadores.Count > 0 && operadores.Peek() != "(")
                    {
                        output.Enqueue(operadores.Pop());
                    }
                    operadores.Pop(); // quitar '('
                }
                else
                {
                    while (operadores.Count > 0 && Precedencia(token) <= Precedencia(operadores.Peek()))
                    {
                        output.Enqueue(operadores.Pop());
                    }
                    operadores.Push(token);
                }
            }

            while (operadores.Count > 0)
            {
                output.Enqueue(operadores.Pop());
            }

            return output;
        }

        private int Precedencia(string operador)
        {
            return operador switch
            {
                "+" or "-" => 1,
                "*" or "/" => 2,
                _ => 0
            };
        }

        private double EvaluatePostfix(Queue<string> postfija)
        {
            var stack = new Stack<double>();

            while (postfija.Count > 0)
            {
                var token = postfija.Dequeue();
                if (double.TryParse(token, out var number))
                {
                    stack.Push(number);
                }
                else
                {
                    var b = stack.Pop();
                    var a = stack.Pop();
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
