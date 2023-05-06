using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] List<WayPoint> path = new List<WayPoint>();
    [SerializeField] [Range(0f,5f)] float speed = 1f; 
    Enemy enemy ;
    void OnEnable() {
        FindPaths();
        ReturnToStart();
        transform.LookAt(path[1].transform.position);
        StartCoroutine(FollowPath());
    }
    void Start() {
        enemy = GetComponent<Enemy>();
    }

    void FindPaths(){
        path.Clear();
        GameObject tiles = GameObject.FindGameObjectWithTag("path");


        foreach(Transform tile in  tiles.transform)
        {
            WayPoint wayPoint = tile.GetComponent<WayPoint>();

            if(wayPoint!=null)
            {
                path.Add(wayPoint);
            }
        }
    }
    public void ReturnToStart()
    {
        transform.position = path[0].transform.position;
    }
    void FinishPath()
    {
        enemy.StealGold();
        gameObject.SetActive(false);
    }
    IEnumerator FollowPath()
    {
        foreach (WayPoint wayPoint in path)
        {
            
            Vector3 startPosition = transform.position;
            Vector3 endPosition = wayPoint.transform.position;

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
