using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEvent {

    [HideInInspector]
    public Dialogue dialogue;

    private enum EventLevel {
        BAD,
        OKAY,
        GOOD,
        EXCELLENT
    }

    private RandomEvent() {
        DayEndEvent e = new DayEndEvent((EventLevel) Random.Range(0, System.Enum.GetValues(typeof(EventLevel)).Length));

        dialogue = new Dialogue() {
            sName = e.sName,
        };
        dialogue.sentences = e.sentences;
    }

    private class DayEndEvent {
        public string[] sentences;
        public string sName;
        public float fHappinessGain;
        public float fResidentGain;

        public DayEndEvent(EventLevel eventLevel) {
            switch (eventLevel) {
                case EventLevel.BAD:
                    sName = "Bad Event";
                    sentences = new string[] { "Uh oh, looks like you just got a bad event.", "You died a painful death" };
                    fHappinessGain = -10;
                    break;
                case EventLevel.OKAY:
                    sName = "Okay Event";
                    sentences = new string[] { "This is okay, everything is fine.", "For now, at least. Hopefully it is okay." };
                    fHappinessGain = 0;
                    break;
                case EventLevel.GOOD:
                    sName = "Good Event";
                    sentences = new string[] { "This is actually good, your residents seem to like you", "Don't fuck up too bad now, okay?" };
                    fHappinessGain = 2;
                    break;
                case EventLevel.EXCELLENT:
                    sName = "Excellent Event";
                    sentences = new string[] { "Wow, an excellent event!", "Go do something outside, maybe. You need to spend time away from games." };
                    fHappinessGain = 10;
                    break;
            }
        }
    }


    public static RandomEvent CreateEvent() {
        return new RandomEvent();
    }
}
