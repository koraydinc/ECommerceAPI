﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Infrastructure.Operations
{
    public static class NameOperation
    {
        public static string CharacterRegulatory(string name)
        {
            char[] invalidChars = { '$', ':', ';', '@', '+', '-', '_', '=', '(', ')', '{', '}', '[', ']', '∑', '€', '₺', '¥', 'π', '¨', '~', 'æ', 'ß', '∂', 'ƒ', '^', '∆', '´', '¬', 'Ω', '√', '∫', 'µ', '≥', '÷', '|' };

            name = name.ToLower();
            name = name.TrimStart(invalidChars).TrimEnd(invalidChars);

            return name.Replace(" ", "-")
                .Replace("/", "")
                .Replace("\"", "")
                .Replace("'", "")
                .Replace("^", "")
                .Replace("+", "")
                .Replace("%", "")
                .Replace("&", "")
                .Replace("(", "")
                .Replace(")", "")
                .Replace("[", "")
                .Replace("]", "")
                .Replace("{", "")
                .Replace("}", "")
                .Replace("=", "")
                .Replace("*", "")
                .Replace("?", "")
                .Replace("_", "")
                .Replace("~", "")
                .Replace("$", "")
                .Replace("#", "")
                .Replace("£", "")
                .Replace("!", "")
                .Replace("@", "")
                .Replace("æ", "")
                .Replace("ß", "")
                .Replace("`", "")
                .Replace(":", "")
                .Replace("<", "")
                .Replace(">", "")
                .Replace("|", "")
                .Replace(",", "")
                .Replace(";", "")
                .Replace("Ö", "o")
                .Replace("ö", "o")
                .Replace("İ", "i")
                .Replace("ı", "i")
                .Replace("Ü", "u")
                .Replace("ü", "u")
                .Replace("Ğ", "g")
                .Replace("ğ", "g")
                .Replace("Ç", "c")
                .Replace("ç", "c")
                .Replace("Ş", "s")
                .Replace("ş", "s");
        }
    }
}
