using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Tower towerPrefab ;
    [SerializeField] private bool isPlaceable;
    public bool IsPlaceable { get => isPlaceable ;}
    GameObject towerParent;


    void Start() {
        towerParent = GameObject.Find("Tower");
     }
    void OnMouseDown() {

        if(IsPlaceable)
        {
            Tower Tower = towerPrefab.createTower(towerPrefab,transform);
            if(Tower == null) return;
            Tower.transform.parent = towerParent.transform;
            isPlaceable = false;
        }
    }    
}
