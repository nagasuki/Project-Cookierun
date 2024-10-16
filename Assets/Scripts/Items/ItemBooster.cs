using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBooster : MonoBehaviour
{
    [SerializeField] private float duration = 5f;
    [SerializeField] private float moveSpeed = 10f;

    private void Update()
    {
        if (GameManager.Instance.IsGameOver || GameManager.Instance.IsGameComplete) return;

        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SoundManager.Instance.PlayeSFX("PickupPlane");
            ApplyEffect(other.gameObject).Forget();
            Destroy(gameObject);
        }
    }

    private async UniTask ApplyEffect(GameObject player)
    {
        Debug.Log("Booster applied to " + player.name);

        var scoreViewModel = player.GetComponent<ScoreViewModel>();
        if (scoreViewModel != null)
        {
            scoreViewModel.AddScore(5000);
        }

        var playerController = player.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.StartDashBoost();
        }

        await UniTask.WaitForSeconds(duration);

        playerController.StopDashBoost();
    }
}
