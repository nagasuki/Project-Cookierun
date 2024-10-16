using UnityEngine;

/// <summary>
/// This class is responsible for updating the sorting order of a sprite renderer based on the x-coordinate of the transform.
/// </summary>
public class SortingByXAxis : MonoBehaviour
{
    /// <summary>
    /// The sprite renderer to update the sorting order of.
    /// </summary>
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    /// <summary>
    /// Updates the sorting order of the sprite renderer when the component is first created.
    /// </summary>
    private void Start()
    {
        UpdateSortingOrder();
    }

    /// <summary>
    /// Updates the sorting order of the sprite renderer every frame.
    /// </summary>
    private void Update()
    {
        UpdateSortingOrder();
    }

    /// <summary>
    /// Updates the sorting order of the sprite renderer based on the x-coordinate of the transform.
    /// </summary>
    private void UpdateSortingOrder()
    {
        spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.x * 100);
    }
}
