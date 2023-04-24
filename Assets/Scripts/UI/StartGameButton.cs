using UnityEngine.SceneManagement;
using UnityEngine;

public class StartGameButton : MonoBehaviour
{
    public int sceneIndex;

    public void StartGame()
    {
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }
}
