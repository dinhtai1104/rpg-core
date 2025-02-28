using Abstractions.Shared;
using Assets.Abstractions.RPG.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Abstractions.RPG.Units.Engine.Status
{
    public class StatusEngine : MonoBehaviour, IStatusEngine
    {
        private List<IStatus> m_Statuses = new List<IStatus>();
        private List<string> m_ImmuneTags = new List<string>();
        private Action<CharacterActor, CharacterActor, IStatus> m_OnStatusAdded;

        public Action<CharacterActor, CharacterActor, IStatus> OnStatusAdded
        {
            get => m_OnStatusAdded;
            set => m_OnStatusAdded = value;
        }

        public CharacterActor Owner { get; private set; }
        public bool Lock { get; set; }

        public void Init(CharacterActor actor)
        {
            Owner = actor;
        }

        public void OnUpdate()
        {
            if (Lock) return;

            for (var i = m_Statuses.Count - 1; i >= 0; i--)
            {
                m_Statuses[i].OnUpdate(Time.deltaTime);
            }

            for (var i = m_Statuses.Count - 1; i >= 0; i--)
            {
                if (i < 0 || i >= m_Statuses.Count) break;

                var status = m_Statuses[i];
                if (status == null)
                {
                    m_Statuses.RemoveAt(i);
                    continue;
                }

                if (!status.IsExpired)
                {
                    continue;
                }

                status.Stop();
                m_Statuses.RemoveAt(i);
                RemoveStatusInstance(status);
            }
        }

        public void SetImmune<T>(bool immune) where T : IStatus
        {
            var type = typeof(T);

            if (immune)
            {
                m_ImmuneTags.Add(type.Name);
            }
            else
            {
                m_ImmuneTags.Remove(type.Name);
            }
        }

        public void SetImmune(Type type, bool immune)
        {
            if (immune)
            {
                m_ImmuneTags.Add(type.Name);
            }
            else
            {
                m_ImmuneTags.Remove(type.Name);
            }
        }

        public bool IsImmune(Type type)
        {
            return m_ImmuneTags.Contains(type.Name);
        }

        public void SetImmune(string tag, bool immune)
        {
            if (immune)
            {
                m_ImmuneTags.Add(tag);
            }
            else
            {
                m_ImmuneTags.Remove(tag);
            }
        }

        public bool IsImmune(string tag)
        {
            return m_ImmuneTags.Contains(tag);
        }

        public bool IsImmune(IList<string> tags)
        {
            if (tags.Count == 0) return false;

            foreach (var tag in tags)
            {
                if (!m_ImmuneTags.Contains(tag)) return false;
            }

            return true;
        }

        public int CountStatus(Type type)
        {
            int count = 0;
            foreach (var status in m_Statuses)
            {
                if (status.GetType() == type)
                {
                    count++;
                }
            }

            return count;
        }

        public int CountStatus<T>() where T : IStatus
        {
            int count = 0;
            foreach (var status in m_Statuses)
            {
                if (status.GetType() == typeof(T))
                {
                    count++;
                }
            }

            return count;
        }

        public int CountStatus(Type type, CharacterActor source)
        {
            int count = 0;
            foreach (var status in m_Statuses)
            {
                if (status.Source == source && status.GetType() == type)
                {
                    count++;
                }
            }

            return count;
        }

        public int CountStatus<T>(CharacterActor source) where T : IStatus
        {
            var type = typeof(T);
            var count = 0;
            foreach (var status in m_Statuses)
            {
                if (status.Source == source && status.GetType() == type)
                {
                    count++;
                }
            }

            return count;
        }

        public bool HasStatusWithTag(string tag)
        {
            foreach (var status in m_Statuses)
            {
                if (status != null && status.Tagger.HasTag(tag)) return true;
            }

            return false;
        }

        public T GetStatus<T>() where T : IStatus
        {
            foreach (var status in m_Statuses)
            {
                if (status.GetType() == typeof(T))
                {
                    return (T)status;
                }
            }

            return default;
        }

        public T GetStatus<T>(CharacterActor source) where T : IStatus
        {
            var type = typeof(T);
            foreach (var status in m_Statuses)
            {
                if (status.GetType() == type && status.Source == source)
                {
                    return (T)status;
                }
            }

            return default;
        }

        public bool HasStatus<T>() where T : IStatus
        {
            foreach (var status in m_Statuses)
            {
                if (status.GetType() == typeof(T))
                {
                    return true;
                }
            }

            return false;
        }

        public bool HasStatus<T>(CharacterActor source) where T : IStatus
        {
            foreach (var status in m_Statuses)
            {
                if (status.GetType() == typeof(T) && status.Source == source)
                {
                    return true;
                }
            }

            return false;
        }

        public bool HasStatus(Type type)
        {
            foreach (var status in m_Statuses)
            {
                if (status.GetType() == type)
                {
                    return true;
                }
            }

            return false;
        }

        public bool HasStatus(CharacterActor source)
        {
            foreach (var status in m_Statuses)
            {
                if (status.Source == source)
                {
                    return true;
                }
            }

            return false;
        }

        public void ClearStatus(IStatus status, bool forced = false)
        {
            if (forced)
            {
                status.Stop();
            }
            else
            {
                if (!status.Permanent)
                {
                    status.Stop();
                }
            }
        }

        public void ClearAllStatus(bool forced = false)
        {
            for (int i = m_Statuses.Count - 1; i >= 0; --i)
            {
                if (i >= m_Statuses.Count) break;

                var status = m_Statuses[i];

                if (status == null) continue;
                if (forced)
                {
                    status.Stop();
                }
                else
                {
                    if (!status.Permanent)
                    {
                        status.Stop();
                    }
                }
            }
        }

        public void ClearStatuses(string tag, bool forced = false)
        {
            for (int i = m_Statuses.Count - 1; i >= 0; --i)
            {
                if (i >= m_Statuses.Count) break;

                var status = m_Statuses[i];

                if (!status.Tagger.HasTag(tag)) continue;
                if (forced)
                {
                    status.Stop();
                }
                else
                {
                    if (!status.Permanent)
                    {
                        status.Stop();
                    }
                }
            }
        }

        public void ClearStatuses<T>(bool forced = false) where T : IStatus
        {
            for (int i = m_Statuses.Count - 1; i >= 0; --i)
            {
                if (i >= m_Statuses.Count)
                {
                    break;
                }

                IStatus status = m_Statuses[i];

                if (status.GetType() != typeof(T))
                {
                    continue;
                }

                if (forced)
                {
                    status.Stop();
                    m_Statuses.RemoveAt(i);
                    RemoveStatusInstance(status);
                }
                else
                {
                    if (!status.Permanent)
                    {
                        status.Stop();
                        m_Statuses.RemoveAt(i);
                        RemoveStatusInstance(status);
                    }
                }
            }
        }

        public void ClearStatuses(Type type, bool forced = false)
        {
            for (int i = m_Statuses.Count - 1; i >= 0; --i)
            {
                if (i >= m_Statuses.Count)
                {
                    break;
                }

                IStatus status = m_Statuses[i];

                if (status.GetType() != type)
                {
                    continue;
                }

                if (forced)
                {
                    status.Stop();
                    m_Statuses.RemoveAt(i);
                    RemoveStatusInstance(status);
                }
                else
                {
                    if (!status.Permanent)
                    {
                        status.Stop();
                        m_Statuses.RemoveAt(i);
                        RemoveStatusInstance(status);
                    }
                }
            }
        }

        public void ClearStatuses<T>(CharacterActor source, bool forced = false) where T : IStatus
        {
            for (int i = m_Statuses.Count - 1; i >= 0; --i)
            {
                if (i >= m_Statuses.Count) break;

                var status = m_Statuses[i];

                if (status.GetType() != typeof(T) || source != status.Source) continue;
                if (forced)
                {
                    status.Stop();
                }
                else
                {
                    if (!status.Permanent)
                    {
                        status.Stop();
                    }
                }
            }
        }

        public void ClearStatuses(CharacterActor source, bool forced = false)
        {
            for (int i = m_Statuses.Count - 1; i >= 0; --i)
            {
                if (i >= m_Statuses.Count) break;

                var status = m_Statuses[i];

                if (status.Source != source) continue;
                if (forced)
                {
                    status.Stop();
                }
                else
                {
                    if (!status.Permanent) status.Stop();
                }
            }
        }

        public void AddStatuses(CharacterActor source, GameObject[] statuses)
        {
            foreach (var status in statuses)
            {
                if (status != null) AddStatus(source, status);
            }
        }

        public IStatus AddStatus(CharacterActor source, GameObject statusPrefab, bool forced = false)
        {
            if (!TryAddStatus(source, statusPrefab, out IStatus status, forced)) return null;
            status?.Begin();
            return status;
        }

        public IStatus AddStatusWithoutStart(CharacterActor source, GameObject statusPrefab, bool forced = false)
        {
            return !TryAddStatus(source, statusPrefab, out IStatus status, forced) ? null : status;
        }

        public bool TryAddStatus(CharacterActor source, GameObject statusPrefab, out IStatus status, bool forced = false)
        {
            status = null;

            if (statusPrefab == null)
            {
                Debug.LogError("Status Prefab is null " + source);
                return false;
            }

            var statusTemplate = statusPrefab.GetComponent<IStatus>();

            Type statusType = statusTemplate.GetType();

            if (!forced && IsImmune(statusType))
            {
                return false;
            }

            if (statusTemplate.Override)
            {
                ClearStatuses(statusType);
                status = CreateStatusInstance(statusPrefab, source);
                return true;
            }
            else if (statusTemplate.Stackable)
            {
                int currentStack = CountStatus(statusType, source);
                if (currentStack < statusTemplate.MaxStack)
                {
                    status = CreateStatusInstance(statusPrefab, source);
                    return true;
                }
            }

            return false;
        }

        private IStatus CreateStatusInstance(GameObject statusPrefab, CharacterActor source)
        {
            if (Owner == null || Owner.IsDead || Owner.Transform == null) return null;
            var addedStatus = PoolFactory.Spawn(statusPrefab);
            addedStatus.transform.SetParent(Owner.Transform);
            addedStatus.transform.localPosition = Vector3.zero;
            addedStatus.transform.localRotation = Quaternion.identity;
            addedStatus.gameObject.SetActive(true);
            var iStatus = addedStatus.GetComponent<IStatus>();
            iStatus.SetSource(source);
            iStatus.SetActor(Owner);
            m_Statuses.Add(iStatus);
            m_OnStatusAdded?.Invoke(Owner, source, iStatus);
            return iStatus;
        }

        private void RemoveStatusInstance(IStatus status)
        {
            if (status is BaseStatus statusObject)
            {
                if (statusObject != null)
                {
                    PoolFactory.Despawn(statusObject.gameObject);
                }
            }
        }
    }
}
