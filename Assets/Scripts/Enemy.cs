using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Transactions;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    private Waypoints[] navPoints;
    private Transform target;
    private Vector3 direction;
    public float amplify = 1;
    private int index = 0;
    private bool move = true;
    private Purse purse;
    private HealthBar healthBar;
    private int startingHealth;
    public int currentHealth = 100;
    public int cashPoints = 100;
    public AudioClip deathSound;
    public UnityEvent DeathEvent;

      // Start is called before the first frame update
   public void StartEnemy(Waypoints[] navigationalPath)
    {
        navPoints = navigationalPath;
        purse = GameObject.FindGameObjectWithTag("Purse").GetComponent<Purse>();
        healthBar = GetComponentInChildren<HealthBar>();
        startingHealth = currentHealth;
        transform.position = navPoints[index].transform.position;
        NextWaypoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            transform.Translate(direction.normalized * Time.deltaTime * amplify);

            if ((transform.position - target.position).magnitude < .1f)
            {
                NextWaypoint();
            }
        }

    }

    private void NextWaypoint()
    {
        if (index < navPoints.Length - 1)
        {
            index += 1;
            target = navPoints[index].transform;
            direction = target.position - transform.position;
        }
        else
        {
            move = false;
        }
    }

    public void Damage(int amountDamage)
    {
        // Decreases health when taking damage
        currentHealth -= amountDamage;
        if (currentHealth < 0)
        { 

            // If enemy is destroyed add cash to purse
            purse.AddCash(cashPoints);

            // Notify towers that enemy is killed
            DeathEvent.Invoke();

            // Destroy enemy
            Destroy(this.gameObject);
        }
        else
        {
            // Reflects health from damage to GUI HealthBar
            healthBar.TakeDamage(currentHealth, startingHealth);
        }

    }

}
