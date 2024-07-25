using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Abstractions.RPG.LocalData
{
    public delegate void OnSceneStartLoad(SceneData data);
    public delegate void OnSceneLoaded(SceneData data);
    public delegate void OnSceneActive(SceneData data);

    public class SceneKey
    {
        public const string Menu = "Menu";
        public const string Battle = "Battle";
    }


    [System.Serializable]
    public class SceneData
    {
        public string Key;
        public 
    }
}
