using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// Log enemies entering/leaving towers
public class Tower : MonoBehaviour
{
    public List<Enemy> currentEnemies;
    public Enemy currentTarget;
    public GameObject turret;
    public GameObject turretBullet;
    public Transform current_position;
    public GameObject shot;
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, GetComponent<SphereCollider>().radius);
    }

    void Update()
    {
        if (currentTarget)
        {
            lineRenderer.SetPosition(0, turret.transform.position);
            lineRenderer.SetPosition(1, currentTarget.transform.position);
        }
        Fire();
    }

    void OnTriggerEnter(Collider collider)
    {
        Enemy newEnemy = collider.GetComponent<Enemy>();
        currentEnemies.Add(newEnemy);
        newEnemy.DeathEvent.AddListener(delegate { BookKeeping(newEnemy);});
        EvaluateTarget(newEnemy);
    }
    void OnTriggerExit(Collider collider)
    {
        Enemy enemyLeaving = collider.GetComponent<Enemy>();
        currentEnemies.Remove(enemyLeaving);
        EvaluateTarget(enemyLeaving);
    }

    private void BookKeeping(Enemy enemy)
    {
        currentEnemies.Remove(enemy);
        EvaluateTarget(enemy);
    }

    private void EvaluateTarget(Enemy enemy)
    {
        if (currentTarget == enemy)
        {
            currentTarget = null;
            lineRenderer.enabled = false;
        }

        if(currentTarget == null)
        {
            currentTarget = currentEnemies[0];
            lineRenderer.enabled = true;
        }
    }

    void Fire()
    {
        if(Random.Range(0f, 500f) < 1)
        {
            // Spawn bullets from turret
            GameObject shot = Instantiate(turretBullet, currentTarget.transform.position, currentTarget.transform.rotation);

            // Shot is distroyed in 3 sec
            Destroy(shot, 3f);
        }
    }
}
