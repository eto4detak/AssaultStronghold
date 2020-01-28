using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabHelper : MonoBehaviour
{
    #region Singleton
    static protected PrefabHelper s_Instance;
    static public PrefabHelper instance { get { return s_Instance; } }
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

    public ParticleSystem GetSpellPrefab(SpellName sName)
    {
        if(sName == SpellName.ChaosFlame)
        {
             return Resources.Load<ParticleSystem>("Prefabs/" + sName.ToString());

        }
        return null;
    }

}


public enum SpellName
{
    ChaosFlame,
}