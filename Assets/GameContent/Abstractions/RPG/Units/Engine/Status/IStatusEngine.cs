using Assets.Abstractions.RPG.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Abstractions.RPG.Units.Engine.Status
{
    public interface IStatusEngine
    {
        CharacterActor Owner { get; }
        void Init(CharacterActor actor);
        void OnUpdate();
        void SetImmune<T>(bool immune) where T : IStatus;

        void SetImmune(Type type, bool immune);

        bool IsImmune(Type type);

        void SetImmune(string tag, bool immune);

        bool IsImmune(string tag);

        bool IsImmune(IList<string> tags);

        int CountStatus(Type type);

        int CountStatus<T>() where T : IStatus;

        int CountStatus(Type type, CharacterActor source);

        int CountStatus<T>(CharacterActor source) where T : IStatus;

        bool HasStatusWithTag(string tag);
        T GetStatus<T>() where T : IStatus;

        T GetStatus<T>(CharacterActor source) where T : IStatus;

        bool HasStatus<T>() where T : IStatus;

        bool HasStatus<T>(CharacterActor source) where T : IStatus;

        bool HasStatus(Type type);

        bool HasStatus(CharacterActor source);
        void ClearStatus(IStatus status, bool forced = false);

        void ClearAllStatus(bool forced = false);

        void ClearStatuses(string tag, bool forced = false);

        void ClearStatuses<T>(bool forced = false) where T : IStatus;
        void ClearStatuses(Type type, bool forced = false);
        void ClearStatuses<T>(CharacterActor source, bool forced = false) where T : IStatus;
        void ClearStatuses(CharacterActor source, bool forced = false);
        void AddStatuses(CharacterActor source, GameObject[] statuses);
        IStatus AddStatus(CharacterActor source, GameObject statusPrefab, bool forced = false);
        IStatus AddStatusWithoutStart(CharacterActor source, GameObject statusPrefab, bool forced = false);
        bool TryAddStatus(CharacterActor source, GameObject statusPrefab, out IStatus status, bool forced = false);
    }
}
