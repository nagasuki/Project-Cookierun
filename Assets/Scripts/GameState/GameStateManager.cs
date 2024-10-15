using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PugDev
{
    public class GameStateManager : MonoBehaviour
    {
        public static GameStateManager Instance { get; private set; }

        public PlayerGameStat playerGameStat;

        private IGameState currentState;
        private bool isEnterState = false;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private async void Update()
        {
            await UniTask.WaitUntil(() => !isEnterState);
            currentState?.Update();
        }

        public async void SetState(IGameState newState, bool isWaitEnterState = true)
        {
            isEnterState = true;
            currentState?.Exit();
            currentState = newState;
            if (currentState != null)
            {
                if (isWaitEnterState)
                {
                    await currentState.EnterAsync();
                    isEnterState = false;
                }
                else
                {
                    currentState.EnterAsync().Forget();
                    isEnterState = false;
                }
            }
        }
    }

    [Serializable]
    public class PlayerGameStat
    {
        public int SoftCurrency;
        public int HardCurrency;
    }
}