using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using MoreMountains.NiceVibrations;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    #region VARIABLES

    [SerializeField] GameObject settingsCanvas;
    [SerializeField] GameObject tutorialCanvas;
    [SerializeField] GameObject cam1;
    [SerializeField] GameObject cam2;
    [SerializeField] AudioMixer audioMixer;

    public bool gamePaused;
    #endregion

    #region METHODS
    private void Awake()
    {
        instance = this;
    }
    public void OpenSettings()
    {
        settingsCanvas.SetActive(true);
        PauseGame();
    }
    public void CloseSettings()
    {
        settingsCanvas.SetActive(false);
        ResumeGame();
    }
    public void SetGameCamera(bool value)
    {
        if (value)
        {
            cam1.SetActive(true);
            cam2.SetActive(false);
        }
        else
        {
            cam1.SetActive(false);
            cam2.SetActive(true);
        }
    }
    public void SetGameSound(bool value)
    {
        if (value)
            audioMixer.SetFloat("mainVolume", 0);
        else
            audioMixer.SetFloat("mainVolume", -80);
    }
    public void SetGameVibration(bool value)
    {
        MMVibrationManager.SetHapticsActive(value);
    }
    public void OpenCloseTutorial(bool state)
    {
        tutorialCanvas.SetActive(true);
    }
    void PauseGame()
    {
        gamePaused = true;
        Time.timeScale = 0;
    }
    void ResumeGame()
    {
        gamePaused = false;
        Time.timeScale = 1;
    }
    #endregion
}
