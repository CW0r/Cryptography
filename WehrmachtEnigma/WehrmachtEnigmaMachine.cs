using System;
using System.Collections.Generic;

namespace WehrmachtEnigma;

// Disables warning for reference of a potential null values
#pragma warning disable CS8602

public class WehrmachtEnigmaMachine
{
    private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    public readonly string[] rotorSets = { "EKMFLGDQVZNTOWYHXUSPAIBRCJ", "AJDKSIRUXBLHWTMCQGZNPYFVOE", "BDFHJLCPRTXVZNYEIWGAKMUSQO", "JFPGQCNLSVAUEWKTORDMBZIXYH", "FRAOEGYVICSBWZKQPJXTHLUDMN" };
    private List<string> _rotors = new List<string>();
    private string? _reflectorPlate;
    private Dictionary<char, char>? _plugboardConnections;
    private List<int>? _rotorPositions;

    public void ChangeMachineSettings(List<int> chosenRotors, string reflectorSet, List<Tuple<char, char>> plugboardSettings, List<int> rotorStartPositions)
    {
        foreach (int index in chosenRotors)
        {
            _rotors.Add(rotorSets[index]);
        }
        _reflectorPlate = reflectorSet;
        _plugboardConnections = CreatePlugboard(plugboardSettings);
        _rotorPositions = rotorStartPositions;
    }

    private Dictionary<char, char> CreatePlugboard(List<Tuple<char, char>> plugboardSettings)
    {
        Dictionary<char, char> plugboard = new Dictionary<char, char>();
        foreach (char letter in Alphabet)
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
        _rotorPositions[0] = (_rotorPositions[0] + 1) % 26;
        if (_rotorPositions[0] != 0) return;

        _rotorPositions[1] = (_rotorPositions[1] + 1) % 26;
        if (_rotorPositions[1] != 0) return;

        _rotorPositions[2] = (_rotorPositions[2] + 1) % 26;
    }

    private char EncryptLetter(char letter)
    {
        letter = _plugboardConnections[letter];

        for (int i = 0; i < _rotors.Count; i++)
        {
            string currentRotor = _rotors[i];
            int index = ((Alphabet.IndexOf(letter)) + _rotorPositions[i]) % 26;
            letter = Convert.ToChar(currentRotor[index]);
        }

        letter = _reflectorPlate[Alphabet.IndexOf(letter)];

        for (int i = _rotors.Count - 1; i >= 0; i--)
        {
            string currentRotor = _rotors[i];
            int index = ((currentRotor.IndexOf(letter) - _rotorPositions[i]) + 26) % 26;
            letter = Alphabet[index];
        }

        letter = _plugboardConnections[letter];

        RotateRotors();

        return letter;
    }

    public List<string> EncryptedMessageSplit(string encryptedMessage)
    {
        List<string> encryptedMessageParts = new List<string>();
        string currentPart = "";
        foreach (char letter in encryptedMessage)
        {
            currentPart += letter;
            if (currentPart.Length == 5)
            {
                encryptedMessageParts.Add(currentPart);
                currentPart = "";
            }
        }
        if (currentPart.Length > 0)
        {
            encryptedMessageParts.Add(currentPart);
        }
        return encryptedMessageParts;
    }

    public List<string> EncryptMessage(string message)
    {
        List<char> encryptedCharacters = new List<char>();
        string encryptedMessage = "";

        foreach (char letter in message)
        {
            if (Alphabet.Contains(letter))
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

        List<string> encryptedMessageList = EncryptedMessageSplit(encryptedMessage);

        return encryptedMessageList;
    }

    public List<int> GetCurrentRotorPositions()
    {
        return _rotorPositions;
    }
}
