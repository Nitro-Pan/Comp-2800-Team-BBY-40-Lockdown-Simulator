using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase.Auth;
using System;
using UnityEngine.UI;

public class DynamicLoadButton : MonoBehaviour {
    [SerializeField] private string sSceneToLoad;

    private Action<string> actButtonAction;

    private void Start() {
        FirebaseAuth.DefaultInstance.StateChanged += HandleAuthStateChanged;
        CheckUser();
    }

    private void OnDestroy() {
        FirebaseAuth.DefaultInstance.StateChanged -= HandleAuthStateChanged;
    }

    private void HandleAuthStateChanged(object sender, EventArgs e) {
        CheckUser();
    }

    private void CheckUser() {
        if (FirebaseAuth.DefaultInstance.CurrentUser == null) {
            GetComponentInChildren<Text>().text = "Login";
            actButtonAction = scene => {
                SceneManager.LoadScene(scene);
            };
        } else {
            GetComponentInChildren<Text>().text = "Logout";
            actButtonAction = text => {
                FirebaseAuth.DefaultInstance.SignOut();
            };
        }
    }

    public void HandleClick() {
        actButtonAction.Invoke(sSceneToLoad);
    }
}
