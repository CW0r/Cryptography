using System;

namespace Caeser_Shift
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CaeserCipher substitutionCipher = new CaeserCipher();
            string? plaintextMessage;
            int cipherShift;

            Console.WriteLine("Would you like to encode or decode a message?\n1 - Encode\n2 - Decode");
            int input = Convert.ToInt32(Console.ReadLine());
            switch (input)
            {
                case 1:
                    Console.WriteLine("\nInput the message you want to encode:");
                    plaintextMessage = Console.ReadLine();
                    Console.WriteLine("\nInput the cipher key");
                    cipherShift = Convert.ToInt32(Console.ReadLine());
                    string encodedMessage = substitutionCipher.EncodingShift(plaintextMessage.ToUpper(), cipherShift);
                    Console.WriteLine($"\n\nThe encoded message is: {encodedMessage}");
                    break;
                case 2:
                    Console.WriteLine("\nInput the message you want to decode:");
                    encodedMessage = Console.ReadLine();
                    Console.WriteLine("\nInput the cipher key");
                    cipherShift = Convert.ToInt32(Console.ReadLine());
                    plaintextMessage = substitutionCipher.DecodingShift(encodedMessage.ToUpper(), cipherShift);
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
