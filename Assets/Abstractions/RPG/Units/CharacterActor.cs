﻿using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Abstractions.RPG.Units
{
    public class CharacterActor : MonoBehaviour, ICharacter
    {
        public bool IsInitialized { private set; get; }
        public bool IsActivated { private set; get; }

        private List<IEngine> engines = new();
        private Dictionary<Type, IEngine> enginesLookup = new();
        private Dictionary<Type, IEngine> nullEngineLookup = new();

        [Button]
        public void Initialize()
        {
            IsInitialized = true;
            IsActivated = false;

            enginesLookup.Clear();
            nullEngineLookup.Clear();
            engines.Clear();

            var enginesComponent = GetComponents<IEngine>();
            foreach (var e in enginesComponent)
            {
                engines.Add(e);
                e.Initialize();
            }
            SetupNullEngine();
        }

        public void ActiveActor()
        {
            IsActivated = true;
        }
        public void Execute()
        {
            foreach (var e in engines)
            {
                e.Execute();
            }
        }

        public TEngine GetEngine<TEngine>() where TEngine : IEngine
        {
            var type = typeof(TEngine);
            if (!enginesLookup.ContainsKey(type))
            {
                foreach (var e in engines)
                {
                    if (e is TEngine)
                    {
                        enginesLookup.Add(type, e);
                        return (TEngine)e;
                    }
                }
                return (TEngine)nullEngineLookup[type];
            }
            return (TEngine)enginesLookup[type];
        }

        private void SetupNullEngine()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var services = assemblies
                .SelectMany(assembly => assembly.GetTypes())
                .Where(t => !t.IsAbstract && t.IsClass && typeof(BaseNullEngine).IsAssignableFrom(t))
                .ToArray();

            foreach (var engine in services)
            {
                var activator = (IEngine)Activator.CreateInstance(engine);
                nullEngineLookup.Add(activator.GetType(), activator);
                Debug.Log($"{GetType()} Add Null Engine: {activator.GetType()}");
            }
        }
    }
}