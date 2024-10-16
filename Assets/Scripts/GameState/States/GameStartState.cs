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

            GameManager.Instance.GameStart();

            mapGenerator = EndlessMapGenerator.Instance;

            var stageId = GameManager.Instance.SelectionStageId;
            Debug.Log($"Stage ID : {stageId}");
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