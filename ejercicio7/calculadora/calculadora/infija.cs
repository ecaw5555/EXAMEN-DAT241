using System;
using System.Collections.Generic;

namespace calculadora
{
    public class infija
    {
        public double Evaluate(string expression)
        {
            var tokens = new tok().Tokenize(expression);
            var eva = new Stack<double>();
            var opera = new Stack<string>(); 
            foreach (var token in tokens)
            {
                if (double.TryParse(token, out var number))
                {
                    eva.Push(number);
                }
                else if (token == "(")
                {
                    opera.Push(token);
                }
                else if (token == ")")
                {
                    while (opera.Count > 0 && opera.Peek() != "(")
                    {
                        eva.Push(EvaluateOperation(opera.Pop(), eva.Pop(), eva.Pop()));
                    }
                    if (opera.Count == 0) throw new ArgumentException("Paréntesis sin cerrar.");
                    opera.Pop();
                }
                else
                {
                    while (opera.Count > 0 && GetPriority(token) <= GetPriority(opera.Peek()))
                    {
                        eva.Push(EvaluateOperation(opera.Pop(), eva.Pop(), eva.Pop()));
                    }
                    opera.Push(token);
                }
            }

            while (opera.Count > 0)
            {
                eva.Push(EvaluateOperation(opera.Pop(), eva.Pop(), eva.Pop()));
            }

            return eva.Pop();
        }

        private int GetPriority(string op)
        {
            return op switch
            {
                "+" => 1,
                "-" => 1,
                "*" => 2,
                "/" => 2,
                _ => 0,
            };
        }

        private double EvaluateOperation(string op, double b, double a)
        {
            return op switch
            {
                "+" => a + b,
                "-" => a - b,
                "*" => a * b,
                "/" => a / b,
               
            };
        }
    }
}

