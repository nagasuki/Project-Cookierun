using UnityEngine;
using Cysharp.Threading.Tasks;

namespace PugDev
{
    public class PlayingState : IGameState
    {
        public UniTask EnterAsync()
        {
            Debug.Log("Entering PlayingState");
            return UniTask.CompletedTask;
        }

        public void Update()
        {

        }

        public void Exit()
        {
            Debug.Log("Exiting PlayingState");
        }
    }
}