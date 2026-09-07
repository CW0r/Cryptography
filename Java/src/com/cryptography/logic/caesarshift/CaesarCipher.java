package com.cryptography.logic.caesarshift;

import com.cryptography.logic.Constants;

public class CaesarCipher {

    // Inputs: Uppercase String, integer; Output: Uppercase String
    public static String encodingShift(String plainText, int shiftValue) {
        char[] plainChars = plainText.toUpperCase().toCharArray();
        char[] cipherChars = new char[plainChars.length];

        for (int i = 0; i < plainChars.length; i++) {
            int index = (Constants.AlphabetList.indexOf(plainChars[i]) + shiftValue) % 26;
            cipherChars[i] = Constants.AlphabetList.get(index);
        }

        return charToString(cipherChars);
    }

    private static String charToString(char[] characters) {
        StringBuilder stringBuilder = new StringBuilder();
        for (char character : characters) {
            stringBuilder.append(character);
        }

        return stringBuilder.toString();
    }
}
