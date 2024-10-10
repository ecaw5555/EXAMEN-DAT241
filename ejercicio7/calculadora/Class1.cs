using System;
using System.Collections.Generic;

namespace calculadora
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Crear instancia de la calculadora infija
            var calculadoraInfija = new CalcuInfijo();
            var resultadoInfijo = calculadoraInfija.Evaluar("3 + 5 * ( 2 - 8 )");
            Console.WriteLine($"Resultado infijo: {resultadoInfijo}");

            // Crear instancia de la calculadora prefija
            var calculadoraPrefija = new CalcuPrefijo();
            var resultadoPrefijo = calculadoraPrefija.Evaluar("+ 3 * 5 - 2 8");
            Console.WriteLine($"Resultado prefijo: {resultadoPrefijo}");
        }
    }

    public class CalcuInfijo
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

    public class CalcuPrefijo
    {
        public double Evaluar(string expresion)
        {
            var tokens = expresion.Split(' ');
            Array.Reverse(tokens);
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
