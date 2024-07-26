using Abstractions.Shared.MEC;
using Assets.Abstractions.Shared.Foundation;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Abstractions.RPG.LocalData
{
    public delegate void OnSceneStartLoad(SceneLoader data);
    public delegate void OnSceneLoaded(SceneLoader data);
    public delegate void OnSceneActive(SceneLoader data);

    public class SceneKey
    {
        public const string Menu = "Menu";
        public const string Battle = "Battle";
    }


    [System.Serializable]
    public class SceneLoader
    {
        private string key;

        public OnSceneStartLoad OnSceneStartLoad;
        public OnSceneLoaded OnSceneLoaded;
        public OnSceneActive OnSceneActive;

        private AsyncOperation task;
        private bool isLoaded = false;
        public string Key { get => key; set => key = value; }

        public SceneLoader(string key)
        {
            this.Key = key;
        }

        public async UniTask LoadAsync(CancellationToken cts = default)
        {
            Log.Info($"{GetType().Name} - Start Load");
            OnSceneStartLoad?.Invoke(this);
            task = SceneManager.LoadSceneAsync(Key);
            task.allowSceneActivation = false;
            await UniTask.WaitUntil(() => task.progress >= 0.9f);
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
