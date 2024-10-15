using Cysharp.Threading.Tasks;
using UnityEngine;

namespace PugDev
{
    public class MainMenuState : IGameState
    {
        private UIMainMenuScene UI;

        public async UniTask EnterAsync()
        {
            UI = UIMainMenuScene.Instance;

            UI.PlayButton.onClick.AddListener(() =>
            {
                LoadSceneManager.Instance.LoadSceneAsync("LevelSelectionScene", onLoaded: () =>
                {
                    GameStateManager.Instance.SetState(new LevelSelectionState());
                }).Forget();
            });
            UI.QuitButton.onClick.AddListener(Application.Quit);

            Debug.Log("Entering MainMenuState");
        }

        public void Update()
        {
            //Debug.Log("Updating MainMenuState");
        }

        public void Exit()
        {
            Debug.Log("Exiting MainMenuState");
        }
    }
}