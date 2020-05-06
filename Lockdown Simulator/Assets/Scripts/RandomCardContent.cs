using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCardContent {

    public int nCardCost;
    public string sCardName;
    public string sCardContent;
    public string sImagePath;

    private RandomCardContent() {
        CardContent card = CardContent.GetCard(Random.Range(1, 11));
        nCardCost = card.nCardCost;
        sCardName = card.sCardName;
        sCardContent = card.sCardContent;
        sImagePath = card.sImagePath;
    }
    private RandomCardContent(float fHappiness, float fResident) {
        //TODO: use a formula to determine what card to draw
        CardContent card = CardContent.GetCard(Random.Range(1, 11));
        nCardCost = card.nCardCost;
        sCardName = card.sCardName;
        sCardContent = card.sCardContent;
        sImagePath = card.sImagePath;
    }

    public static RandomCardContent GenerateRandomCard() {
        return new RandomCardContent();
    }
    public static RandomCardContent GenerateRandomSeededCard(float fHappiness, float fResident) {
        return new RandomCardContent(fHappiness, fResident);
    }

    private class CardContent {
        public int nCardCost;
        public string sCardName;
        public string sCardContent;
        public string sImagePath;

        private CardContent(int nCardCost, string sCardName, string sCardContent, string sImagePath) {
            this.nCardCost = nCardCost;
            this.sCardName = sCardName;
            this.sCardContent = sCardContent;
            this.sImagePath = sImagePath;
        }

        private static List<CardContent> oneCostCards =     new List<CardContent>();
        private static List<CardContent> twoCostCards =     new List<CardContent>();
        private static List<CardContent> threeCostCards =   new List<CardContent>();
        private static List<CardContent> fourCostCards =    new List<CardContent>();
        private static List<CardContent> fiveCostCards =    new List<CardContent>();
        private static List<CardContent> sixCostCards =     new List<CardContent>();
        private static List<CardContent> sevenCostCards =   new List<CardContent>();
        private static List<CardContent> eightCostCards =   new List<CardContent>();
        private static List<CardContent> nineCostCards =    new List<CardContent>();
        private static List<CardContent> tenCostCards =     new List<CardContent>();

        static CardContent() {
            //One cost cards
            oneCostCards.Add(new CardContent(1, "Pet Dog", 
                "You pet a dog. It seems to be happy, but what does it mean under those deep, terrifying eyes?", 
                "Cards/Pet_Dog_Card"));
            //two cost cards
            twoCostCards.Add(new CardContent(2, "Say Hello!",
                "You wave and say hello to a resident. They like this and will probably wave back in the future.",
                "Cards/Wave_Card"));
            //three cost cards
            threeCostCards.Add(new CardContent(3, "Shopping",
                "It's that time of the week again, you need some supplies and shopping is the best way to fix that.",
                "Cards/Shopping_Card"));
            threeCostCards.Add(new CardContent(3, "Face Masks",
                "You found some facemasks at the store!. You decide to give them out to your residents.",
                "Cards/Face_Mask_Card"));
            //four cost cards
            fourCostCards.Add(new CardContent(4, "Condemned",
                "Condemn a resident. They probably won't like it much, but now they can't infect anyone at least.",
                "Cards/Condemn_Card"));
            //five cost cards
            fiveCostCards.Add(new CardContent(5, "Dinner",
                "You've gone out for a lovely dinner, but at what cost?",
                "Cards/Dinner_Card"));
            //six cost cards
            sixCostCards.Add(new CardContent(6, "Help!",
                "Out of the goodness of your heart, you help a resident. What if they were infected?",
                "Cards/Help_Card"));
            //seven cost cards
            sevenCostCards.Add(new CardContent(7, "A Stray?",
                "There's a stray rat here but it looks like it has wings. Maybe a new pet to keep you company?",
                "Cards/Stray_Bat_Card"));
            //eight cost cards
            eightCostCards.Add(new CardContent(8, "Quarantine",
                "The most effective way to keep everyone safe is to keep them separate. Hopefully they don't hate you too much.",
                "Cards/Quarantine_Card"));
            //nine cost cards
            nineCostCards.Add(new CardContent(9, "A New Pet",
                "You think it's time that you had something to keep you company in your sad, lonely life.",
                "Cards/New_Pet_Card"));
            //ten cost cards
            tenCostCards.Add(new CardContent(10, "Execution",
                "Execute one of your sick villagers. You might slow down the infection, but people won't like it.",
                "Cards/Axe_Card"));
        }

        public static CardContent GetCard(int nCost) {
            switch (nCost) {
                case 1:
                    return oneCostCards[Random.Range(0, oneCostCards.Count)];
                case 2:
                    return twoCostCards[Random.Range(0, twoCostCards.Count)];
                case 3:
                    return threeCostCards[Random.Range(0, threeCostCards.Count)];
                case 4:
                    return fourCostCards[Random.Range(0, fourCostCards.Count)];
                case 5:
                    return fiveCostCards[Random.Range(0, fiveCostCards.Count)];
                case 6:
                    return sixCostCards[Random.Range(0, sixCostCards.Count)];
                case 7:
                    return sevenCostCards[Random.Range(0, sevenCostCards.Count)];
                case 8:
                    return eightCostCards[Random.Range(0, eightCostCards.Count)];
                case 9:
                    return nineCostCards[Random.Range(0, nineCostCards.Count)];
                case 10:
                    return tenCostCards[Random.Range(0, tenCostCards.Count)];
            }
            return new CardContent(0, "Fail", "Failed to get card", "Cards/Axe_Card");
        }
    }
}
