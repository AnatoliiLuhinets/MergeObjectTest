using System;
using Common;
using Data;
using Environment;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class ScoreManager
    {
        public event Action<int> OnScoreValueChanged;  
        
        private int _score;
        private const int _upgradeCoefficient = 5;
        private FoldingManager _folding;

        [Inject]
        private void Construct(FoldingManager foldingManager = null)
        {
            _folding = foldingManager;

            if (_folding)
            {
                _folding.ObjectsFolded += OnObjectsFolded;
            }
        }

        private void OnObjectsFolded(FoldingItemsType foldingItemsType)
        {
            var upgrade = FoldingItemUpgradeData.GetUpgradeLevel(foldingItemsType);
            
            AddScore(upgrade * _upgradeCoefficient);
        }

        private void AddScore(int value)
        {
            _score += value;
            SaveScore();
            
            OnScoreValueChanged?.Invoke(_score);
        }

        public int GetScoreValue()
        {
            if (PlayerPrefs.HasKey(Consts.UserProgress.ScoreValue))
            {
                _score = PlayerPrefs.GetInt(Consts.UserProgress.ScoreValue);
            }
            return _score;
        }

        public int GetMaxScoreValue()
        {
            return PlayerPrefs.GetInt(Consts.UserProgress.MaxScoreValue);
        }

        private void SaveScore()
        {
            PlayerPrefs.SetInt(Consts.UserProgress.ScoreValue, _score);
            
            if (!PlayerPrefs.HasKey(Consts.UserProgress.MaxScoreValue) || _score > PlayerPrefs.GetInt(Consts.UserProgress.MaxScoreValue))
            {
                PlayerPrefs.SetInt(Consts.UserProgress.MaxScoreValue, _score);
            }
        }

        private void OnDestroy()
        {
            _folding.ObjectsFolded -= OnObjectsFolded;
        }
    }
}