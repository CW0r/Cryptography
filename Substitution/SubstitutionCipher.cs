using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Substitution
{
    class SubstitutionCipher
    {
        private readonly List<char> BaseAlphabet = new List<char> { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        
        public string EncodeMessage(string plainTextMessage, string cipherKey)
        {
            List<char> KeyAlphabet = CreateKeyAlphabet(cipherKey);
            char[] plainText = plainTextMessage.ToCharArray();
            char[] cipherText = new char[plainText.Length];

            for (int i = 0; i < plainText.Length; i++)
            {
                int index = BaseAlphabet.IndexOf(plainText[i]);
                cipherText[i] = KeyAlphabet[index];
            }

            return CharArrayToString(cipherText);
        }

        public string DecodeMessage(string cipherTextMessage, string cipherKey)
        {
            List<char> KeyAlphabet = CreateKeyAlphabet(cipherKey);
            char[] cipherText = cipherTextMessage.ToCharArray();
            char[] plainText = new char[cipherText.Length];

            for (int i = 0; i < cipherText.Length; i++)
            {
                int index = KeyAlphabet.IndexOf(cipherText[i]);
                plainText[i] = BaseAlphabet[index];
            }

            return CharArrayToString(plainText);
        }

        private List<char> CreateKeyAlphabet(string cipherKey)
        {
            List<char> cipherKeyList = new List<char>();
            char[] keyElements = cipherKey.ToCharArray();
            foreach (char keyChar in keyElements)
            {
                cipherKeyList.Add(keyChar);
            }

            return cipherKeyList;
        }

        private string CharArrayToString(char[] charArray)
        {
            string returnString = "";
            foreach (char character in charArray)
            {
                returnString += character.ToString();
            }

            return returnString;
        }
    }
}
