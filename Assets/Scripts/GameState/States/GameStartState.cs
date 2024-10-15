using UnityEngine;
using Cysharp.Threading.Tasks;

namespace PugDev
{
    public class GameStartState : IGameState
    {
        public UniTask EnterAsync()
        {
            Debug.Log("Entering GameStartState");
            return UniTask.CompletedTask;
        }

        public void Update()
        {

        }

        public void Exit()
        {
            Debug.Log("Exiting GameStartState");
        }
    }
}