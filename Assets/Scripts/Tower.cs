using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int buy = 50; 
    [SerializeField] int cost = 80; 
    [SerializeField] float buildDelay = 1f; 
    public Bank bank;

    private void Start() {
        StartCoroutine(Build());
    }
    public void RewardGold()
    {
        if(bank == null) return;
        bank.Deposit(buy);
    }


    public bool CreateTower(Tower towerPrefab , Transform transform, GameObject towerParent)
    {
        bank = FindObjectOfType<Bank>();
        if(bank == null)
        {

            return false;
        }

        if(bank.CurrentBalance >= cost)
        {

            bank.Withdraw(cost);
            Tower Tower = Instantiate(towerPrefab,transform.position,transform.rotation);
            Tower.transform.parent = towerParent.transform;
            return true;
        }
        return false;
        
    }
    IEnumerator Build()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
            foreach(Transform grandchild in child)
            {
                grandchild.gameObject.SetActive(false);
            }
        }

        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(buildDelay) ;
            foreach(Transform grandchild in child)
            {
                grandchild.gameObject.SetActive(true);
            }
        }
    }
}
