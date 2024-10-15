using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapProperties (1)", menuName = "Maps/MapProperties")]
public class MapProperties : ScriptableObject
{
    [Header("Settings")]
    public int MaxSegmentsInMap;

    [Header("Reference")]
    public Sprite[] MapBackground;
    public GameObject[] MapSegmentsPrefab;
}
