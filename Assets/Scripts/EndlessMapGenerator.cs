using PugDev;
using System.Collections.Generic;
using UnityEngine;

public class EndlessMapGenerator : SingletonMonoBehaviour<EndlessMapGenerator>
{
    [Header("Map Properties")]
    [SerializeField] private MapProperties mapProperties;
    [SerializeField] private SpriteRenderer bgSpriteRenderer;

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

    public void SetupStage(MapProperties map)
    {
        mapProperties = map;

        cameraLeftBound = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.transform.position.z)).x * 3f;
        currentMoveSpeed = initialMoveSpeed;

        for (int i = 0; i < maxSegmentsOnSpawn; i++)
        {
            SpawnSegmentInitialize();
        }
    }

    private void Start()
    {

    }

    private void Update()
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

        if (activeSegments.Count == 0 && currentSegmentIndex >= mapProperties.MapSegmentsPrefab.Length)
        {
            Debug.Log("Level Completed!");
        }
    }

    private void SetupBackgroundMap()
    {
        bgSpriteRenderer.sprite = mapProperties.MapBackground[0];
    }

    private void ChangeBackground()
    {
        if (mapProperties.MapBackground.Length > 1)
        {
            
        }
        else
        {
            bgSpriteRenderer.sprite = mapProperties.MapBackground[0];
        }
    }

    private void MoveMapSegments()
    {
        for (int i = 0; i < activeSegments.Count; i++)
        {
            activeSegments[i].transform.Translate(Vector3.left * currentMoveSpeed * Time.deltaTime);
        }
    }

    private void SpawnSegmentInitialize()
    {
        var randomSegmentIndex = Random.Range(0, mapProperties.MapSegmentsPrefab.Length);
        GameObject segment = mapProperties.MapSegmentsPrefab[randomSegmentIndex];
        GameObject newSegment = Instantiate(segment, new Vector3(spawnPosition, 0, 0), Quaternion.identity);
        activeSegments.Add(newSegment);
        spawnPosition += segmentLength;
        currentSegmentIndex++;
    }

    private void SpawnSegment()
    {
        var randomSegmentIndex = Random.Range(0, mapProperties.MapSegmentsPrefab.Length);
        GameObject segment = mapProperties.MapSegmentsPrefab[randomSegmentIndex];
        GameObject newSegment = Instantiate(segment, new Vector3(activeSegments[maxSegmentsOnSpawn - 1].transform.position.x + segmentLength, 0, 0), Quaternion.identity);
        activeSegments.Add(newSegment);
        spawnPosition += segmentLength;
        currentSegmentIndex++;
    }

    private void RemoveOldSegment()
    {
        if (activeSegments.Count > maxSegmentsOnSpawn)
        {
            Destroy(activeSegments[0]);
            activeSegments.RemoveAt(0);
        }
    }

    private void IncreaseMoveSpeed()
    {
        if (currentMoveSpeed < maxMoveSpeed)
        {
            currentMoveSpeed += speedIncreaseRate * Time.deltaTime;
            currentMoveSpeed = Mathf.Min(currentMoveSpeed, maxMoveSpeed);
        }
    }
}
