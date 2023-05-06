using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int maxHitPoints = 5;

    [Tooltip("Adds amount to maxHitPoints when enemy dies.")]
    [SerializeField] int difficultyRamp = 1;

    [SerializeField] int currentHitPoints = 0;
    Enemy enemy ;
    void OnEnable()
    {
        currentHitPoints = maxHitPoints;
        enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    
    void OnParticleCollision(GameObject other) {
        ProcessHit();
    }

    void ProcessHit()
    {
        
        currentHitPoints-- ;
        if(currentHitPoints <= 0 )
        {
            enemy.RewardGold();
            maxHitPoints+=difficultyRamp;
            gameObject.SetActive(false);
            gameObject.GetComponent<EnemyMover>().ReturnToStart();
            currentHitPoints = maxHitPoints;
        }
    }
}
