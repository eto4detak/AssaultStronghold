using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statistic : MonoBehaviour
{
    public List<CharacterManager> sportsmans = new List<CharacterManager>();

    #region Singleton
    static protected Statistic s_Instance;
    static public Statistic instance { get { return s_Instance; } }
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

    public void Setup(List<CharacterManager> _sportsmans)
    {
        sportsmans = new List<CharacterManager>(_sportsmans);
    }

    public CharacterManager GetLast()
    {
        SortByDistance();
        return sportsmans[sportsmans.Count - 1];
    }

    private void SortByDistance()
    {
        CharacterManager temp;
        for (int i = 0; i < sportsmans.Count; i++)
        {
            for (int j = i + 1 ; j < sportsmans.Count; j++)
            {
                if (sportsmans[i].mission.path.Count > sportsmans[j].mission.path.Count ||
                    ( (sportsmans[i].mission.path.Count ==  sportsmans[j].mission.path.Count)
                    && (sportsmans[i].movement.agent.remainingDistance > sportsmans[j].movement.agent.remainingDistance) ) )
                {
                    temp = sportsmans[i];
                    sportsmans[i] = sportsmans[j];
                    sportsmans[j] = temp;
                }
            }
        }
    }

    public List<CharacterManager> GetRivalForSportman(CharacterManager sportman)
    {
        SortByDistance();
        List < CharacterManager > list =  new List<CharacterManager>(sportsmans);
        list.Remove(sportman);
        return list;
    }

}
