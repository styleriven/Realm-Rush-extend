using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    [SerializeField] GameObject gameobject ;
    [SerializeField] [Range(0,50)]int sizeEnemy =5;

    [SerializeField] [Range(0.1f,30f)] float spawnTimer = 1f;

    GameObject [] pool; 
    void Awake() {
        PopulatePool();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }
    void PopulatePool()
    {
        pool = new GameObject[sizeEnemy];

        for(int i = 0; i < sizeEnemy; i++)
        {
            pool[i] = Instantiate(gameobject,transform);
            pool[i].SetActive(false);
        }
    }

    void EnableObjectInPool()
    {
        for(int i = 0; i < sizeEnemy; i++)
        {
            if(pool[i].activeSelf == false)
            {
                pool[i].SetActive(true);
                return;
            }
        }

    }
    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            EnableObjectInPool();
            
            yield return new  WaitForSeconds(spawnTimer) ;

        }
    }
}
