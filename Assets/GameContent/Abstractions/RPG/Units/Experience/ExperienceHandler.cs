using Assets.Abstractions.RPG.Value;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Abstractions.RPG.Units.Experience
{
    [System.Serializable]
    public class ExperienceHandler : ResourceValue
    {
        private readonly IList<ExperienceData> _ExperienceData;
        private int _currentLevel = 1;

        /// <summary>
        /// Notify when level was changed. Params: fromLevel, toLevel
        /// </summary>
        public event Action<int, int> OnLevelChanged;

        [ShowInInspector] public int CurrentLevel => _currentLevel;
        public int NextLevel => Mathf.Clamp(_currentLevel + 1, 1, MaxLevel);
        public int PrevLevel => Mathf.Clamp(_currentLevel - 1, 1, MaxLevel);
        public bool IsMaxLevel => _currentLevel == _ExperienceData.Count;
        public int MaxLevel => _ExperienceData.Count;
        public int CapLevel { private set; get; }

        public float CurrentLevelProgress
        {
            get
            {
                var currentLvlExp = LevelToExp(_currentLevel);
                var nextLvlExp = LevelToExp(NextLevel);
                float exp = TotalAmount - currentLvlExp;
                float total = nextLvlExp - currentLvlExp;
                float percentage = exp / total;
                return percentage;
            }
        }

        public ExperienceHandler(IList<ExperienceData> ExperienceData)
        {
            _ExperienceData = ExperienceData;
            if (ExperienceData != null) MaxValue = _ExperienceData.Last().Exp;
            CapLevel = MaxLevel;
        }

        public void SetLevelCap(int levelCap)
        {
            CapLevel = levelCap;
            MaxValue = LevelToExperienceData(levelCap).Exp;
            _gainCalculator.SetConstraintMax(MaxValue);
        }

        public override void SetAmount(float amount, bool invokeEvent = true)
        {
            base.SetAmount(amount, invokeEvent);
            int newLevel = CalculateLevel(TotalAmount);

            if (_currentLevel == newLevel) return;

            if (invokeEvent)
            {
                InvokeLevelChangeEvent(_currentLevel, newLevel);
            }

            _currentLevel = newLevel;
        }

        public override void Add(float amount, bool invokeEvent = true)
        {
            base.Add(amount, invokeEvent);
            int newLevel = CalculateLevel(TotalAmount);

            if (_currentLevel == newLevel) return;

            // Notify about all changes
            if (invokeEvent)
            {
                InvokeLevelChangeEvent(_currentLevel, newLevel);
            }

            _currentLevel = newLevel;
        }

        public override void Substract(float amount, bool invokeEvent = true)
        {
            base.Substract(amount, invokeEvent);
            int newLevel = CalculateLevel(TotalAmount);

            if (_currentLevel == newLevel) return;

            // Notify about all changes
            if (invokeEvent)
            {
                InvokeLevelChangeEvent(_currentLevel, newLevel);
            }

            _currentLevel = newLevel;
        }

        public int PredictLevelUp(float amount)
        {
            float predictAmount = PredictAdd(amount);
            int newLevel = CalculateLevel(predictAmount);
            return Mathf.Min(newLevel, CapLevel);
        }

        public IList<ExperienceData> GetExperienceData(int fromLevel, int toLevel)
        {
            var data = new List<ExperienceData>();

            for (int i = fromLevel; i <= toLevel; i++)
            {
                data.Add(LevelToExperienceData(i));
            }

            return data;
        }

        private ExperienceData LevelToExperienceData(int level)
        {
            return _ExperienceData[level - 1];
        }

        public long LevelToExp(int level)
        {
            if (level < 1 || level > _ExperienceData.Count) return 0;
            return _ExperienceData[level - 1].Exp;
        }

        public int CalculateLevel(float currentExp)
        {
            if (currentExp >= MaxValue) return CapLevel;
            //Debug.Log(currentExp);
            var currentLevel = 1;
            for (int i = 0; i < _ExperienceData.Count - 1; i++)
            {
                var levelData = _ExperienceData[i];
                var nextLevelData = _ExperienceData[i + 1];

                if (currentExp >= levelData.Exp && currentExp < nextLevelData.Exp)
                {
                    currentLevel = i + 1;
                    break;
                }
            }

            return Mathf.Min(currentLevel, CapLevel);
        }

        private void InvokeLevelChangeEvent(int from, int to)
        {
            int current = from;

            if (to >= from)
            {
                for (int level = from + 1; level <= to; level++)
                {
                    OnLevelChanged?.Invoke(current, level);
                    current = level;
                }
            }
            else
            {
                for (int level = from - 1; level >= to; level--)
                {
                    OnLevelChanged?.Invoke(current, level);
                    current = level;
                }
            }
        }

        public ExperienceHandler Clone()
        {
            var clone = new ExperienceHandler(_ExperienceData);
            clone.Add(TotalAmount, false);
            clone.SetConstrainMax(MaxValue);
            return clone;
        }
    }
}

