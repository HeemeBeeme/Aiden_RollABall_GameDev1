using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIButtons : MonoBehaviour
{
    private bool InSettings = false;
    public bool InSidebar = true;

    public GameObject SettingsMenuObj;
    public GameObject MainMenu;
    public GameObject SideBar;
    public GameObject SideBarClosedButton;

    public void TutorialOpen()
    {
        SceneManager.LoadSceneAsync("Level0", LoadSceneMode.Single);
    }

    public void Play()
    {
        SceneManager.LoadSceneAsync("Level1", LoadSceneMode.Single);
    }

    public void Replay()
    {
        SceneManager.LoadSceneAsync("Level1", LoadSceneMode.Single);
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync("Menu", LoadSceneMode.Single);
    }

    public void SideBarActivation()
    {
        if(InSidebar)
        {
            SideBar.SetActive(false);
            SideBarClosedButton.SetActive(true);
            InSidebar = false;
        }
        else
        {
            SideBar.SetActive(true);
            SideBarClosedButton.SetActive(false);
            InSidebar = true;
        }
    }

    public void SettingsMenu()
    {
        if(InSettings == false)
        {
            SettingsMenuObj.SetActive(true);
            MainMenu.SetActive(false);
            InSettings = true;
        }
        else
        {
            SettingsMenuObj.SetActive(false);
            MainMenu.SetActive(true);
            InSettings = false;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
