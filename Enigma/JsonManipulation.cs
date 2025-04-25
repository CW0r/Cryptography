using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Enigma;

// Disables warning for conversion of potential null value to non-nullable type
#pragma warning disable CS8600

public class JsonManipulation
{    
    public EnigmaMachine ReadJsonToEnigmaMachine(EnigmaJson enigmaJson)
    {
        EnigmaMachine enigmaMachine = new EnigmaMachine();

        List<int> chosenRotors = ReadChosenRotors(enigmaJson.chosenRotors);
        List<Tuple<char, char>> plugboardSettings = ReadPlugboardSettings(enigmaJson.plugboardSettings);
        List<int> rotorStartPositions = ReadRotorPositions(enigmaJson.rotorPositions);

        enigmaMachine.ChangeMachineSettings(chosenRotors, reflectorSet: enigmaJson.reflectorPlate, plugboardSettings, rotorStartPositions);
        return enigmaMachine;
    }

    public EnigmaJson ReadJsonFile()
    {
        EnigmaJson enigmaJson = new EnigmaJson();

        using (StreamReader r = new StreamReader("..\\..\\..\\EnigmaJsonFile.json"))
        {
            string json = r.ReadToEnd();
            enigmaJson = JsonSerializer.Deserialize<EnigmaJson>(json);
        }

        return enigmaJson;
    }

    public string ReadMessageFromJson(EnigmaJson enigmaJson)
    {
        string message = string.Join("", enigmaJson.message);
        return message;
    }

    public void WriteEnigmaMachineToJson()
    {
        EnigmaJson json = new EnigmaJson();


        string jsonString = JsonSerializer.Serialize(json, new JsonSerializerOptions { WriteIndented = true });
        using (StreamWriter outputFile = new StreamWriter("EnigmaJsonFile.json"))
        {
            outputFile.WriteLine(jsonString);
        }
    }

    private List<Tuple<char, char>> ReadPlugboardSettings(PlugboardSettings plugboardLetters)
    {
        string plugboardFirstHalf = plugboardLetters.plugboardFirstHalf;
        string plugboardSecondHalf = plugboardLetters.plugboardSecondHalf;
        List<Tuple<char, char>> plugboardSettings = new List<Tuple<char, char>>();

        for (int i = 0; i < plugboardFirstHalf.Length; i++)
        {
            plugboardSettings.Add(new Tuple<char, char>(plugboardFirstHalf[i], plugboardSecondHalf[i]));
        }

        return plugboardSettings;
    }

    private List<int> ReadRotorPositions(RotorPositions rotorPositions)
    {
        return new List<int>()
        {
            rotorPositions.rotorOnePosition - 1,
            rotorPositions.rotorTwoPosition - 1,
            rotorPositions.rotorThreePosition - 1
        };
    }

    private List<int> ReadChosenRotors(List<int> chosenRotors)
    {
        for (int i = 0; i < chosenRotors.Count; i++)
        {
            chosenRotors[i] -= 1;
        }

        return chosenRotors;
    }
}
