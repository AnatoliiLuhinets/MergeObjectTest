using Common;
using Cysharp.Threading.Tasks;
using Managers;
using UnityEngine;
using Zenject;

namespace UI
{
    public class SettingsWindow : DefaultWindow
    {
        [SerializeField] private DefaultButton _exitButton;
        [SerializeField] private DefaultSlider _volumeSlider;
        [SerializeField] private DefaultToggle _soundToggle;
        
        private SceneLoader _sceneLoader;
        private AudioManager _audioManager;
        private LevelSaver _levelSaver;
        
        [Inject]
        private void Construct(SceneLoader sceneLoader, LevelSaver saver)
        {
            _sceneLoader = sceneLoader;
            _levelSaver = saver;
            
            _audioManager = ProjectContext.Instance.Container.Resolve<AudioManager>();

            _volumeSlider.SliderValueChanged += OnSliderValueChanged;
            _exitButton.ButtonClicked += OnExitButtonClicked;
            _soundToggle.ToggleStateChanged += OnToggleStateChanged;
            
            _soundToggle.SetState(!_audioManager.GetAudioState());
        }

        public override void Open()
        {
            base.Open();
            
            if(PlayerPrefsExtensions.HasKey(Consts.Audio.Volume))
            {
                _volumeSlider.SetValue(PlayerPrefsExtensions.GetFloat(Consts.Audio.Volume));
            }

            if (PlayerPrefsExtensions.HasKey(Consts.Audio.AudioState))
            {
                _soundToggle.SetState(!PlayerPrefsExtensions.GetBool(Consts.Audio.AudioState));
            }
        }

        private void OnSliderValueChanged(float value)
        {
            _audioManager.SetVolume(value);
            
            _soundToggle.SetState(value != 0);
        }        
        
        private void OnToggleStateChanged(bool state)
        {
            _audioManager.SetAudioState(!state);
        }

        private void OnExitButtonClicked()
        {
            _levelSaver.Save();
            _sceneLoader.LoadScene(Consts.Scenes.MainMenuScene).Forget();
        }

        protected override void OnDestroy()
        {
            _exitButton.ButtonClicked -= OnExitButtonClicked;
            _volumeSlider.SliderValueChanged -= OnSliderValueChanged;
            _soundToggle.ToggleStateChanged -= OnToggleStateChanged;
            
            base.OnDestroy();
        }
    }
}
