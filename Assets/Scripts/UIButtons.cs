using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadSceneAsync("Level1", LoadSceneMode.Single);
    }
}
