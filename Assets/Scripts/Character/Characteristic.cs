using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Characteristic : MonoBehaviour
{
    private float damage;
    private float bonusDamage;
    private float startDamage = 1f;
    [SerializeField]
    private float speed;
    private float bonusSpeed;
    [SerializeField]
    private float startSpeed = 1f;

    public List<Feature> features = new List<Feature>();
    public UnityAction<Feature> OnAddFeature;
    public UnityAction<Feature> OnRemoveFeature;
    public float Speed { get => speed; }

    private void Awake()
    {
        damage = startDamage;
        speed = startSpeed;
    }

    private void Update()
    {
        TickFeatures();
    }


    public bool ExistFeature(FeatureName f_name)
    {
        return features.Exists(x => x.data.featureName == f_name);
    }

    public void AddFeature(Feature newFeature)
    {
        if (features.Exists(x => x.data.featureName == newFeature.data.featureName)) return;
        features.Add(newFeature);
        UpdateCharacteristic();
        if (OnAddFeature != null)
        {
            OnAddFeature.Invoke(newFeature);
        }
    }

    public void RemoveFeature(Feature lost)
    {
        features.Remove(lost);
        UpdateCharacteristic();
        if (OnRemoveFeature != null)
        {
            OnRemoveFeature.Invoke(lost);
        }
    }
    private void TickFeatures()
    {
        for (int i = 0; i < features.Count; i++)
        {
            if(features[i] == null) RemoveFeature(features[i]);
        }
    }

    private void SetBonusSpeed(float bonus)
    {
        speed = startSpeed + bonus;
        if (speed < 0) speed = 0;
    }

    private void UpdateCharacteristic()
    {
        float newBonusSpeed = 0;
        for (int i = 0; i < features.Count; i++)
        {
            if (features[i].data.ch_type == CharacteristicType.Speed) newBonusSpeed += features[i].data.value;
        }
        SetBonusSpeed(newBonusSpeed);
    }


}
