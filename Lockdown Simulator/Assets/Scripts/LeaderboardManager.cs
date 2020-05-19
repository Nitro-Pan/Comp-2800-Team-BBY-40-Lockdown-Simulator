using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Firebase.Database;
using Firebase.Auth;

public class LeaderboardManager : MonoBehaviour {
    private readonly string PLAYER_KEY = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
    private FirebaseDatabase _database;

    void Start() {
        _database = FirebaseDatabase.DefaultInstance;
    }

    public void SaveScore(float score) {
        UserData user = new UserData(score, FirebaseAuth.DefaultInstance.CurrentUser.Email);
        StartCoroutine(SaveAsync(user));
        //_database.GetReference(PLAYER_KEY).SetRawJsonValueAsync(JsonUtility.ToJson(score));
        //Debug.Log($"Saved score {score} to user {PLAYER_KEY}");
    }

    private IEnumerator SaveAsync(UserData user) {
        var saveTask = _database.GetReference(PLAYER_KEY).SetRawJsonValueAsync(JsonUtility.ToJson(user));
        Debug.Log("Saving score...");
        yield return new WaitUntil(() => saveTask.IsCompleted);
        Debug.Log($"Saved score {user.score} to user {PLAYER_KEY}.");
    }

    public async Task<bool> SaveExists() {
        var dataSnapshot = await _database.GetReference(PLAYER_KEY).GetValueAsync();
        return dataSnapshot.Exists;
    }

    public void EraseSave() {
        _database.GetReference(PLAYER_KEY).RemoveValueAsync();
    }

    private class UserData {
        public float score;
        public string name;

        public UserData(float score, string name) {
            this.score = score;
            this.name = name;
        }
    }
}
