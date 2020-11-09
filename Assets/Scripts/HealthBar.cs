using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
  public Transform fill;

    void Awake()
    {
        // Gets fill component
        fill = GetComponentsInChildren<Transform>()[1];
    }
    
    // Moves healthbar in GUI when enemy takes damage
    public void TakeDamage(int amountOfHealth, int startingHealth)
    {
        float healthPercentage = Mathf.Clamp01(amountOfHealth / (float)startingHealth); 
        fill.localScale = new Vector3(healthPercentage, fill.localScale.y, fill.localScale.z);
     }

}
