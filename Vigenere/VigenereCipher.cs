using System;
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

        private Tuple<string, List<int>> RemoveSpaces(string message)
        {
            string messageWithoutSpaces = "";
            List<int> spaceIndices = new List<int>();
            int length = message.Length;
            for (int i = 0; i < length; i++)
            {
                if (message[i] == ' ')
                {
                    spaceIndices.Add(i);
                }
                else
                {
                    messageWithoutSpaces += message[i];
                }
            }

            return new Tuple<string, List<int>>(messageWithoutSpaces, spaceIndices);
        }

        private string InsertSpaces(string message, List<int> spacesIndices)
        {
            string messageWithSpaces = "";
            int messageIndex = 0;

            for (int i = 0; i < message.Length + spacesIndices.Count; i++)
            {
                if (spacesIndices.Contains(i))
                {
                    messageWithSpaces += " ";
                }
                else
                {
                    messageWithSpaces += message[messageIndex];
                    messageIndex++;
                }
            }

            return messageWithSpaces;
        }

        public string DecodeMessage(string cipherTextMessage, string cipherKey)
        {
            Tuple<string, List<int>> messageSpaceTuple = RemoveSpaces(cipherTextMessage);
            string cipherTextNoSpaces = messageSpaceTuple.Item1;
            List<int> spaceIndices = messageSpaceTuple.Item2;
            int keyLength = cipherKey.Length;
            string plainTextMesssage = "";
            for (int i = 0; i < cipherTextNoSpaces.Length; i++)
            {
                int keyIndex = i % keyLength;
                string decodedChar = DecodeCharacter(cipherTextNoSpaces[i], cipherKey[keyIndex]).ToString();
                plainTextMesssage += decodedChar;
            }

            return InsertSpaces(plainTextMesssage, spaceIndices);
        }

        private char DecodeCharacter(char cipherTextChar, char keyChar)
        {
            int keyValue = BaseAlphabet.IndexOf(keyChar);
            int index = ((BaseAlphabet.IndexOf(cipherTextChar) -  keyValue) + 26) % 26;
            char plainTextChar = BaseAlphabet[index];
            return plainTextChar;
        }
    }
}
