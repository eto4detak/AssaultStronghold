using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Feature : MonoBehaviour
{
    public FeatureData data;
    public float remainingTime;
    public bool isLive = true;
    public UnityAction ended;

    private void Update()
    {
        if(data.ch_time == CharacteristicTime.Temporarily)
        {
            UseTime();
        }
    }

    public void UseTime()
    {
        remainingTime -= Time.deltaTime;
        if (remainingTime <= 0)
        {
            isLive = false;
            OnDead();
        }
    }

    private void OnDead()
    {
        ended?.Invoke();
        Destroy(gameObject);
    }

    public static Feature GetFeature(FeatureName item)
    {
        return Resources.Load<Feature>("Feature/" + item.ToString());
    }
}




public enum FeatureName
{
    LostUnit,
    First,
    Storm,
    Cheer,
    ChaosFlame,
    IceArrow,
}

public enum CharacteristicType
{
    Speed,
    Attack,
}

public enum CharacteristicTime
{
    Constantly,
    Temporarily,
}