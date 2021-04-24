using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SLIDDES
{
    /// <summary>
    /// Contains string related functions
    /// </summary>
    public static class String
    {
        /// <summary>
        /// Returns a string until it comes accros the stopAt character or empty
        /// </summary>
        /// <param name="s">The string to check</param>
        /// <param name="stopAt">The string char to stop at</param>
        /// <returns>String</returns>
        public static string GetUntilOrEmpty(string s, string stopAt)
        {
            // https://stackoverflow.com/questions/1857513/get-substring-everything-before-certain-char
            if(!string.IsNullOrWhiteSpace(s))
            {
                int charLocation = s.IndexOf(stopAt, StringComparison.Ordinal);

                if(charLocation > 0)
                {
                    return s.Substring(0, charLocation);
                }
                return s;
            }
            return string.Empty;
        }
    }
}