using AYellowpaper.SerializedCollections;
using Cysharp.Threading.Tasks;
using System;
using TransitionsPlus;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PugDev
{
    public class LoadSceneManager : MonoBehaviour
    {
        public static LoadSceneManager Instance { get; private set; }

        [SerializeField]
        private TransitionAnimator transitionAnimator;
        [SerializeField]
        private SerializedDictionary<LoadTransition, TransitionProfile> transitionProfiles = new SerializedDictionary<LoadTransition, TransitionProfile>();

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

        public async UniTask LoadSceneAsync(string sceneName, LoadTransition transition = LoadTransition.ChangeScene, Action onChangeGameState = null)
        {
            transitionAnimator.SetProfile(transitionProfiles[transition]);
            transitionAnimator.profile.invert = false;
            transitionAnimator.Play();

            await UniTask.WaitUntil(() => transitionAnimator.progress == 1);

            await SceneManager.LoadSceneAsync(sceneName);

            if (onChangeGameState != null)
                onChangeGameState?.Invoke();

            transitionAnimator.SetProfile(transitionProfiles[transition]);
            transitionAnimator.profile.invert = true;
            transitionAnimator.Play();

            await UniTask.WaitUntil(() => transitionAnimator.progress == 1);
            transitionAnimator.profile.invert = false;
        }
    }

    [Serializable]
    public enum LoadTransition
    {
        None = 0,
        ChangeScene = 1,
        GameOver = 2
    }
}