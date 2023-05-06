using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Bank : MonoBehaviour
{
     [SerializeField] TextMeshProUGUI displayBalance;
    [SerializeField] int startingBalance = 150;

    [SerializeField] private int currentBalance ;

    public int CurrentBalance { get => currentBalance; }

    void Awake() {
        currentBalance = startingBalance;
        UpdateGold();
    }

    
    public void Deposit(int amount)
    {
        currentBalance += Mathf.Abs(amount);
        UpdateGold();
    }

    public void Withdraw(int amount)
    {
        currentBalance -= Mathf.Abs(amount);
        UpdateGold();
        if(currentBalance < 0)
        {
            //Lose the game;
            ReloadScene();
        }
    }
    void UpdateGold(){
        displayBalance.text = "Gold: " + currentBalance;
    }

    void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);

    }
}
