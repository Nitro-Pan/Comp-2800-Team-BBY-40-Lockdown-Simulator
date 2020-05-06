using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEvent {

    [HideInInspector]
    public Dialogue dialogue;
    public float fHappinessGain;
    public float fResidentGain;

    private enum EventLevel {
        BAD,
        OKAY,
        GOOD,
        EXCELLENT
    }

    private RandomEvent() {
        DayEndEvent e = DayEndEvent.GetEvent((EventLevel) Random.Range(0, System.Enum.GetValues(typeof(EventLevel)).Length));

        dialogue = new Dialogue() {
            sName = e.sName,
        };
        dialogue.sentences = e.sentences;
        fHappinessGain = e.fHappinessGain;
        fResidentGain = e.fResidentGain;
    }

    private RandomEvent(float fHappiness, float fResidents) {
        //TODO: seed events with fHappiness and fResidents
        //so that it draws from a valid pool of events with
        //some fuzziness so it isn't too consistent
        DayEndEvent e = DayEndEvent.GetEvent((EventLevel)Random.Range(0, System.Enum.GetValues(typeof(EventLevel)).Length));

        dialogue = new Dialogue() {
            sName = e.sName,
            sentences = e.sentences
        };
        fHappinessGain = e.fHappinessGain;
        fResidentGain = e.fResidentGain;
    }


    public static RandomEvent CreateEvent() {
        return new RandomEvent();
    }

    public static RandomEvent CreateSeededEvent(float fHappiness, float fResidents) {
        return new RandomEvent(fHappiness, fResidents);
    }

    private class DayEndEvent {
        public string[] sentences;
        public string sName;
        public float fHappinessGain;
        public float fResidentGain;

        private DayEndEvent(string sName, float fHappinessGain, float fResidentGain, params string[] sentences) {
            this.sName = sName;
            this.fHappinessGain = fHappinessGain;
            this.fResidentGain = fResidentGain;
            this.sentences = sentences;
        }


        private static List<DayEndEvent> dayEndEventsBad = new List<DayEndEvent>();
        private static List<DayEndEvent> dayEndEventsOkay = new List<DayEndEvent>();
        private static List<DayEndEvent> dayEndEventsGood = new List<DayEndEvent>();
        private static List<DayEndEvent> dayEndEventsExcellent = new List<DayEndEvent>();

        static DayEndEvent() {
            //bad events
            dayEndEventsBad.Add(new DayEndEvent("Murdered", -10000, -10000, 
                "You were sleeping peacefully when all of a sudden, you were attacked in your sleep!", 
                "You did not survive the encounter and unfortunately lost all of your residents because you died.",
                "Looks like you weren't such a good guy after all..."));
            dayEndEventsBad.Add(new DayEndEvent("Riot", -30, -10,
                "It's getting bad. People are restless, and they are no longer listening to reason.",
                "Rooms are being lit on fire, looting and violence has taken over the building."));
            //okay events
            dayEndEventsOkay.Add(new DayEndEvent("Rain", -1, 0, 
                "It's raining today, so your residents aren't as happy as they normally would be."));
            dayEndEventsOkay.Add(new DayEndEvent("Silent Night", 0, 0,
                "Nothing happened tonight, lucky you. What does this mean for the next night...?"));
            //good events
            dayEndEventsGood.Add(new DayEndEvent("Hello, Neighbour!", 3, 0,
                "One of your residents said hello to you today and it make you happy.", 
                "Nothing can go wrong now, can it?"));
            //excellent events
            dayEndEventsExcellent.Add(new DayEndEvent("Shrines", 15, 5,
                "Your residents have made a shrine in your honor.", 
                "You don't know how to feel about their devotion, but if they're happy that's less for you to worry about."));
        }

        public static DayEndEvent GetEvent(EventLevel type) {
            switch (type) {
                case EventLevel.BAD:
                    return dayEndEventsBad[Random.Range(0, dayEndEventsBad.Count)];
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
