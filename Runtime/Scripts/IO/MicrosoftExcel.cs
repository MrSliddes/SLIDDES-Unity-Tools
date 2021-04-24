using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SLIDDES.IO
{
    /// <summary>
    /// Handels interacting with microsoft excel data
    /// </summary>
    public class MicrosoftExcel
    {
        // ! Important ! the file has to end on .csv ! it does not read xslx files
        // Row <-> (horizontal)
        // Collum (vertical)

        /// <summary>
        /// Get string data from an microsoft .csv excel sheet
        /// </summary>
        /// <param name="location">Location of the excel sheet</param>
        /// <param name="collumStartIndex">On what collum index to start. Default is 0</param>
        /// <param name="collumEndIndex">Default on 1 since csv files have an end empty collum? On what collum index to stop. It gets added like ".Length - collumEndIndex"</param>
        /// <returns>[!Important The string[] contains line endings, you can remove this with "Replace(System.Environment.NewLine, "replacement text")"!] List<string[]>. Get collum index as List[collumIndex] and row as List[collumIndex].[rowIndex]. An collum and row array [collum, row] in string from the excel sheet</returns>
        public static List<string[]> GetStringDataFromExcel(string location, int collumStartIndex = 0, int collumEndIndex = 1)
        {
            // Load data as TextAsset
            TextAsset textAsset = Resources.Load<TextAsset>(location);
            if(textAsset == null) // Error catch
            {
                Debug.LogError("[TAB][MicrosoftExcel] textAsset not found at location: " + location + " !");
                return new List<string[]>();
            }

            // Create 2d string list to return as array, <index collum, string[] row strings>
            List<string[]> stringData = new List<string[]>(); // Instead of using a custom class you can use Tuple, which can contain types

            // Split textAsset into collums (vertically) on line endings '\n'
            // This might cause a problem on iOS tho https://support.nesi.org.nz/hc/en-gb/articles/218032857-Converting-from-Windows-style-to-UNIX-style-line-endings
            // Windows: \r\n
            // Mac: \n
            string[] collums = textAsset.text.Split(new char[] { '\n' });

            // Loop trough collums for row data
            for(int i = collumStartIndex; i < collums.Length - collumEndIndex; i++)
            {
                // Get rows from collum (horizontally) on commas ';'
                string[] rows = collums[i].Split(new char[] { ';' });

                // Add collum and row to string data
                stringData.Add(rows);
            }

            return stringData;
        }
    }

}