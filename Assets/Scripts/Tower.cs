using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int buy = 50; 
    [SerializeField] int cost = 80; 

    public Bank bank;
    

    public void RewardGold()
    {
        if(bank == null) return;
        bank.Deposit(buy);
    }


    public Tower createTower(Tower towerPrefab , Transform transform )
    {
        bank = FindObjectOfType<Bank>();
        if(bank == null)
        {

            return null;
        }

        if(bank.CurrentBalance >= cost)
        {
            bank.Withdraw(cost);
            return Instantiate(towerPrefab,transform.position,transform.rotation);
        }
        return null;
        
    }

}
