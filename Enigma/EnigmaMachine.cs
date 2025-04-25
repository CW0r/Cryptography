using System;
using System.Collections.Generic;

namespace Enigma;

// Disables warning for reference of a potential null values
#pragma warning disable CS8602

public class EnigmaMachine
{
    private const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    public readonly string[] rotorSets = { "EKMFLGDQVZNTOWYHXUSPAIBRCJ", "AJDKSIRUXBLHWTMCQGZNPYFVOE", "BDFHJLCPRTXVZNYEIWGAKMUSQO", "JFPGQCNLSVAUEWKTORDMBZIXYH", "FRAOEGYVICSBWZKQPJXTHLUDMN" };
    private List<string> rotors = new List<string>();
    private string? reflectorPlate;
    private Dictionary<char, char>? plugboardConnections;
    private List<int>? rotorPositions;

    public void ChangeMachineSettings(List<int> chosenRotors, string reflectorSet, List<Tuple<char, char>> plugboardSettings, List<int> rotorStartPositions)
    {
        foreach (int index in chosenRotors)
        {
            rotors.Add(rotorSets[index]);
        }
        reflectorPlate = reflectorSet;
        plugboardConnections = CreatePlugboard(plugboardSettings);
        rotorPositions = rotorStartPositions;
    }

    private Dictionary<char, char> CreatePlugboard(List<Tuple<char, char>> plugboardSettings)
    {
        Dictionary<char, char> plugboard = new Dictionary<char, char>();
        foreach (char letter in alphabet)
        {
            plugboard.Add(letter, letter);
        }

        for (int i = 0; i < plugboardSettings.Count; i++)
        {
            char a = plugboardSettings[i].Item1;
            char b = plugboardSettings[i].Item2;
            plugboard[a] = b;
            plugboard[b] = a;
        }

        return plugboard;
    }

    private void RotateRotors()
    {
        rotorPositions[0] = (rotorPositions[0] + 1) % 26;
        if (rotorPositions[0] != 0) return;

        rotorPositions[1] = (rotorPositions[1] + 1) % 26;
        if (rotorPositions[1] != 0) return;

        rotorPositions[2] = (rotorPositions[2] + 1) % 26;
    }

    private char EncryptLetter(char letter)
    {
        letter = plugboardConnections[letter];

        for (int i = 0; i < rotors.Count; i++)
        {
            string currentRotor = rotors[i];
            int index = ((alphabet.IndexOf(letter)) + rotorPositions[i]) % 26;
            letter = Convert.ToChar(currentRotor[index]);
        }

        letter = reflectorPlate[alphabet.IndexOf(letter)];

        for (int i = rotors.Count - 1; i >= 0; i--)
        {
            string currentRotor = rotors[i];
            int index = ((currentRotor.IndexOf(letter) - rotorPositions[i]) + 26) % 26;
            letter = alphabet[index];
        }

        letter = plugboardConnections[letter];

        RotateRotors();

        return letter;
    }

    public string EncryptMessage(string message)
    {
        List<char> encryptedCharacters = new List<char>();
        string encryptedMessage = "";

        foreach (char letter in message)
        {
            if (alphabet.Contains(letter))
            {
                char encryptedLetter = EncryptLetter(letter);
                encryptedCharacters.Add(encryptedLetter);
            }
            else
            {
                encryptedCharacters.Add(letter);
            }
        }

        foreach (char character in encryptedCharacters)
        {
            encryptedMessage += character;
        }

        return encryptedMessage;
    }
}
