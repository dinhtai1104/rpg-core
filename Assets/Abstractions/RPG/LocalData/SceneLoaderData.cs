using Assets.Abstractions.Shared.Foundation;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Abstractions.RPG.LocalData
{
    public delegate void OnSceneStartLoad(SceneLoaderData data);
    public delegate void OnSceneLoaded(SceneLoaderData data);
    public delegate void OnSceneActive(SceneLoaderData data);

    public class SceneKey
    {
        public const string Menu = "Menu";
        public const string Battle = "Battle";
    }


    [System.Serializable]
    public class SceneLoaderData
    {
        private string key;

        public OnSceneStartLoad OnSceneStartLoad;
        public OnSceneLoaded OnSceneLoaded;
        public OnSceneActive OnSceneActive;

        private AsyncOperation task;

        public string Key { get => key; set => key = value; }

        public SceneLoaderData(string key)
        {
            this.Key = key;
        }

        public async UniTask LoadAsync(CancellationToken cts = default)
        {
            Log.Info($"{GetType().Name} - Start Load");
            OnSceneStartLoad?.Invoke(this);
            task = SceneManager.LoadSceneAsync(Key);
            task.allowSceneActivation = false;
            bool isDone = false;
            task.completed += (a) =>
            {
                isDone = true;
            };
            await UniTask.WaitUntil(() => isDone, cancellationToken: cts);
            Log.Info($"{GetType().Name} - Done Load");
            OnSceneLoaded?.Invoke(this);
        }

        public void ActiveScene()
        {
            Log.Info($"{GetType().Name} - Active Scene - {Key}");
            OnSceneActive?.Invoke(this);
            task.allowSceneActivation = true;
        }
    }
}
