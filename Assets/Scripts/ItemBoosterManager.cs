using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBoosterManager : MonoBehaviour
{
    public GameObject itemBoosterPrefab;
    public Transform[] spawnPoints;
    public float spawnInterval = 10f;

    private float timer;

    void Start()
    {
        timer = spawnInterval;
    }

    /// <summary>
    /// Updates the item booster manager.
    /// </summary>
    void Update()
    {
        // Check if the game is over or complete
        if (GameManager.Instance.IsGameOver || GameManager.Instance.IsGameComplete)
        {
            // If so, return and do nothing
            return;
        }

        // Decrease the timer by the time elapsed since the last frame
        timer -= Time.deltaTime;

        // Check if the timer has reached zero or below
        if (timer <= 0f)
        {
            // If so, spawn an item booster
            SpawnItemBooster();

            // Reset the timer to the spawn interval
            timer = spawnInterval;
        }
    }

    /// <summary>
    /// Spawns an item booster at a random spawn point.
    /// </summary>
    void SpawnItemBooster()
    {
        // Generate a random index within the range of spawn points
        int randomIndex = Random.Range(0, spawnPoints.Length);

        // Instantiate an item booster at the position of the selected spawn point
        Instantiate(itemBoosterPrefab, spawnPoints[randomIndex].position, Quaternion.identity);

        // Log the position where the item booster was spawned
        Debug.Log("Item Booster spawned at: " + spawnPoints[randomIndex].position);
    }
}
