using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapProperties (1)", menuName = "Maps/MapProperties")]
public class MapProperties : ScriptableObject
{
    [Header("Settings")]
    public int MaxSegmentsInMap;

    [Header("Reference")]
    public bool IsChangeMapBackground;
    public Sprite[] MapBackground;
    public GameObject[] MapSegmentsPrefab;

    [Header("IsChangePlatForm")]
    public bool IsChangePlatform;
    public GameObject[] MapSegmentsChangedPrefab;
}
