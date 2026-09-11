package com.cryptography.logic.enigma.army;

import com.cryptography.logic.Constants;
import com.cryptography.logic.Tuple2;
import com.cryptography.logic.enigma.EnigmaMachine;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;

public class ArmyEnigma extends EnigmaMachine {
    public final String[] RotorSets = {
            "EKMFLGDQVZNTOWYHXUSPAIBRCJ",
            "AJDKSIRUXBLHWTMCQGZNPYFVOE",
            "BDFHJLCPRTXVZNYEIWGAKMUSQO",
            "ESOVPZJAYQUIRHXLNFTGKDCMWB",
            "VZBRGITYUPSDNHLXAWMJQOFECK"
    };
    public final HashMap<String, String> ReflectorPlateSets = new HashMap<>(
            Map.of(
                    "A", "EJMZALYXVBWFCRQUONTSPIKHGD",
                    "B", "YRUHQSLDPXNGOKMIEBFZCWVJAT",
                    "C", "FVPJIAOYEDRZXWGCTKUQSBNMHL"
            )
    );

    public void changeMachineSettings(String chosenReflector, ArrayList<Tuple2<Character, Character>> plugboardSettings, int[] rotorStartPositions, int[] chosenRotors) {
        super.changeMachineSettings(ReflectorPlateSets.get(chosenReflector), plugboardSettings, rotorStartPositions, chosenRotors);

        for (int i : chosenRotors)
            _rotors.add(RotorSets[i]);
    }

    @Override
    protected char encryptLetter(char letter) {
        letter = _plugboardConnections.get(letter);

        for (int i = 0; i < _rotors.size(); i++) {
            String currentRotor = _rotors.get(i);
            int index = (Constants.AlphabetList.indexOf(letter) + _rotorPositions[i]) % 26;
            letter = currentRotor.charAt(index);
        }

        letter = _reflectorPlate.charAt(Constants.AlphabetList.indexOf(letter));

        for (int i = _rotors.size() - 1; i > -1; i--) {
            String currentRotor = _rotors.get(i);
            int index = ((currentRotor.indexOf(letter) - _rotorPositions[i]) + 26) % 26;
            letter = Constants.AlphabetString.charAt(index);
        }

        letter = _plugboardConnections.get(letter);

        rotateRotors();

        return letter;
    }
}
