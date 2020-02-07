using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button buttonPref;
    [SerializeField] private GameObject levelPanel;
    [SerializeField] private GameObject contentLevelPanel;
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private TextMeshProUGUI levelTitle;

    #region Singleton
    static protected MainMenuManager s_Instance;
    static public MainMenuManager instance { get { return s_Instance; } }
    #endregion

    void Awake()
    {
        #region Singleton
        if (s_Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        s_Instance = this;
        #endregion
    }

    void Start()
    {
        SetTitleLevel();
        if (levelPanel) levelPanel.SetActive(false);
        SetupLevelsInPanel();
    }

    public void ShowMainMenu()
    {
        GMode.instance.PauseGame();
        pausePanel.gameObject.SetActive(false);
        menuPanel.gameObject.SetActive(true);
        if (levelPanel) levelPanel.SetActive(false);
        if (mainPanel) mainPanel.SetActive(true);

    }
    public void HideMainMenu()
    {
        menuPanel.gameObject.SetActive(false);
        pausePanel.gameObject.SetActive(true);
    }

    public void OnOpenLevelPanel()
    {
        if (levelPanel) levelPanel.SetActive(true);
        if (mainPanel) mainPanel.SetActive(false);
    }

    public void OnContinueGame()
    {
        HideMainMenu();
        bool isFirstScene = SceneManager.GetActiveScene().buildIndex == 0;
        if (isFirstScene)
        {
            LevelManager.instance.LoadLevel(LevelManager.instance.saved.pData.lastLevel);
        }
        else
        {
            GMode.instance.ContinueGame();
        }
    }

    public void OnExitGame()
    {
        Application.Quit();
    }

    public void OnRestartLevel()
    {
        LevelManager.instance.RestartLevel();

        //int level = SceneManager.GetActiveScene().buildIndex;
        //level = level > 0 ? level : 1;
        //SceneManager.LoadScene(level);
    }

    private void SetupLevelsInPanel()
    {
        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            Button button = Instantiate(buttonPref, contentLevelPanel.transform);
            button.gameObject.SetActive(true);
            button.GetComponentInChildren<TextMeshProUGUI>().text = "Level " + i.ToString();
        }
    }

    private void SetTitleLevel()
    {
        int level = LevelManager.instance.levelData.levelNumber;
        if(level < 1)
        {
            SaveLoad.GetInstance().Load();
            level = 0;
        }
        levelTitle.text = "Level " + level;
    }
}
