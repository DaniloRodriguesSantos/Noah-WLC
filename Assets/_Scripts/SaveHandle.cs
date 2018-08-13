using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveHandle : MonoBehaviour {

    public string PrologoSceneName;

    public void SaveGame()
    {
        PlayerPrefs.SetString("SceneName", SceneManager.GetActiveScene().name);
    }

    public void LoadGame()
    {
        if (PlayerPrefs.HasKey("SceneName"))
        {
            SceneManager.LoadScene(PlayerPrefs.GetString("SceneName"));
        }
        else
        {
            SceneManager.LoadScene(PrologoSceneName);
        }

    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
}
