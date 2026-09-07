package com.cryptography.logic.enigma;

import com.cryptography.logic.Constants;
import com.cryptography.logic.Tuple2;

import java.util.ArrayList;
import java.util.HashMap;

public abstract class EnigmaMachine {
    protected ArrayList<String> _rotors = new ArrayList<>();
    protected String _reflectorPlate;
    protected HashMap<Character, Character> _plugboardConnections;
    protected Integer[] _rotorPositions;
    protected Integer[] _chosenRotors;

    protected void changeMachineSettings(String reflectorSet, ArrayList<Tuple2<Character, Character>> plugboardSettings, Integer[] rotorStartPositions, Integer[] chosenRotors) {
        _reflectorPlate = reflectorSet;
        _plugboardConnections = createPlugboard(plugboardSettings);
        _rotorPositions = rotorStartPositions;
        _chosenRotors = chosenRotors;
    }

    protected HashMap<Character, Character> createPlugboard(ArrayList<Tuple2<Character, Character>> plugboardSettings) {
        HashMap<Character, Character> plugboard = new HashMap<>();

        for (char letter : Constants.AlphabetList) {
            plugboard.put(letter, letter);
        }

        for (int i = 0; i < plugboardSettings.size(); i++) {
            char a = plugboardSettings.get(i).Item1();
            char b = plugboardSettings.get(i).Item2();
            plugboard.replace(a, b);
            plugboard.replace(b, a);
        }

        return plugboard;
    }

    protected void rotateRotors() {
        _rotorPositions[0] = (_rotorPositions[0] + 1) % 26;
        if (!isNotched(0)) return;

        _rotorPositions[1] = (_rotorPositions[1] + 1) % 26;
        if (!isNotched(1)) return;

        _rotorPositions[2] = (_rotorPositions[2] + 1) % 26;
    }

    public String encryptMessage(String message) {
        ArrayList<Character> encryptedCharacters = new ArrayList<>();
        StringBuilder encryptedMessage = new StringBuilder();

        for (char letter : message.toCharArray()) {
            if (Constants.AlphabetList.contains(letter)) {
                char encryptedLetter = encryptLetter(letter);
                encryptedMessage.append(encryptedLetter);
            }
            else
                encryptedMessage.append(letter);
        }

        return encryptedMessage.toString();
    }

    protected abstract char encryptLetter(char letter);

    protected boolean isNotched(int rotorIndex) {
        int rotor = _chosenRotors[rotorIndex];
        int position = _rotorPositions[rotorIndex];

        return switch (rotor) {
            case 0 -> position == 17;
            case 1 -> position == 5;
            case 2 -> position == 22;
            case 3 -> position == 10;
            case 4 -> position == 0;
            case 5, 6, 7 -> position == 0 || position == 13;
            default -> false;
        };
    }
}
