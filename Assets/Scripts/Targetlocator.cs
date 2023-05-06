using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetlocator : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] ParticleSystem projectileParticles ;

    [SerializeField ] GameObject weapon;

    [SerializeField ] Transform target;
    [SerializeField]  float range = 15f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       FindClosetTarget();
        AiWeapon();
    }

    void FindClosetTarget()
    {
        Enemy [] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float maxDistance = Mathf.Infinity;

        foreach(Enemy enemy in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position , enemy.transform.position);

            if(targetDistance < maxDistance)
            {
                closestTarget = enemy.transform;
                maxDistance = targetDistance;
            }
        }
        target = closestTarget;
    }
    void AiWeapon()
    {
        float targetDistance = Vector3.Distance(transform.position, target.position);

        weapon.transform.LookAt(target);
        if(targetDistance < range)
        {
            Attack(true);
        }
        else
        {
            Attack(false);
        }
    }
    void Attack(bool status){
        var emissionModule = projectileParticles.emission;
        emissionModule.enabled = status;
    }

}
