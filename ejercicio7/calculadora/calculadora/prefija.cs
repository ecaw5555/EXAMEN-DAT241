using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculadora
{
    public class prefija
    {

        public double Evaluate(string res)
        {
            var tokens = new tok().Tokenize(res);
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
                    var a = values.Pop();
                    var b = values.Pop();
                    values.Push(Evalua(token[0], a, b));
                }
            }

            return values.Pop();
        }

        private double Evalua(char op, double a, double b)
        {
            return op switch
            {
                '+' => a + b,
                '-' => a - b,
                '*' => a * b,
                '/' => a / b,
         
            };
        }
    }

}