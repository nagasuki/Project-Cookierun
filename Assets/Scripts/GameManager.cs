using Cysharp.Threading.Tasks;
using PugDev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Properties")]
    [SerializeField] private int stageId = 0;

    public bool IsGameOver { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        GameStateManager.Instance.SetState(new MainMenuState());
    }

    public void GameStart()
    {
        IsGameOver = false;
    }

    public async UniTask GameOver()
    {
        IsGameOver = true;

        await UniTask.Delay(2500);


    }
}
