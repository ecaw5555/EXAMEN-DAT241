using System;
using System.Collections.Generic;

namespace calculadora2
{
    public class Calculadora
    {
        public double infija(string expression)
        {
            var tokens = Tok(expression);
            var val = new Stack<double>();
            var oper = new Stack<char>();

            foreach (var token in tokens)
            {
                if (double.TryParse(token, out var number))
                {
                    val.Push(number);
                }
                else if (token == "(")
                {
                    oper.Push('(');
                }
                else if (token == ")")
                {
                    while (oper.Count > 0 && oper.Peek() != '(')
                    {
                        val.Push(Evalua(oper.Pop(), val.Pop(), val.Pop()));
                    }
                    oper.Pop(); 
                }
                else
                {
                    while (oper.Count > 0 && GetPriority(token[0]) <= GetPriority(oper.Peek()))
                    {
                        val.Push(Evalua(oper.Pop(), val.Pop(), val.Pop()));
                    }
                    oper.Push(token[0]);
                }
            }

            while (oper.Count > 0)
            {
                val.Push(Evalua(oper.Pop(), val.Pop(), val.Pop()));
            }

            return val.Pop();
        }

        public double prefija(string expression)
        {
            var tokens = Tok(expression);
            tokens.Reverse(); 
            var values = new Stack<double>();

            foreach (var token in tokens)
            {
                if (double.TryParse(token, out var number))
                {
                    values.Push(number);
                }
                else
                {
                   
                    var b = values.Pop(); 
                    var a = values.Pop(); 
                    values.Push(Evalua(token[0], a, b));
                }
            }

            return values.Pop();
        }



        private List<string> Tok(string expression)
        {
            var tokens = new List<string>();
            var number = "";

            foreach (var c in expression)
            {
                if (char.IsDigit(c) || c == '.')
                {
                    number += c;
                }
                else
                {
                    if (!string.IsNullOrEmpty(number))
                    {
                        tokens.Add(number);
                        number = "";
                    }
                    if (!char.IsWhiteSpace(c))
                    {
                        tokens.Add(c.ToString());
                    }
                }
            }

            if (!string.IsNullOrEmpty(number))
            {
                tokens.Add(number);
            }

            return tokens;
        }

        private int GetPriority(char op)
        {
            return op switch
            {
                '+' or '-' => 1,
                '*' or '/' => 2,
                _ => 0,
            };
        }

        private double Evalua(char op, double b, double a)
        {
            return op switch
            {
                '+' => a + b,
                '-' => a - b,
                '*' => a * b,
                '/' => a / b,
                _ => throw new ArgumentException("Operador desconocido"),
            };
        }
    }
}


















