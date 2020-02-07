using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public float startDelay = 3f;
    public float endDelay = 3f;
    public GameObject[] spawnPoints;
    public CharacterManager[] sportsmans;
    public Text messageText;
    public List<MissionLine> path;
    public GameObject playerSportsman;
    public CharacterManager playerHunter;
    public List<GameObject> finishedSportsman = new List<GameObject>();

    private int roundNumber = 0;
    private WaitForSeconds startWait;
    private WaitForSeconds endWait;
    private GameObject roundWinner;
    private GameObject gameWinner;
    private LevelRecord records;
    #region Singleton
    static protected Level s_Instance;
    static public Level instance { get { return s_Instance; } }
    #endregion

    private void Awake()
    {
        #region Singleton
        if (s_Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        s_Instance = this;
        #endregion
        startWait = new WaitForSeconds(startDelay);
        endWait = new WaitForSeconds(endDelay);
        records = new LevelRecord();
    }


    public void StartRound()
    {
        StartCoroutine(GameLoop());
    }

    private IEnumerator GameLoop()
    {
        while (roundNumber < LevelManager.instance.levelData.partyCount)
        {
            yield return StartCoroutine(RoundStarting());
            yield return StartCoroutine(RoundPlaying());
            yield return StartCoroutine(RoundEnding());
        }
        CheckWinPlayer();
    }


    private void CheckWinPlayer()
    {
        if(records.GetAverage() <= 2)
            LevelManager.instance.LoadNextLevel();
        else
            LevelManager.instance.RestartLevel();
    }


    private IEnumerator RoundStarting()
    {
        ResetAllSportsmans();
        DisableSportsmanPath();
        roundNumber++;
        messageText.gameObject.SetActive(true);
        messageText.text = "Level " + LevelManager.instance.levelData.levelNumber;
        messageText.text += "    ROUND " + roundNumber;
        yield return startWait;
    }

    private IEnumerator RoundPlaying()
    {
        EnableSportmanPath();
        messageText.gameObject.SetActive(false);
        messageText.text = string.Empty;
        while (!FinishedPlayer())
        {
            yield return null;
        }
    }

    private IEnumerator RoundEnding()
    {
        records.recordList.Add( finishedSportsman.Count);
        roundWinner = null;
        roundWinner = GetRoundWinner();
        gameWinner = GetGameWinner();
        messageText.gameObject.SetActive(true);
        messageText.text = EndMessage();

        DisableSportsmanPath();
        yield return endWait;
    }

    private bool FinishedPlayer()
    {
        for (int i = 0; i < finishedSportsman.Count; i++)
        {
            if (finishedSportsman[i] == playerSportsman) return true;
        }
        return false;
    }

    private GameObject GetRoundWinner()
    {
        for (int i = 0; i < sportsmans.Length; i++)
        {

        }
        return null;
    }

    private GameObject GetGameWinner()
    {
        for (int i = 0; i < sportsmans.Length; i++)
        {

        }

        return null;
    }

    private string EndMessage()
    {
        string message = "Finihed!";
        //if (m_RoundWinner != null)
        //    message = m_RoundWinner.m_ColoredPlayerText + " WINS THE ROUND!";
        message += "\n\n\n\n";
        for (int i = 0; i < records.recordList.Count; i++)
        {
            message += records.recordList[i] + " \n";
        }
        //if (m_GameWinner != null)
        //    message = m_GameWinner.m_ColoredPlayerText + " WINS THE GAME!";

        return message;
    }

    private void ResetAllSportsmans()
    {
        for (int i = 0; i < sportsmans.Length; i++)
        {
            sportsmans[i].movement.Stop();
            sportsmans[i].transform.position = spawnPoints[i].transform.position;
        }
    }

    private void EnableSportmanPath()
    {
        for (int i = 0; i < sportsmans.Length; i++)
        {
            sportsmans[i].mission.ResetRound();
            sportsmans[i].mission.path = new List<MissionLine>(path);
        }
    }

    private void DisableSportsmanPath()
    {
        finishedSportsman.Clear();
        for (int i = 0; i < sportsmans.Length; i++)
        {
            sportsmans[i].mission.isFinish = false;
            sportsmans[i].mission.currentIndexPoint = 0;
            sportsmans[i].mission.path = new List<MissionLine>();
        }
    }
}
