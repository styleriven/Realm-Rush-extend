using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int goldReward = 25; 
    [SerializeField] int goldPenalty = 25; 
    // Start is called before the first frame update
    public Bank bank;
    void Start()
    {
        bank = FindObjectOfType<Bank>();
    }

    public void RewardGold(int k=0)
    {
        if(bank == null) return;
        bank.Deposit(goldReward+k);
    }

    public void StealGold( )
    {
        if(bank == null) return;
        bank.Withdraw(goldReward);
    }
}
