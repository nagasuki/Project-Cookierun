using AYellowpaper.SerializedCollections;
using Cysharp.Threading.Tasks;
using System;
using TransitionsPlus;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PugDev
{
    /// <summary>
    /// Class responsible for managing scene loading and transitions.
    /// </summary>
    public class LoadSceneManager : MonoBehaviour
    {
        /// <summary>
        /// Singleton instance of the LoadSceneManager.
        /// </summary>
        public static LoadSceneManager Instance { get; private set; }

        [SerializeField]
        /// <summary>
        /// Transition animator used for scene transitions.
        /// </summary>
        private TransitionAnimator transitionAnimator;
        [SerializeField]
        /// <summary>
        /// Dictionary of transition profiles for different types of transitions.
        /// </summary>
        private SerializedDictionary<LoadTransition, TransitionProfile> transitionProfiles = new SerializedDictionary<LoadTransition, TransitionProfile>();

        private void Awake()
        {
            /// <summary>
            /// Ensure that only one instance of the LoadSceneManager exists in the scene.
            /// </summary>
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

        /// <summary>
        /// Load a scene asynchronously with a transition.
        /// </summary>
        /// <param name="sceneName">Name of the scene to load.</param>
        /// <param name="transition">Type of transition to use.</param>
        /// <param name="onChangeGameState">Callback function to invoke when the scene is loaded.</param>
        /// <returns>A UniTask representing the asynchronous operation.</returns>
        public async UniTask LoadSceneAsync(string sceneName, LoadTransition transition = LoadTransition.ChangeScene, Action onChangeGameState = null)
        {
            // Set the transition profile for the specified transition type.
            transitionAnimator.SetProfile(transitionProfiles[transition]);
            transitionAnimator.profile.invert = false;
            transitionAnimator.Play();

            // Wait until the transition is complete.
            await UniTask.WaitUntil(() => transitionAnimator.progress == 1);

            // Load the specified scene.
            await SceneManager.LoadSceneAsync(sceneName);

            // Invoke the callback function if provided.
            if (onChangeGameState != null)
                onChangeGameState?.Invoke();

            // Play the transition in reverse.
            transitionAnimator.SetProfile(transitionProfiles[transition]);
            transitionAnimator.profile.invert = true;
            transitionAnimator.Play();

            // Wait until the reverse transition is complete.
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