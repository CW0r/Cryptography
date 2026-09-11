package com.cryptography.logic.enigma.navy;

import com.cryptography.logic.Constants;
import com.cryptography.logic.Tuple2;
import com.cryptography.logic.enigma.EnigmaMachine;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;

public class NavyEnigma extends EnigmaMachine {
    public final String[] MovingRotorSets = {
            "EKMFLGDQVZNTOWYHXUSPAIBRCJ",
            "AJDKSIRUXBLHWTMCQGZNPYFVOE",
            "BDFHJLCPRTXVZNYEIWGAKMUSQO",
            "ESOVPZJAYQUIRHXLNFTGKDCMWB",
            "VZBRGITYUPSDNHLXAWMJQOFECK",
            "JPGVOUMFYQBENHZRDKASXLICTW",
            "NZJHGRCXMYSWBOUFAIVLPEKQDT",
            "FKQHTLXOCBJSPDZRAMEWNIUYGV"
    };
    public final String[] StaticRotorSets = {
            "LEYJVCNIXWPBQMDRTAKZGFUHOS",
            "FSOKANUERHMBTIYCWLQPZXVGJD"
    };
    public final HashMap<String, String> ReflectorPlateSets = new HashMap<>(
            Map.of(
                    "A", "EJMZALYXVBWFCRQUONTSPIKHGD",
                    "B", "YRUHQSLDPXNGOKMIEBFZCWVJAT",
                    "C", "FVPJIAOYEDRZXWGCTKUQSBNMHL",
                    "Bt", "ENKQAUYWJICOPBLMDXZVFTHRGS",
                    "Ct", "RDOBJNTKVEHMLFCWZAXGYIPSUQ"
            )
    );
    private String _staticRotor;
    private int _staticRotorPosition;

    public void changeMachineSettings(String chosenReflector, ArrayList<Tuple2<Character, Character>> plugboardSettings, int[] movingRotorStartPositions, int[] chosenMovingRotors, int chosenStaticRotor, int staticRotorStartPosition) {
        super.changeMachineSettings(ReflectorPlateSets.get(chosenReflector), plugboardSettings, movingRotorStartPositions, chosenMovingRotors);

        for (int i : chosenMovingRotors)
            _rotors.add(MovingRotorSets[i]);

        _staticRotor = StaticRotorSets[chosenStaticRotor];
        _staticRotorPosition = staticRotorStartPosition;
    }

    @Override
    protected char encryptLetter(char letter) {
        letter = _plugboardConnections.get(letter);

        for (int i = 0; i < _rotors.size(); i++) {
            String currentRotor = _rotors.get(i);
            int index = (Constants.AlphabetList.indexOf(letter) + _rotorPositions[i]) % 26;
            letter = currentRotor.charAt(index);
        }

        letter = _staticRotor.charAt((Constants.AlphabetList.indexOf(letter) + _staticRotorPosition) % 26);

        letter = _reflectorPlate.charAt(Constants.AlphabetList.indexOf(letter));

        letter = Constants.AlphabetList.get((_staticRotor.indexOf(letter) - _staticRotorPosition + 26) % 26);

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
