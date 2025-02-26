using Abstractions.Shared.MEC;
using Assets.Abstractions.GameScene;
using Assets.Abstractions.GameScene.Interface;
using Assets.Abstractions.GameScene.Loading;
using Assets.Abstractions.Shared.Core.DI;
using Assets.Abstractions.Shared.Foundation;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Abstractions.RPG.LocalData
{
    public delegate UniTask OnSceneStartLoad(SceneLoader data);
    public delegate UniTask OnSceneLoaded(SceneLoader data);
    public delegate UniTask OnSceneActive(SceneLoader data);

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
        public bool IsLoaded => isLoaded;
        [Inject] ICanvasManager canvas;
        private IView loading;
        float timeLoad = Time.time;
        float offset = 0;
        private LoadingModel loadingModel;
        public SceneLoader(string key)
        {
            this.Key = key;
        }

        public async UniTask LoadAsync(CancellationToken cts = default)
        {
            timeLoad = Time.time;
            loadingModel = new LoadingModel() { Addressable = "UI/UILoadingPanel", MinTimeLoading = 1f };
            loading = await canvas.ShowView(loadingModel);
            
            Log.Info($"{GetType().Name} - Start Load");
            if (OnSceneStartLoad != null)
                await OnSceneStartLoad(this);

            offset += Time.time - timeLoad;
            timeLoad = Time.time;

            task = SceneManager.LoadSceneAsync(Key);
            task.allowSceneActivation = false;

            await UniTask.WaitUntil(() => task.progress >= 0.9f);
            Log.Info($"{GetType().Name} - Done Load");

            if (OnSceneLoaded != null)
                await OnSceneLoaded(this);

            offset += Time.time - timeLoad;
            timeLoad = Time.time;

            isLoaded = true;
        }
        public async UniTask ActiveScene()
        {
            Log.Info($"{GetType().Name} - Active Scene - {Key}");
            if (OnSceneActive != null)
                await OnSceneActive(this);
            task.allowSceneActivation = true;
            offset += Time.time - timeLoad;
            timeLoad = Time.time;

            if ((loadingModel.MinTimeLoading - offset) > 0)
                await UniTask.Delay((int)((loadingModel.MinTimeLoading - offset) * 1000));
            await canvas.CloseView(loading);
        }
    }
}
