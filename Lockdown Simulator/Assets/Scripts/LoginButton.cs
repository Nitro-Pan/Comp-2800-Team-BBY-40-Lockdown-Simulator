using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using UnityEngine.Events;
using UnityEngine.UI;

public class LoginButton : MonoBehaviour {
    [SerializeField] private InputField _emailField;
    [SerializeField] private InputField _passwordField;
    [SerializeField] private Button _loginButton;
    private Coroutine _loginCoroutine;
    public UserLoginEvent OnUserLogin = new UserLoginEvent();
    public UserLoginFailedEvent OnUserLoginFailed = new UserLoginFailedEvent();

    private void Reset() {
        _loginButton = FindObjectOfType<Button>();
    }

    private void Start() {
        _emailField.onValueChanged.AddListener(HandleNewText);
        _passwordField.onValueChanged.AddListener(HandleNewText);
        _loginButton.onClick.AddListener(HandleLoginEventClicked);
        UpdateInteractible();
    }

    private void OnDestroy() {
        _loginButton.onClick.RemoveListener(HandleLoginEventClicked);
    }

    private void HandleLoginEventClicked() {
        if (_loginCoroutine == null) {
            _loginCoroutine = StartCoroutine(LoginUser(_emailField.text, _passwordField.text));
            UpdateInteractible();
        }
    }

    private void HandleNewText(string _) {
        UpdateInteractible();
    }

    private void UpdateInteractible() {
        _loginButton.interactable = _loginCoroutine == null && !string.IsNullOrEmpty(_emailField.text) && !string.IsNullOrEmpty(_passwordField.text);
    }

    private IEnumerator LoginUser(string email, string password) {
        var auth = FirebaseAuth.DefaultInstance;
        var loginTask = auth.SignInWithEmailAndPasswordAsync(email, password);

        yield return new WaitUntil(() => loginTask.IsCompleted);

        if (loginTask.Exception != null) {
            Debug.LogWarning($"Login task failed with {loginTask.Exception}");
            OnUserLoginFailed.Invoke(loginTask.Exception);
        } else {
            Debug.Log($"Successfully logged in user {loginTask.Result.Email} ");
            OnUserLogin.Invoke(loginTask.Result);
        }

        _loginCoroutine = null;
    }

    [System.Serializable]
    public class UserLoginEvent : UnityEvent<FirebaseUser> { }
    [System.Serializable]
    public class UserLoginFailedEvent : UnityEvent<System.AggregateException> { }

    public void SuccessfulUserLogin(FirebaseUser user) {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
