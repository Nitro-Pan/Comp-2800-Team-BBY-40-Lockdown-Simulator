﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEvent {
    public static List<EventFunction> listLockedEvents = new List<EventFunction>(5);
    [HideInInspector]
    public Dialogue dialogue;
    public float fHappinessGain;
    public float fInfectionRate;

    private enum EventLevel {
        BAD,
        OKAY,
        GOOD,
        EXCELLENT
    }

    private RandomEvent() {
        DayEndEvent e = DayEndEvent.GetEvent((EventLevel) Random.Range(0, System.Enum.GetValues(typeof(EventLevel)).Length), 100);

        dialogue = new Dialogue() {
            sName = e.sName,
        };
        dialogue.sentences = e.sentences;
        fHappinessGain = e.fHappinessGain;
        fInfectionRate = e.fInfectionGain;
    }

    private RandomEvent(float fHappiness, float fInfected) {
        //TODO: seed events with fHappiness and fResidents
        //so that it draws from a valid pool of events with
        //some fuzziness so it isn't too consistent
        float fTotalChance = 100;
        float fExcellentChance = fTotalChance - Mathf.Pow(2, (1 / 19f) * fHappiness);
        fTotalChance -= fExcellentChance;
        float fOkayChance = fTotalChance / 2;
        fTotalChance -= fOkayChance;
        float fGoodChance = fTotalChance / 2;
        fTotalChance -= fGoodChance;

        float fEventSelector = Random.Range(0f, 100f);

        DayEndEvent e;

        if (fEventSelector >= fExcellentChance) {
            e = DayEndEvent.GetEvent(EventLevel.EXCELLENT, fHappiness);
        } else if (fEventSelector >= fGoodChance) {
            e = DayEndEvent.GetEvent(EventLevel.GOOD, fHappiness);
        } else if (fEventSelector >= fOkayChance) {
            e = DayEndEvent.GetEvent(EventLevel.OKAY, fHappiness);
        } else if (fHappiness <= 20f) {
            e = DayEndEvent.GetEvent(EventLevel.BAD, fHappiness);
        } else {
            e = DayEndEvent.GetEvent(EventLevel.OKAY, fHappiness);
        }

        dialogue = new Dialogue() {
            sName = e.sName,
            sentences = e.sentences
        };
        fHappinessGain = e.fHappinessGain;
        fInfectionRate = e.fInfectionGain;
    }


    public static RandomEvent CreateEvent() {
        return new RandomEvent();
    }

    public static RandomEvent CreateSeededEvent(float fHappiness, float fInfected) {
        return new RandomEvent(fHappiness, fInfected);
    }

    private class DayEndEvent {
        public string[] sentences;
        public string sName;
        public float fHappinessGain;
        public float fInfectionGain;

        private DayEndEvent(string sName, float fHappinessGain, float fInfectionGain, params string[] sentences) {
            this.sName = sName;
            this.fHappinessGain = fHappinessGain;
            this.fInfectionGain = fInfectionGain;
            this.sentences = sentences;
        }

        private static List<DayEndEvent> dayEndEventsBad = new List<DayEndEvent>();
        private static List<DayEndEvent> dayEndEventsOkay = new List<DayEndEvent>();
        private static List<DayEndEvent> dayEndEventsGood = new List<DayEndEvent>();
        private static List<DayEndEvent> dayEndEventsExcellent = new List<DayEndEvent>();

        static DayEndEvent() {
            //bad events
            dayEndEventsBad.Add(new DayEndEvent("Murdered", 0, 100, 
                "You were sleeping peacefully when all of a sudden, you were attacked in your sleep!", 
                "You did not survive the encounter and unfortunately lost all of your residents because you died.",
                "Looks like you weren't such a good guy after all..."));
            dayEndEventsBad.Add(new DayEndEvent("Riot", 0, 20,
                "It's getting bad. People are restless, and they are no longer listening to reason.",
                "Rooms are being lit on fire, looting and violence has taken over the building."));
            dayEndEventsBad.Add(new DayEndEvent("Going Viral", -15, 0,
                "Videos of the terrible conditions in the building have gone viral, and not in a funny way.",
                "Hopefully this will all blow over quickly... like that 'Friday' girl."));
            //okay events
            dayEndEventsOkay.Add(new DayEndEvent("Rain", -0.7f, 0, 
                "It's raining today, so your residents aren't as happy as they normally would be."));
            dayEndEventsOkay.Add(new DayEndEvent("Silent Night", 0, 0,
                "Nothing happened tonight, lucky you. What does this mean for the next night...?"));
            dayEndEventsOkay.Add(new DayEndEvent("Construction", -5, 0,
                "There is loud construction going on outside.",
                "Nobody is happy about it..."));
            //good events
            dayEndEventsGood.Add(new DayEndEvent("Hello, Neighbour!", 0, 0,
                "One of your residents said hello to you today and it make you happy.", 
                "Nothing can go wrong now, can it?"));
            dayEndEventsGood.Add(new DayEndEvent("Pie!", 0.2f, 0,
                "You found a freshly baked pie at your doorstep!",
                "You can feel the love that is baked right into it."));
            dayEndEventsGood.Add(new DayEndEvent("Government Assistance", 1f, -0.2f,
                "The local government hands out a one-time payment for everybody",
                "This will stimulate the economy. Or something like that."));
            //excellent events
            dayEndEventsExcellent.Add(new DayEndEvent("Shrines", 0, -1.2f,
                "Your residents have made a shrine in your honor.", 
                "You don't know how to feel about their devotion, but if they're happy that's less for you to worry about."));
            dayEndEventsExcellent.Add(new DayEndEvent("Newsworthy!", 2, 0,
                "You are doing such a good job containing the virus, you made news headlines.",
                "Everyone is recognizing the great work you have done containing the virus."));
        }

        public static DayEndEvent GetEvent(EventLevel type, float fHappiness) {
            //if anything has locked an event to happen, randomly select
            if (listLockedEvents.Count > 0) {
                int index = Random.Range(0, listLockedEvents.Count);
                EventFunction ef = listLockedEvents[index];
                listLockedEvents.Clear();
                switch (ef) {
                    case EventFunction.WAVE_HELLO:
                        return new DayEndEvent("Hello, Neighbour!", 3, 0,
                            "One of your residents said hello to you today and it make you happy.",
                            "Doesn't waving to people from an acceptable distance make you feel better about yourself?");
                }
                return new DayEndEvent("Failed", 0, 0, 
                    "Failed to get an event from event queue",
                    "Event should be " + listLockedEvents[index]);
            }
            switch (type) {
                case EventLevel.BAD:
                    //make sure you can only be murdered if your residents are very unhappy
                    return dayEndEventsBad[Random.Range(0 + (fHappiness > 5f ? 1 : 0), dayEndEventsBad.Count)];
                case EventLevel.OKAY:
                    return dayEndEventsOkay[Random.Range(0, dayEndEventsOkay.Count)];
                case EventLevel.GOOD:
                    return dayEndEventsGood[Random.Range(0, dayEndEventsGood.Count)];
                case EventLevel.EXCELLENT:
                    return dayEndEventsExcellent[Random.Range(0, dayEndEventsExcellent.Count)];
            }
            return new DayEndEvent("Failed", 0, 0, "Failed to get an event");
        }
    }
}
