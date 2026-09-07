using System.Collections.Generic;

namespace WehrmachtEnigma;

public class WehrmachtEnigmaJson
{
    public List<int> chosenRotors { get; set; }
    public string reflectorPlate { get; set; }
    public PlugboardSettings plugboardSettings { get; set; }
    public RotorPositions rotorPositions { get; set; }
    public string[] message { get; set; }
}

public class PlugboardSettings
{
    public string plugboardFirstHalf { get; set; }
    public string plugboardSecondHalf { get; set; }
}

public class RotorPositions
{
    public int rotorOnePosition { get; set; }
    public int rotorTwoPosition { get; set; }
    public int rotorThreePosition { get; set; }
}