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

    void Update()
    {
        if (GameManager.Instance.IsGameOver || GameManager.Instance.IsGameComplete) return;

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SpawnItemBooster();
            timer = spawnInterval;
        }
    }

    void SpawnItemBooster()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(itemBoosterPrefab, spawnPoints[randomIndex].position, Quaternion.identity);
        Debug.Log("Item Booster spawned at: " + spawnPoints[randomIndex].position);
    }
}
