using Cysharp.Threading.Tasks;
using PugDev;
using UnityEngine;

public class IntroController : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("IntroController Started");
        LoadSceneManager.Instance.LoadSceneAsync("MainMenuScene", onLoaded:() =>
        {
            GameStateManager.Instance.SetState(new MainMenuState());
        }).Forget();
    }
}
