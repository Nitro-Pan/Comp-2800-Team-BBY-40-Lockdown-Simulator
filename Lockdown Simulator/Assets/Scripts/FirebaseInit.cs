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

    public UnityEvent OnFirebaseInitialized = new UnityEvent();

    void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        } else {
            _instance = this;
            DontDestroyOnLoad(this);
        }

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            if (task.Exception != null) {
                Debug.LogError($"Failed to initialize Firebase with error {task.Exception}");
                return;
            }

            FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://lockdown-simulator.firebaseio.com/");

            OnFirebaseInitialized.Invoke();
        });
    }
}
