using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneralManager : MonoBehaviour
{
    public static GeneralManager Instance;

    public GameObject fullscreenButton;

    public Sprite fullscreenIcon;
    public Sprite minimizeIcon;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (Screen.fullScreen)
        {
            GoFullScreen();
        }
        else
        {
            Minimize();
        }
    }

    public void ToggleFullScreen() {
        if (Screen.fullScreen)
        {
            Minimize();
        }
        else
        {
            GoFullScreen();
        }
    }

    private void GoFullScreen()
    {
        Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
        fullscreenButton.GetComponent<Image>().sprite = minimizeIcon;
    }

    private void Minimize()
    {
        Screen.SetResolution(960, 540, false);
        fullscreenButton.GetComponent<Image>().sprite = fullscreenIcon;
    }
}
