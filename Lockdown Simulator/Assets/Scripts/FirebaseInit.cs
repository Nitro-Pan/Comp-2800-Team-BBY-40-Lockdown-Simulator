using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Extensions;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine;
using UnityEngine.Events;

public class FirebaseInit : MonoBehaviour {
    private static FirebaseInit _instance;
    public DialogueManager dm;

    public UnityEvent OnFirebaseInitialized = new UnityEvent();

    void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(gameObject);
        } else {
            _instance = this;
            DontDestroyOnLoad(this);
        }

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            if (task.Exception != null) {
                Debug.LogError($"Failed to initialize Firebase with error {task.Exception}");
                dm.StartDialogue(new Dialogue() {
                    sName = "Error",
                    sentences = new string[] { $"Failed to initialize Firebase with error {task.Exception}" }
                });
                return;
            }

            FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://lockdown-simulator.firebaseio.com/");

            OnFirebaseInitialized.Invoke();
        });
    }
}
