using UnityEngine;
using Cysharp.Threading.Tasks;

namespace PugDev
{
    public class GameStartState : IGameState
    {
        private EndlessMapGenerator mapGenerator;

        public async UniTask EnterAsync()
        {
            Debug.Log("Entering GameStartState");
            mapGenerator = EndlessMapGenerator.Instance;

            var stageId = GameManager.Instance.GetLastStageId();
            var map = GameManager.Instance.GetMapProperties(stageId);
            mapGenerator.SetupStage(map);
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