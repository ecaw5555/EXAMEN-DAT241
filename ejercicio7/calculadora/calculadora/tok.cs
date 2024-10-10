using System;
using System.Collections.Generic;

namespace calculadora
{
    public class tok
    {
        public List<string> Tokenize(string expression)
        {
            var tokens = new List<string>();
            var num = "";

            foreach (var c in expression)
            {
                if (char.IsDigit(c) || c == '.')
                {
                    num += c;
                }
                else
                {
                    if (num != "")
                    {
                        tokens.Add(num);
                        num = "";
                    }
                    if (!char.IsWhiteSpace(c))
                    {
                        tokens.Add(c.ToString());
                    }
                }
            }

            if (num != "")
            {
                tokens.Add(num);
            }

            return tokens;
        }
    }
}
