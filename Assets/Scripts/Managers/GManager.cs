using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GManager : MonoBehaviour
{
    public static float startPositionY = 5.5f;
    public static float deltaPositionY = 0.1f;
    public static float minPositionY = -0.1f;

    //  public static GMode gMode;

    public List<CharacterManager> units;
    public Statistic statistic;

    void Awake()
    {
        //gameHUD = (GameHUD)FindObjectOfType(typeof(GameHUD));
    }

    void Start()
    {
        statistic.Setup(units);
        Level.instance.StartRound();
        WeaponPanel.instance.SetTarget(Level.instance.playerHunter);
    }

    private void CheckLast()
    {
        CharacterManager last = statistic.GetLast();
        if (!last.characteristic.ExistFeature(FeatureName.LostUnit))
        {
            var instance = Instantiate(Feature.GetFeature(FeatureName.LostUnit));
            last.characteristic.AddFeature(instance);
        }
    }

}
