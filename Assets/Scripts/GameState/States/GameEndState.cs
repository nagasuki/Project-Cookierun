using UnityEngine;
using Cysharp.Threading.Tasks;

namespace PugDev
{
    public class GameEndState : IGameState
    {
        public UniTask EnterAsync()
        {
            Debug.Log("Entering GameEndState");
            return UniTask.CompletedTask;
        }

        public void Update()
        {

        }

        public void Exit()
        {
            Debug.Log("Exiting GameEndState");
        }
    }
}