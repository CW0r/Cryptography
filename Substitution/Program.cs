using System;

namespace Substitution
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SubstitutionCipher substitutionCipher = new SubstitutionCipher();
            string? plaintextMessage, cipherKey;

            Console.WriteLine("Would you like to encode or decode a message?\n1 - Encode\n2 - Decode");
            int input = Convert.ToInt32(Console.ReadLine());
            switch (input)
            {
                case 1:
                    Console.WriteLine("\nInput the message you want to encode:");
                    plaintextMessage = Console.ReadLine();
                    Console.WriteLine("\nInput the cipher key");
                    cipherKey = Console.ReadLine();
                    string encodedMessage = substitutionCipher.EncodeMessage(plaintextMessage.ToUpper(), cipherKey.ToUpper());
                    Console.WriteLine($"\n\nThe encoded message is: {encodedMessage}");
                    break;
                case 2:
                    Console.WriteLine("\nInput the message you want to decode:");
                    encodedMessage = Console.ReadLine();
                    Console.WriteLine("\nInput the cipher key");
                    cipherKey = Console.ReadLine();
                    plaintextMessage = substitutionCipher.DecodeMessage(encodedMessage.ToUpper(), cipherKey.ToUpper());
                    Console.WriteLine($"\n\nThe plaintext message is: {plaintextMessage}");
                    break;
                default:
                    Console.WriteLine("I'm sorry, that's not an option");
                    throw new ArgumentOutOfRangeException();
            }
            Console.ReadKey();
        }
    }
}
