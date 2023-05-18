using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] List<Node> path = new List<Node>();
    [SerializeField] [Range(0f,5f)] float speed = 1f; 
    Enemy enemy ;
    GridManager gridManager ;
    Pathfinder pathfinder;
    void OnEnable() {
        
        RecalculatePath(true);
        ReturnToStart();
        transform.LookAt(gridManager.GetPositionFromCoordinates(path[1].coordinates));
        
    }
    void Awake() {
        enemy = GetComponent<Enemy>();
        gridManager = FindAnyObjectByType<GridManager>();
        pathfinder = FindAnyObjectByType<Pathfinder>();
    }

    void RecalculatePath(bool resetPath){
        Vector2Int coordinates = new Vector2Int();
        if (resetPath)
        {
            coordinates = pathfinder.StartCoordinates ;
        }
        else
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }
        StopAllCoroutines();
        path.Clear();
        path = pathfinder.GetNewPath(coordinates);
        StartCoroutine(FollowPath());
    }
    public void ReturnToStart()
    {
        transform.position = gridManager.GetPositionFromCoordinates(pathfinder.StartCoordinates);
    }
    void FinishPath()
    {
        enemy.StealGold();
        gameObject.SetActive(false);
    }
    IEnumerator FollowPath()
    {
        for (int i=1; i<path.Count;i++)
        {
            
            Vector3 startPosition = transform.position;
            Vector3 endPosition = gridManager.GetPositionFromCoordinates(path[i].coordinates);

            transform.LookAt(endPosition);

            float travelPercen = 0f;
            while(travelPercen <1f)
            {
                travelPercen +=Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition,travelPercen);
                yield return new WaitForEndOfFrame();
            }
        }
        FinishPath();
    }
}
