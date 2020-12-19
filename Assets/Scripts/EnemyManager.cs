using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enemy group struct/ Container to spawn enemies, amount of enemies and time delay between enemies
[System.Serializable]
public struct Group
{
    public GameObject enemyType;
    [Range(0, 50)] public int amountOfEnemies;
    [Range(0, 5)] public float timeDelay;

    public Group(GameObject enemyType, int amountOfEnemies, float timeDelay)
    {
        this.enemyType = enemyType;
        this.amountOfEnemies = amountOfEnemies;
        this.timeDelay = timeDelay;
    }
}

// Data structure for multiple different groups
[System.Serializable]
public struct Wave
{
    // Group array for multiple enemy groups
   public Group[] enemyGroups;
}

public class EnemyManager : MonoBehaviour
{
    //Pass navigation path
    public Waypoints[] navPoints;
    public Wave enemyWave;

    // Start is called before the first frame update
    void Start()
    {
        SpawnWave();
    }

    private void SpawnWave()
    {
        // Instantiate one representative for each group
        foreach (Group group in enemyWave.enemyGroups)
        {
            StartCoroutine(SpawnGroup(group));
        }
    }

    IEnumerator SpawnGroup(Group enemyGroup)
    {
        int i = 0;
        while (enemyGroup.amountOfEnemies > 0)
        {
            // Spawn enemy
            GameObject spawnedEnemy = Instantiate(enemyGroup.enemyType);
            // Give navPoint
            spawnedEnemy.GetComponent<Enemy>().StartEnemy(navPoints);
            // Gives enemies unique names w/ number
            spawnedEnemy.name = $"{enemyGroup.enemyType.ToString()} {i}";
            enemyGroup.amountOfEnemies--;
            i++;
            // Wait and go back to 0
            yield return new WaitForSeconds(enemyGroup.timeDelay);          
        }
    }
}
