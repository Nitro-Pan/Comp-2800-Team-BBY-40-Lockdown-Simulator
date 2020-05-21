using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using UnityEngine.Events;
using UnityEngine.UI;

public class RegistrationButton : MonoBehaviour {
    [SerializeField] private RegistrationUI _registrationFlow;
    [SerializeField] private Button _registrationButton;
    private Coroutine _registrationCoroutine;
    public UserRegisteredEvent OnUserRegistered = new UserRegisteredEvent();
    public UserRegistrationFailedEvent OnUserRegistrationFailed = new UserRegistrationFailedEvent();

    public DialogueManager dm;

    private void Reset() {
        _registrationFlow = FindObjectOfType<RegistrationUI>();
        _registrationButton = FindObjectOfType<Button>();
    }

    private void Start() {
        _registrationFlow.OnStateChanged.AddListener(HandleRegistrationStateChanged);
        _registrationButton.onClick.AddListener(HandleRegistrationEventClicked);

        UpdateInteractible();
    }

    private void OnDestroy() {
        _registrationFlow.OnStateChanged.RemoveListener(HandleRegistrationStateChanged);
        _registrationButton.onClick.RemoveListener(HandleRegistrationEventClicked);
    }

    private void UpdateInteractible() {
        _registrationButton.interactable = _registrationFlow.CurrentState == RegistrationUI.State.Ok && _registrationCoroutine == null;
    }

    private void HandleRegistrationStateChanged(RegistrationUI.State registrationState) {
        UpdateInteractible();
    }

    public void HandleRegistrationEventClicked() {
        if (_registrationCoroutine == null) {
            _registrationCoroutine = StartCoroutine(RegisterUser(_registrationFlow.Email, _registrationFlow.Password));
            UpdateInteractible();
        }
    }

    private IEnumerator RegisterUser(string email, string password) {
        var auth = FirebaseAuth.DefaultInstance;
        var registerTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);

        Debug.Log("Waiting for firebase servers");
        yield return new WaitUntil(() => registerTask.IsCompleted);
        Debug.Log("Got response from firebase servers");

        if (registerTask.Exception != null) {
            Debug.LogWarning($"Failed to register task with {registerTask.Exception}");
            switch (_registrationFlow.CurrentState) {
                case RegistrationUI.State.EnterEmail:
                    dm.StartDialogue(new Dialogue() {
                        sName = "Enter Email",
                        sentences = new string[] { "Enter your email" }
                    });
                    break;
                case RegistrationUI.State.EnterPassword:
                    dm.StartDialogue(new Dialogue() {
                        sName = "Enter Password",
                        sentences = new string[] { "Enter your password" }
                    });
                    break;
                case RegistrationUI.State.PasswordsDontMatch:
                    dm.StartDialogue(new Dialogue() {
                        sName = "Unmatched Passwords",
                        sentences = new string[] { "Make sure your passwords match" }
                    });
                    break;
                default:
                    dm.StartDialogue(new Dialogue() {
                        sName = "Error",
                        sentences = new string[] { $"Failed to register task with {registerTask.Exception}" }
                    });
                    break;
            }
            OnUserRegistrationFailed.Invoke(registerTask.Exception);
        } else {
            Debug.Log($"Successfully registered user {registerTask.Result.Email} ");
            OnUserRegistered.Invoke(registerTask.Result);
        }

        _registrationCoroutine = null;
    }

    [System.Serializable]
    public class UserRegisteredEvent : UnityEvent<FirebaseUser> { }
    [System.Serializable]
    public class UserRegistrationFailedEvent : UnityEvent<System.AggregateException> { }

    public void SuccessfulUserRegistration(FirebaseUser user) {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void FailedUserRegistration(System.AggregateException e) {
        switch (_registrationFlow.CurrentState) {
            case RegistrationUI.State.EnterEmail:
                dm.StartDialogue(new Dialogue() {
                    sName = "Enter Email",
                    sentences = new string[] { "Enter your email" }
                });
                break;
            case RegistrationUI.State.EnterPassword:
                dm.StartDialogue(new Dialogue() {
                    sName = "Enter Password",
                    sentences = new string[] { "Enter your password" }
                });
                break;
            case RegistrationUI.State.PasswordsDontMatch:
                dm.StartDialogue(new Dialogue() {
                    sName = "Unmatched Passwords",
                    sentences = new string[] { "Make sure your passwords match" }
                });
                break;
            default:
                dm.StartDialogue(new Dialogue() {
                    sName = "Error",
                    sentences = new string[] { $"Failed to register task with {e}" }
                });
                break;
        }
    }
}
