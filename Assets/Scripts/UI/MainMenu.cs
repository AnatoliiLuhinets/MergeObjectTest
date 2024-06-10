using Common;
using Cysharp.Threading.Tasks;
using Managers;
using TMPro;
using UnityEngine;
using Zenject;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private DefaultButton _playButton;
        [SerializeField] private DefaultToggle _soundsToggle;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private AudioSource _audioSource;

        private SceneLoader _sceneLoader;
        private AudioManager _audioManager;

        [Inject]
        private void Construct(SceneLoader loader, ScoreManager scoreManager)
        {
            _sceneLoader = loader;
            _audioManager = ProjectContext.Instance.Container.Resolve<AudioManager>();
            _audioManager.SetAudioSource(_audioSource);

            _playButton.ButtonClicked += PlayButtonClicked;
            _soundsToggle.ToggleStateChanged += OnToggleStateChanged;

            _scoreText.SetText(scoreManager.GetMaxScoreValue().ToString());
        }

        private void Start()
        {
            _soundsToggle.SetState(!_audioManager.GetAudioState());
        }

        private void OnToggleStateChanged(bool state)
        {
            _audioManager.SetAudioState(!state);
        }

        private void PlayButtonClicked()
        {
            _sceneLoader.LoadScene(Consts.Scenes.GameScene).Forget();
        }

        private void OnDestroy()
        {
            _playButton.ButtonClicked -= PlayButtonClicked;
            _soundsToggle.ToggleStateChanged -= OnToggleStateChanged;
        }
    }
}
