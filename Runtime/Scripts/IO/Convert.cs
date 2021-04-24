using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace SLIDDES.IO
{
    /// <summary>
    /// Convert stuff to other stuff
    /// </summary>
    public static class Convert
    {
        /// <summary>
        /// Converts a List<string[]> to sinle text string for file generation
        /// </summary>
        /// <param name="data">The data to convert</param>
        /// <returns>String containing data</returns>
        public static string ListStringArrayToTextString(List<string[]> data, bool removeLineBreaks = true)
        {
            // { new string[] { "a, b"}, new string[] { "c, d"} };
            string s = "{ ";

            // Extract data
            for(int i = 0; i < data.Count; i++)
            {
                s += " new string[] {";

                for(int j = 0; j < data[i].Length; j++)
                {
                    s += "\"" + data[i][j] + "\""; // Dont forget to remove \n!, 

                    // Check if there is more for ,
                    if(j + 1 != data[i].Length) s += ","; // BUG somewhere forgets a , which fucks up the file
                }

                s += "}";
                // Check if there is more for ,
                if(i < data.Count - 1) s += ",";
            }

            s += " };";

            // remove any linebreaks
            if(removeLineBreaks)
            {
                s = Regex.Replace(s, @"\t|\n|\r", "");
            }

            return s;
        }
    }
}
