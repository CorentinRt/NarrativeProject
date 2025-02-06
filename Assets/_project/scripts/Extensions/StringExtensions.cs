using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NarrativeProject
{
    public static class StringExtensions
    {
        public static string GetName(this string myString)
        {
            string name = "";
            foreach (char c in myString)
            {
                if (c == '_') break;
                name += c;
            }
            return name;
        }

        public static string GetIndex(this string myString)
        {
            string index = "";
            foreach (char c in myString)
            {
                if (char.IsDigit(c))
                {
                    index += c;
                }
            }
            return index;
        }
    }
}
