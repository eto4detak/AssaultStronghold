using System;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    #region Singleton
    static protected SpriteManager s_Instance;
    static public SpriteManager instance { get { return s_Instance; } }
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

    public Sprite GetSprite<T>(T inst) where T : class
    {
        return null;
    }

}
