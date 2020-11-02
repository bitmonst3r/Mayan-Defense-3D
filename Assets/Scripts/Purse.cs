using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Purse : MonoBehaviour
{
  public int currentCash = 1000;
  public TextMeshProUGUI purseText;

    // Start is called before the first frame update
    void Start()
    {
        SetCash();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetCash()
    {
      purseText.text = $"${currentCash}";
    }

    public void AddCash(int amountOfCash)
    {
        currentCash += amountOfCash;
        SetCash();
    }

    // Doesn't allow defense to be placed if purse < $0
    public bool PlaceTower(int amountOfCashRequired)
    {
        // Updates Purse amount
        if (currentCash - amountOfCashRequired >= 0)  
        {
            // Updates GUI
            currentCash -= amountOfCashRequired; 
            SetCash();
            // Tower can be added   
            return true;  
        }

        // Not enough money to place defense
        return false;  
    }
}
