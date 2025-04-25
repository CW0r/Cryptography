using System.Collections.Generic;

namespace Caeser_Shift
{
    class CaeserCipher
    {
        private readonly List<char> BaseAlphabet = new List<char> { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

        public string EncodingShift(string plaintText, int shiftValue)
        {
            char[] characters = plaintText.ToCharArray();
            char[] cipherText = new char[characters.Length];

            for (int i = 0; i < characters.Length; i++)
            {
                int index = (BaseAlphabet.IndexOf(characters[i]) + shiftValue) % 26;
                cipherText[i] = BaseAlphabet[index];
            }

            return CharArrayToString(cipherText);
        }

        public string DecodingShift(string cipherText, int shiftValue)
        {
            char[] characters = cipherText.ToCharArray();
            char[] plainText = new char[characters.Length];

            for (int i = 0; i < characters.Length; i++)
            {
                int index = (BaseAlphabet.IndexOf(characters[i]) - shiftValue) % 26;
                plainText[i] = BaseAlphabet[index];
            }

            return CharArrayToString(plainText);
        }

        private string CharArrayToString(char[] charArray)
        {
            string returnString = "";
            for (int i = 0; i < charArray.Length; i++)
            {
                returnString += charArray[i].ToString();
            }

            return returnString;
        }
    }
}
