using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingByXAxis : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    void Start()
    {
        UpdateSortingOrder();
    }

    void Update()
    {
        UpdateSortingOrder();
    }

    void UpdateSortingOrder()
    {
        spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.x * 100);
    }
}
