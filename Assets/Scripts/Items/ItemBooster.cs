using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents an item booster that can be picked up by the player.
/// </summary>
public class ItemBooster : MonoBehaviour
{
    [SerializeField]
    /// <summary>
    /// The duration of the booster effect in seconds.
    /// </summary>
    private float duration = 5f;

    [SerializeField]
    /// <summary>
    /// The speed at which the booster moves horizontally.
    /// </summary>
    private float moveSpeed = 10f;

    private void Update()
    {
        // If the game is over or complete, do nothing
        if (GameManager.Instance.IsGameOver || GameManager.Instance.IsGameComplete) return;

        // Move the booster horizontally
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the colliding object is the player, apply the booster effect
        if (other.CompareTag("Player"))
        {
            SoundManager.Instance.PlayeSFX("PickupPlane");
            ApplyEffect(other.gameObject).Forget();
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Applies the booster effect to the player.
    /// </summary>
    /// <param name="player">The player object.</param>
    /// <returns>A UniTask representing the async operation.</returns>
    private async UniTask ApplyEffect(GameObject player)
    {
        Debug.Log("Booster applied to " + player.name);

        // Get the score view model component from the player object
        var scoreViewModel = player.GetComponent<ScoreViewModel>();
        if (scoreViewModel != null)
        {
            // Add 5000 points to the player's score
            scoreViewModel.AddScore(5000);
        }

        // Get the player controller component from the player object
        var playerController = player.GetComponent<PlayerController>();
        if (playerController != null)
        {
            // Start the dash boost effect
            playerController.StartDashBoost();
        }

        // Wait for the duration of the booster effect
        await UniTask.WaitForSeconds(duration);

        // Stop the dash boost effect
        playerController.StopDashBoost();
    }
}
