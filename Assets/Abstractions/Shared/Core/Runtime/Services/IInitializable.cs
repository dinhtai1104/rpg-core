using Cysharp.Threading.Tasks;

namespace Assets.Abstractions.Shared.Core
{
    public interface IInitializable : IService
    {
        bool Initialized { get; set; }

        UniTask OnInitialize(IArchitecture architecture);
    }
}