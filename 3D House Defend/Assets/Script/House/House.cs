using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    #region
    public static House instance;
    private void Awake()
    {
        instance = this;
    }



    #endregion

    public GameObject _HouseGameObject;



}