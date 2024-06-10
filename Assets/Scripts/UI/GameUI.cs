using System;
using Managers;
using TMPro;
using UnityEngine;
using Zenject;

namespace UI
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private DefaultButton _settingsButton;
        
        private ScoreManager _scoreManager;
        private SettingsWindow _settingsWindow;

        [Inject]
        private void Construct(ScoreManager scoreManager, SettingsWindow settingsWindow)
        {
            _scoreManager = scoreManager;
            _settingsWindow = settingsWindow;

            _scoreManager.OnScoreValueChanged += OnScoreChanged;
            _settingsButton.ButtonClicked += OnSettingsClicked;
            
            _scoreText.SetText(_scoreManager.GetScoreValue().ToString());
        }

        private void OnSettingsClicked()
        {
            _settingsWindow.Open();
        }

        private void OnScoreChanged(int value)
        {
            _scoreText.SetText(value.ToString());
        }

        private void OnDestroy()
        {
            if (_scoreManager != null)
            {
                _scoreManager.OnScoreValueChanged -= OnScoreChanged;
            }

            _settingsButton.ButtonClicked -= OnSettingsClicked;
        }
    }
}
