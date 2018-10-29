using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CityLife.Extensions.ExtensionMethods
{
    public static class StringExtension
    {
        /// <summary>
        /// Convert Cyrillic to Latic alphabet
        /// </summary>
        /// <param name=""></param>
        public static string ToAlias(this string term)
        {
            if (!string.IsNullOrEmpty(term) && term.Length > 0)
            {
                term = term.ToLower().Trim();
                Dictionary<string, string> transliter = new Dictionary<string, string>();

                transliter.Add("а", "a");
                transliter.Add("б", "b");
                transliter.Add("в", "v");
                transliter.Add("г", "g");
                transliter.Add("д", "d");
                transliter.Add("е", "e");
                transliter.Add("ё", "yo");
                transliter.Add("ж", "zh");
                transliter.Add("з", "z");
                transliter.Add("и", "i");
                transliter.Add("й", "j");
                transliter.Add("к", "k");
                transliter.Add("л", "l");
                transliter.Add("м", "m");
                transliter.Add("н", "n");
                transliter.Add("о", "o");
                transliter.Add("п", "p");
                transliter.Add("р", "r");
                transliter.Add("с", "s");
                transliter.Add("т", "t");
                transliter.Add("у", "u");
                transliter.Add("ф", "f");
                transliter.Add("х", "h");
                transliter.Add("ц", "c");
                transliter.Add("ч", "ch");
                transliter.Add("ш", "sh");
                transliter.Add("щ", "sch");
                transliter.Add("ъ", "");
                transliter.Add("ы", "y");
                transliter.Add("ь", "");
                transliter.Add("э", "e");
                transliter.Add("ю", "yu");
                transliter.Add("я", "ya");

                transliter.Add("a", "a");
                transliter.Add("b", "b");
                transliter.Add("c", "c");
                transliter.Add("d", "d");
                transliter.Add("e", "e");
                transliter.Add("f", "f");
                transliter.Add("g", "g");
                transliter.Add("h", "h");
                transliter.Add("i", "i");
                transliter.Add("j", "j");
                transliter.Add("k", "k");
                transliter.Add("l", "l");
                transliter.Add("m", "m");
                transliter.Add("n", "n");
                transliter.Add("o", "o");
                transliter.Add("p", "p");
                transliter.Add("q", "q");
                transliter.Add("r", "r");
                transliter.Add("s", "s");
                transliter.Add("t", "t");
                transliter.Add("u", "u");
                transliter.Add("v", "v");
                transliter.Add("w", "w");
                transliter.Add("x", "x");
                transliter.Add("y", "y");
                transliter.Add("z", "z");
                transliter.Add("-", "-");
                transliter.Add(" ", "-");
                transliter.Add("/", "-");
                transliter.Add("\\", "-");
                transliter.Add(".", "-");
                transliter.Add(",", "-");
                transliter.Add(";", "-");

                transliter.Add("0", "0");
                transliter.Add("1", "1");
                transliter.Add("2", "2");
                transliter.Add("3", "3");
                transliter.Add("4", "4");
                transliter.Add("5", "5");
                transliter.Add("6", "6");
                transliter.Add("7", "7");
                transliter.Add("8", "8");
                transliter.Add("9", "9");


                StringBuilder ans = new StringBuilder();

                for (int i = 0; i < term.Length; i++)
                {
                    if (transliter.ContainsKey(term[i].ToString()))
                    {
                        ans.Append(transliter[term[i].ToString()]);
                    }
                }
                string alias = ans.ToString();
                alias = Regex.Replace(alias, "[-]{2,}", "-");
                return alias;
            }
            return term;
        }
    }
}
