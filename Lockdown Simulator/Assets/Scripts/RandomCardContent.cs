using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCardContent {

    public int nCardCost;
    public string sCardName;
    public string sCardContent;

    private RandomCardContent() {

    }
    private RandomCardContent(float fHappiness, float fResident) {

    }

    private class CardContent {
        public CardContent() {

        }
    }

    public static RandomCardContent GenerateRandomCard() {
        return new RandomCardContent();
    }
    public static RandomCardContent GenerateRandomSeededCard(float fHappiness, float fResident) {
        return new RandomCardContent(fHappiness, fResident);
    }
}
