using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericSpace : MonoBehaviour
{

    #region Singleton
    static protected GenericSpace s_Instance;
    static public GenericSpace instance { get { return s_Instance; } }
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

}
