using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadSceneAsync("Level1", LoadSceneMode.Single);
    }

    public void Replay()
    {
        SceneManager.LoadSceneAsync("Level1", LoadSceneMode.Single);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
