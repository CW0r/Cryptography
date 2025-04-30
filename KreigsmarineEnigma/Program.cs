using KriegsmarineEnigma;
using System;
using System.Collections.Generic;

#pragma warning disable CS8600, CS8602, CS8604

// PARTIAL ERROR WITH DECODING - REFLECTION OR PLUGBOARD

void ManualEnigma()
{
    KriegsmarineEnigmaMachine KriegsmarineEnigmaMachine = new KriegsmarineEnigmaMachine();
    // Choosing which three rotors to use
    Console.WriteLine("Which three rotors would you like to use?");
    for (int i = 0; i < KriegsmarineEnigmaMachine.rotorSets.Length; i++)
    {
        Console.WriteLine($"{i + 1} - {KriegsmarineEnigmaMachine.rotorSets[i]}");
    }
    List<int> chosenRotors = new List<int>()
    {
        Convert.ToInt32(Console.ReadLine()) - 1,
        Convert.ToInt32(Console.ReadLine()) - 1,
        Convert.ToInt32(Console.ReadLine()) - 1
    };
    Console.WriteLine();

    Console.WriteLine("Which static rotor would you like to use?");
    for (int i = 0; i < KriegsmarineEnigmaMachine.staticRotorSets.Length; i++)
    {
        Console.WriteLine($"{i + 1} - {KriegsmarineEnigmaMachine.staticRotorSets[i]}");
    }
    chosenRotors.Add(Convert.ToInt32(Console.ReadLine()) - 1);
    Console.WriteLine();

    // Setting the reflector plate
    string reflectorSet = SymmetricReflectorPlateSetter();


    // Choosing which letters to include in the plugboard
    Console.WriteLine("Select up to 10 letters to enter into the plugboard");
    char[] plugboardStart = Console.ReadLine().ToUpper().ToCharArray();
    Console.WriteLine($"Select {plugboardStart.Length} letters to swap them with");
    char[] plugboardEnd = Console.ReadLine().ToUpper().ToCharArray();
    List<Tuple<char, char>> plugboardSettings = new List<Tuple<char, char>>();
    for (int i = 0; i < plugboardStart.Length; i++)
    {
        plugboardSettings.Add(new Tuple<char, char>(plugboardStart[i], plugboardEnd[i]));
    }
    Console.WriteLine();


    // Choosing the rotation of each rotor
    Console.WriteLine("How many times should each rotor start rotated");
    List<int> rotorStartPositions = new List<int>()
    {
        Convert.ToInt32(Console.ReadLine()),
        Convert.ToInt32(Console.ReadLine()),
        Convert.ToInt32(Console.ReadLine())
    };
    Console.WriteLine();

    Console.WriteLine("How many times should the static rotor be rotated");
    rotorStartPositions.Add(Convert.ToInt32(Console.ReadLine()));

    KriegsmarineEnigmaMachine.ChangeMachineSettings(chosenRotors, reflectorSet, plugboardSettings, rotorStartPositions);


    Console.WriteLine("Enter your message:");
    string message = Console.ReadLine().ToUpper();
    string encryptedMessage = KriegsmarineEnigmaMachine.EncryptMessage(message);
    Console.WriteLine($"The encrypted form of your message is\n{encryptedMessage}");
}
;

string SymmetricReflectorPlateSetter()
{
    char[] reflectorPlate = new char[26];
    Console.WriteLine("Enter 13 distinct letters");
    char[] reflectorInputHalf = Console.ReadLine()
                                       .ToUpper()
                                       .ToCharArray();

    List<char> alphabet = new List<char> { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

    // Generate a list of unused letters of non-input letters
    List<char> reflectorUnusedLetters = new List<char>(alphabet);

    foreach (char letter in reflectorInputHalf)
    {
        reflectorUnusedLetters.Remove(letter);
    }


    for (int i = 0; i < reflectorInputHalf.Length; i++)
    {
        // Take indexes of next letter in each list, and add each letter to the reflector using the other letter's index
        int indexOne = alphabet.IndexOf(reflectorInputHalf[i]);
        int indexTwo = alphabet.IndexOf(reflectorUnusedLetters[i]);
        reflectorPlate[indexOne] = reflectorUnusedLetters[i];
        reflectorPlate[indexTwo] = reflectorInputHalf[i];
    }

    Console.WriteLine();
    return CharArrayToString(reflectorPlate);
}

string CharArrayToString(char[] charArray)
{
    string returnString = "";
    for (int i = 0; i < charArray.Length; i++)
    {
        returnString += charArray[i].ToString();
    }

    return returnString;
}

/* void JsonEnigma()
{
    JsonManipulation jsonManipulator = new JsonManipulation();
    WehrmachtEnigmaJson enigmaJson = jsonManipulator.ReadJsonFile();
    WehrmachtEnigmaMachine enigmaMachine = jsonManipulator.ReadJsonToEnigmaMachine(enigmaJson);
    string message = jsonManipulator.ReadMessageFromJson(enigmaJson);
    string encryptedMessage = enigmaMachine.EncryptMessage(message);
    Console.WriteLine($"The encrypted form of your message is\n{encryptedMessage}");
}
*/

ManualEnigma();
//JsonEnigma();