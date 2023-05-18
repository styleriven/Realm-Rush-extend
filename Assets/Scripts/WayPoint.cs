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


    GridManager gridManager;
    Vector2Int coordinates = new Vector2Int();
    Pathfinder pathfinder ;
    private void Awake() {
        gridManager = FindAnyObjectByType<GridManager>();
        pathfinder = FindAnyObjectByType<Pathfinder>();
        
    }

    void Start() {
        towerParent = GameObject.Find("Tower");
        if(gridManager != null) {
            
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
            if (!IsPlaceable )
            {
                gridManager.BlockNode(coordinates);
            }
        }
     }
     
    void OnMouseDown() {

        if(gridManager.GetNode(coordinates).isWalkable && !pathfinder.WillBlockPath(coordinates))
        {
            bool isSuccessful = towerPrefab.CreateTower(towerPrefab, transform,towerParent);
            if(isSuccessful){
            gridManager.BlockNode(coordinates);
            pathfinder.NotifyReceiver();
            }
        }
        
    }    
}
