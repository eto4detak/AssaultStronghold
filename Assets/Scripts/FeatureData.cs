using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New FeatureData", menuName = "FeatureData", order = 52)]
public class FeatureData : ScriptableObject
{
    public Sprite icon;
    public FeatureName featureName;
    public CharacteristicType ch_type;
    public CharacteristicTime ch_time;
    public float value;
}