using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessMapGenerator : MonoBehaviour
{
    [Header("Map Properties")]
    public MapProperties MapProperties;

    [Header("Camera")]
    [SerializeField] private Camera mainCamera;

    [Header("Map Segments")]
    public Transform player;
    public float segmentLength = 20f;
    public int maxSegmentsOnSpawn = 10;
    public int maxSegmentsInLevel = 30;
    public float initialMoveSpeed = 10f;
    public float maxMoveSpeed = 30f;
    public float speedIncreaseRate = 0.1f;

    private float currentMoveSpeed;
    private List<GameObject> activeSegments = new List<GameObject>();
    private float spawnPosition = 0f;
    private int currentSegmentIndex = 0;
    private float cameraLeftBound;

    void Start()
    {
        cameraLeftBound = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.transform.position.z)).x * 3f;
        currentMoveSpeed = initialMoveSpeed;

        for (int i = 0; i < maxSegmentsOnSpawn; i++)
        {
            SpawnSegmentInitialize();
        }
    }

    void Update()
    {
        if (GameManager.Instance.IsGameOver) return;

        IncreaseMoveSpeed();

        MoveMapSegments();

        if (activeSegments.Count > 0 && activeSegments[0].transform.position.x < cameraLeftBound)
        {
            RemoveOldSegment();
            if (currentSegmentIndex < maxSegmentsInLevel)
            {
                SpawnSegment();
            }
        }

        if (activeSegments.Count == 0 && currentSegmentIndex >= MapProperties.MapSegmentsPrefab.Length)
        {
            Debug.Log("Level Completed!");
        }
    }

    void MoveMapSegments()
    {
        for (int i = 0; i < activeSegments.Count; i++)
        {
            activeSegments[i].transform.Translate(Vector3.left * currentMoveSpeed * Time.deltaTime);
        }
    }

    void SpawnSegmentInitialize()
    {
        var randomSegmentIndex = Random.Range(0, MapProperties.MapSegmentsPrefab.Length);
        GameObject segment = MapProperties.MapSegmentsPrefab[randomSegmentIndex];
        GameObject newSegment = Instantiate(segment, new Vector3(spawnPosition, 0, 0), Quaternion.identity);
        activeSegments.Add(newSegment);
        spawnPosition += segmentLength;
        currentSegmentIndex++;
    }

    void SpawnSegment()
    {
        var randomSegmentIndex = Random.Range(0, MapProperties.MapSegmentsPrefab.Length);
        GameObject segment = MapProperties.MapSegmentsPrefab[randomSegmentIndex];
        GameObject newSegment = Instantiate(segment, new Vector3(activeSegments[maxSegmentsOnSpawn - 1].transform.position.x + segmentLength, 0, 0), Quaternion.identity);
        activeSegments.Add(newSegment);
        spawnPosition += segmentLength;
        currentSegmentIndex++;
    }

    void RemoveOldSegment()
    {
        if (activeSegments.Count > maxSegmentsOnSpawn)
        {
            Destroy(activeSegments[0]);
            activeSegments.RemoveAt(0);
        }
    }

    void IncreaseMoveSpeed()
    {
        if (currentMoveSpeed < maxMoveSpeed)
        {
            currentMoveSpeed += speedIncreaseRate * Time.deltaTime;
            currentMoveSpeed = Mathf.Min(currentMoveSpeed, maxMoveSpeed);
        }
    }
}
