using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statistic : MonoBehaviour
{
    public List<CharacterManager> units = new List<CharacterManager>();

    public void Setup(List<CharacterManager> targets)
    {
        units = new List<CharacterManager>( targets);
    }

    public CharacterManager GetLast()
    {
        SortByDistance();
        return units[units.Count - 1];
    }

    private void SortByDistance()
    {
        CharacterManager temp;
        for (int i = 0; i < units.Count; i++)
        {
            for (int j = i + 1 ; j < units.Count; j++)
            {
                if (units[i].mission.path.Count > units[j].mission.path.Count ||
                    ( (units[i].mission.path.Count ==  units[j].mission.path.Count)
                    && (units[i].movement.agent.remainingDistance > units[j].movement.agent.remainingDistance) ) )
                {
                    temp = units[i];
                    units[i] = units[j];
                    units[j] = temp;
                }
            }
        }
    }
}
