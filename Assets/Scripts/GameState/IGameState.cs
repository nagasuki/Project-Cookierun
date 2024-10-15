using Cysharp.Threading.Tasks;

namespace PugDev
{
    public interface IGameState
    {
        UniTask EnterAsync();
        void Update();
        void Exit();
    }
}