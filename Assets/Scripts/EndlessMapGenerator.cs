using PugDev;
using System.Collections.Generic;
using UnityEngine;
using PrimeTween;
using Cysharp.Threading.Tasks;
using System.Linq;

/// <summary>
/// Generates an endless map for a game.
/// </summary>
public class EndlessMapGenerator : SingletonMonoBehaviour<EndlessMapGenerator>
{
    [Header("Map Properties")]
    [SerializeField] private MapProperties mapProperties; // The properties of the map
    [SerializeField] private SpriteRenderer bgSpriteRenderer; // The sprite renderer for the background

    [Header("Camera")]
    [SerializeField] private Camera mainCamera; // The main camera of the game

    [Header("Map Segments")]
    public Transform player; // The player transform
    public float segmentLength = 20f; // The length of each segment
    public int maxSegmentsOnSpawn = 5; // The maximum number of segments to spawn at once
    public float initialMoveSpeed = 10f; // The initial move speed of the map segments
    public float maxMoveSpeed = 30f; // The maximum move speed of the map segments
    public float speedIncreaseRate = 0.1f; // The rate at which the move speed increases

    [Header("Score")]
    [SerializeField] private ScoreViewModel scoreViewModel; // The view model for the score

    private float currentMoveSpeed; // The current move speed of the map segments
    private List<GameObject> activeSegments = new List<GameObject>(); // The active map segments
    private float spawnPosition = 0f; // The position to spawn new segments
    private int currentSegmentIndex = 0; // The index of the current segment
    private float cameraLeftBound; // The left bound of the camera
    private float cameraRightBound; // The right bound of the camera
    private bool isSetup = false; // Whether the map is set up

    private bool isDashBoost = false; // Whether the player is dashing

    /// <summary>
    /// Sets up the stage with the given map properties.
    /// </summary>
    /// <param name="map">The map properties.</param>
    public void SetupStage(MapProperties map)
    {
        mapProperties = map;

        cameraLeftBound = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.transform.position.z)).x * 3f;
        cameraRightBound = 0;
        currentMoveSpeed = initialMoveSpeed;

        SetupBackgroundMap();

        for (int i = 0; i < maxSegmentsOnSpawn; i++)
        {
            SpawnSegmentInitialize();
        }

        isSetup = true;
    }

    public void StartDashBoost()
    {
        isDashBoost = true;
    }

    public void StopDashBoost()
    {
        isDashBoost = false;
    }

    private void Update()
    {
        if (!isSetup) return;
        if (GameManager.Instance.IsGameOver || GameManager.Instance.IsGameComplete) return;

        IncreaseMoveSpeed();

        MoveMapSegments();

        ChangeBackground().Forget();

        if (activeSegments.Count > 0 && activeSegments[0].transform.position.x < cameraLeftBound)
        {
            RemoveOldSegment();
            if (currentSegmentIndex < mapProperties.MaxSegmentsInMap)
            {
                SpawnSegment();
            }
        }

        if (currentSegmentIndex >= mapProperties.MaxSegmentsInMap && activeSegments.Last().transform.position.x < cameraRightBound)
        {
            Debug.Log("Level Completed!");
            GameManager.Instance.GameComplete(scoreViewModel.CurrentScore).Forget();
        }
    }

    private void SetupBackgroundMap()
    {
        bgSpriteRenderer.sprite = mapProperties.MapBackground[0];
    }

    private async UniTask ChangeBackground()
    {
        if (mapProperties.IsChangeMapBackground)
        {
            if (currentSegmentIndex == mapProperties.MaxSegmentsInMap / 2)
            {
                await Tween.Alpha(bgSpriteRenderer, 0f, .5f);

                bgSpriteRenderer.sprite = mapProperties.MapBackground[1];

                await Tween.Alpha(bgSpriteRenderer, 1f, .5f);
            }
        }
    }

    private void MoveMapSegments()
    {
        for (int i = 0; i < activeSegments.Count; i++)
        {
            var speed = isDashBoost ? currentMoveSpeed * 2f : currentMoveSpeed;
            activeSegments[i].transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }

    private void SpawnSegmentInitialize()
    {
        GameObject segment = mapProperties.MapSegmentsPrefab[0];
        GameObject newSegment = Instantiate(segment, new Vector3(spawnPosition, 0, 0), Quaternion.identity);
        activeSegments.Add(newSegment);
        spawnPosition += segmentLength;
        currentSegmentIndex++;
    }

    private void SpawnSegment()
    {
        var randomSegmentIndex = Random.Range(0, mapProperties.MapSegmentsPrefab.Length);
        GameObject segment = currentSegmentIndex >= mapProperties.MaxSegmentsInMap / 2 ? mapProperties.MapSegmentsChangedPrefab[randomSegmentIndex] : mapProperties.MapSegmentsPrefab[randomSegmentIndex];
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
