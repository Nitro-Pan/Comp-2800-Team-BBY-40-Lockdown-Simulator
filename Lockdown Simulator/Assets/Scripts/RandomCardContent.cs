using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCardContent {

    public int nCardCost;
    public int nHappinessGain;
    public float fInfectionGain;
    public string sCardName;
    public string sCardContent;
    public string sImagePath;
    public EventFunction func;

    private RandomCardContent() {
        CardContent card = CardContent.GetCard(Random.Range(1, 11));
        nCardCost = card.nCardCost;
        nHappinessGain = card.nHappinessGain;
        fInfectionGain = card.fInfectionGain;
        sCardName = card.sCardName;
        sCardContent = card.sCardContent;
        sImagePath = card.sImagePath;
        func = card.func;
    }
    private RandomCardContent(float fHappiness, float fResident, int nDay) {
        //max cost increases every 4 days, starting at 3, with a min of 2
        //and a max of 11
        int maxCost = Mathf.Clamp(Mathf.FloorToInt((1 / 4f) * nDay + 3), 2, 11);

        //get a random card between 1 and the maxCost, exclusive
        CardContent card = CardContent.GetCard(Random.Range(1, maxCost));
        nCardCost = card.nCardCost;
        nHappinessGain = card.nHappinessGain;
        fInfectionGain = card.fInfectionGain;
        sCardName = card.sCardName;
        sCardContent = card.sCardContent;
        sImagePath = card.sImagePath;
    }

    public static RandomCardContent GenerateRandomCard() {
        return new RandomCardContent();
    }
    public static RandomCardContent GenerateRandomSeededCard(float fHappiness, float fResident, int nDay) {
        return new RandomCardContent(fHappiness, fResident, nDay);
    }

    private class CardContent {
        public int nCardCost;
        public int nHappinessGain;
        public float fInfectionGain;
        public string sCardName;
        public string sCardContent;
        public string sImagePath;
        public EventFunction func;

        private CardContent(int nCardCost, int nHappinessGain, float fInfectionGain, string sCardName, EventFunction func, string sCardContent, string sImagePath) {
            this.nCardCost = nCardCost;
            this.nHappinessGain = nHappinessGain;
            this.fInfectionGain = fInfectionGain;
            this.sCardName = sCardName;
            this.sCardContent = sCardContent;
            this.sImagePath = sImagePath;
            this.func = func;
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
            oneCostCards.Add(new CardContent(1, 1, 0, "Pet Dog", EventFunction.PET_DOG, 
                "You pet a dog. It seems to be happy, but what does it mean under those deep, terrifying eyes?", 
                "Cards/Pet_Dog_Card"));
            //two cost cards
            twoCostCards.Add(new CardContent(2, 3, 0, "Say Hello!", EventFunction.WAVE_HELLO,
                "You wave and say hello to a resident. They like this and will probably wave back in the future.",
                "Cards/Wave_Card"));
            //three cost cards
            threeCostCards.Add(new CardContent(3, 1, 0, "Shopping", EventFunction.SHOPPING,
                "It's that time of the week again, you need some supplies and shopping is the best way to fix that.",
                "Cards/Shopping_Card"));
            threeCostCards.Add(new CardContent(3, 5, 0, "Face Masks", EventFunction.FACE_MASKS,
                "You found some facemasks at the store!. You decide to give them out to your residents.",
                "Cards/Face_Mask_Card"));
            //four cost cards
            fourCostCards.Add(new CardContent(4, -2, -1f, "Condemned", EventFunction.CONDEMN,
                "Condemn a resident. They probably won't like it much, but now they can't infect anyone at least.",
                "Cards/Condemn_Card"));
            //five cost cards
            fiveCostCards.Add(new CardContent(5, -2, 0.3f, "Dinner", EventFunction.DINNER,
                "You've gone out for a lovely dinner, but at what cost?",
                "Cards/Dinner_Card"));
            //six cost cards
            sixCostCards.Add(new CardContent(6, 7, 1, "Help!", EventFunction.HELP_RESIDENT,
                "Out of the goodness of your heart, you help a resident. What if they were infected?",
                "Cards/Help_Card"));
            //seven cost cards
            sevenCostCards.Add(new CardContent(7, 5, 1.5f, "A Stray?", EventFunction.STRAY_BAT,
                "There's a stray rat here but it looks like it has wings. Maybe a new pet to keep you company?",
                "Cards/Stray_Bat_Card"));
            //eight cost cards
            eightCostCards.Add(new CardContent(8, -10, -10f, "Quarantine", EventFunction.QUARANTINE,
                "The most effective way to keep everyone safe is to keep them separate. Hopefully they don't hate you too much.",
                "Cards/Quarantine_Card"));
            //nine cost cards
            nineCostCards.Add(new CardContent(9, 15, 0.2f, "A New Pet", EventFunction.NEW_PET,
                "You think it's time that you had something to keep you company in your sad, lonely life.",
                "Cards/New_Pet_Card"));
            //ten cost cards
            tenCostCards.Add(new CardContent(10, -20, -15f, "Execution", EventFunction.EXECUTE,
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
            return new CardContent(0, 0, 0, "Fail", EventFunction.ERROR, "Failed to get card", "Cards/Axe_Card");
        }
    }
}
