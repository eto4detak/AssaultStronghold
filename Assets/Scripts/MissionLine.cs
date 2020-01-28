using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionLine : MonoBehaviour
{
    public List<Transform> points;
    private int layer;

    private void OnTriggerEnter(Collider other)
    {
        var sportsman = other.GetComponent<CharacterManager>();
        if(sportsman != null)
        {
            sportsman.mission.AtLine(this);
        }
    }

}
