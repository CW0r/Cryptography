using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace WehrmachtEnigma;

// Disables warning for conversion of potential null value to non-nullable type
#pragma warning disable CS8600

public class JsonManipulation
{    
    public WehrmachtEnigmaMachine ReadJsonToEnigmaMachine(WehrmachtEnigmaJson enigmaJson)
    {
        WehrmachtEnigmaMachine wehrmachtEnigmaMachine = new WehrmachtEnigmaMachine();

        List<int> chosenRotors = ReadChosenRotors(enigmaJson.chosenRotors);
        List<Tuple<char, char>> plugboardSettings = ReadPlugboardSettings(enigmaJson.plugboardSettings);
        List<int> rotorStartPositions = ReadRotorPositions(enigmaJson.rotorPositions);

        wehrmachtEnigmaMachine.ChangeMachineSettings(chosenRotors, reflectorSet: enigmaJson.reflectorPlate, plugboardSettings, rotorStartPositions);
        return wehrmachtEnigmaMachine;
    }

    public WehrmachtEnigmaJson ReadJsonFile()
    {
        WehrmachtEnigmaJson enigmaJson = new WehrmachtEnigmaJson();

        using (StreamReader r = new StreamReader("..\\..\\..\\EnigmaJsonFile.json"))
        {
            string json = r.ReadToEnd();
            enigmaJson = JsonSerializer.Deserialize<WehrmachtEnigmaJson>(json);
        }

        return enigmaJson;
    }

    public string ReadMessageFromJson(WehrmachtEnigmaJson enigmaJson)
    {
        string message = string.Join("", enigmaJson.message);
        return message;
    }

    public void WriteEnigmaMachineToJson()
    {
        WehrmachtEnigmaJson json = new WehrmachtEnigmaJson();


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
