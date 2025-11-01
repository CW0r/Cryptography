using System.Collections.Generic;

namespace Vigenere
{
    class VigenereCipher
    {
        private readonly List<char> BaseAlphabet = new List<char> { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

        public string EncodeMessage(string plainTextMessage, string cipherKey)
        {
            int keyLength = cipherKey.Length;
            string cipherTextMessage = "";
            for (int i = 0; i < plainTextMessage.Length; i++)
            {
                int keyIndex = i % keyLength;
                if (plainTextMessage[i] == ' ')
                {
                    cipherTextMessage += " ";
                }
                else
                {
                    string encodedChar = EncodeCharacter(plainTextMessage[i], cipherKey[keyIndex]).ToString();
                    cipherTextMessage += encodedChar;
                }
            }

            return cipherTextMessage;
        }

        private char EncodeCharacter(char plainTextChar, char keyChar)
        {
            int keyValue = BaseAlphabet.IndexOf(keyChar);
            int index = (BaseAlphabet.IndexOf(plainTextChar) + keyValue) % 26;
            char cipherTextChar = BaseAlphabet[index];
            return cipherTextChar;
        }

        public string DecodeMessage(string cipherTextMessage, string cipherKey)
        {
            int keyLength = cipherKey.Length;
            string plainTextMesssage = "";
            for (int i = 0; i < cipherTextMessage.Length; i++)
            {
                int keyIndex = i % keyLength;
                if (cipherTextMessage[i] == ' ')
                {
                    plainTextMesssage += " ";
                }
                else
                {
                    string decodedChar = DecodeCharacter(cipherTextMessage[i], cipherKey[keyIndex]).ToString();
                    plainTextMesssage += decodedChar;
                }
            }

            return plainTextMesssage;
        }

        private char DecodeCharacter(char cipherTextChar, char keyChar)
        {
            int keyValue = BaseAlphabet.IndexOf(keyChar);
            int index = (BaseAlphabet.IndexOf(cipherTextChar) -  keyValue) % 26;
            char plainTextChar = BaseAlphabet[index];
            return plainTextChar;
        }
    }
}
