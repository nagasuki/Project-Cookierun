using UnityEngine;

public class ItemScore : MonoBehaviour
{
    public int Score = 500;

    public Transform player;
    public float magnetSpeed = 5f;
    public float magnetRadius = 3f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player.GetComponent<PlayerController>().IsDash)
        {
            float distance = Vector3.Distance(transform.position, player.position);

            if (distance < magnetRadius)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.position, magnetSpeed * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SoundManager.Instance.PlayeSFX("KeepStar");
            other.GetComponent<ScoreViewModel>().AddScore(Score);
            Destroy(gameObject);
        }
    }
}
