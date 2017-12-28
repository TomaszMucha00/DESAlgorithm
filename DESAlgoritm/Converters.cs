using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DESAlgoritm
{
    class Converters
    {
        //Formats a byte[] into a binary string (010010010010100101010)
        public static string ByteToBitTab(byte[] data)
        {
            //storage for the resulting string
            string result = string.Empty;
            //iterate through the byte[]
            foreach (byte value in data)
            {
                //storage for the individual byte
                string binarybyte = Convert.ToString(value, 2);
                //if the binarybyte is not 8 characters long, its not a proper result
                while (binarybyte.Length < 8)
                {
                    //prepend the value with a 0
                    binarybyte = "0" + binarybyte;
                }
                //append the binarybyte to the result
                result += binarybyte;
            }
            //return the result
            return result;
        }

        public static string[] NumberBoolToTrueFalseString(string toConvert)
        {
            List<char> temp = new List<char>();
            temp.AddRange(toConvert.ToList());
            List<int> temp2 = new List<int>();
            foreach (var item in temp)
            {
                temp2.Add((int)item);
            }
            List<string> temp3 = new List<string>();
            foreach (var item in temp2)
            {
                if (item==48)
                {
                    temp3.Add("False");
                }
                else
                {
                    temp3.Add("True");
                }
            }
            string[] stringArray = new string[temp3.Count];
            stringArray = temp3.ToArray<string>();
            return stringArray;
        }

        public static bool[] TrueFalseStringToBoolTable(string toConvert)
        {

            string[] stringTab = new string[toConvert.Length];
            stringTab = NumberBoolToTrueFalseString(toConvert);

            List<bool> boolList = new List<bool>();
            foreach (var item in stringTab)
            {
                boolList.Add(Convert.ToBoolean(item));
            }
            bool[] boolTab = new bool[boolList.Count()];
            boolTab = boolList.ToArray<bool>();
            return boolTab;
        }

        public static string TrueFalseTabToZeroOneTab(bool[] toConvert)
        {
            string toReturn = "";
            foreach (var item in toConvert)
            {
                if (item)
                {
                    toReturn += "1";
                }
                else
                {
                    toReturn += "0";
                }
            }
            return toReturn;
        }

        public static string StreamToString(Stream stream)
        {
            stream.Position = 0;
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }

        public static Stream StringToStream(string src)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(src);
            return new MemoryStream(byteArray);
        }
    }

}
