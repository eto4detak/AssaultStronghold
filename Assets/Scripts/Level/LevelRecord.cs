using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRecord
{
    public List<int> recordList = new List<int>();


    public float GetAverage()
    {
        if (recordList.Count == 0) return 0;
        int average = 0;
        for (int i = 0; i < recordList.Count; i++)
        {
            average += recordList[i];
        }
        return average / recordList.Count;
    }

}
public struct RoundRecord
{
    CharacterManager character;
}