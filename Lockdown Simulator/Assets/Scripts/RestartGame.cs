using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour {
    [SerializeField]
    public string sSceneToLoad;

    public void Restart() {
        SceneManager.LoadScene(sSceneToLoad);
    }
}
