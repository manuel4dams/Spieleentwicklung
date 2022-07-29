using System;
using Cinemachine.Editor;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Project
{
    public class SettingsHandler : MonoBehaviour
    {
        [Header("Audio")] //
        public Slider masterVolumeSlider;
        public TMP_InputField masterVolumeInput;
        public Slider soundVolumeSlider;
        public TMP_InputField soundVolumeInput;
        public AudioClip soundSample;
        public float soundSampleDelay;
        public Slider musicVolumeSlider;
        public TMP_InputField musicVolumeInput;

        [Header("Graphics")] //
        public Slider gammaSlider;
        public TMP_InputField gammaInput;

        private float soundSampleNextTimeToPlay = float.PositiveInfinity;

        void Start()
        {
            if (!Application.isEditor && Prefs.SettingsDone.GetBool())
            {
                GoToMenu();
                return;
            }


            void NotifySoundVolumeChange()
            {
                AudioConfiguration.NotifyVolume();
                soundSampleNextTimeToPlay = Time.realtimeSinceStartup + soundSampleDelay;
            }

            UpdateSliderAndInput(Prefs.MasterVolume, masterVolumeSlider, masterVolumeInput);
            masterVolumeSlider.onValueChanged.AddListener(
                e => ProcessFloat(e, Prefs.MasterVolume, masterVolumeSlider, masterVolumeInput, AudioConfiguration.NotifyVolume));
            masterVolumeInput.onValueChanged.AddListener(
                e => ProcessInput(e, Prefs.MasterVolume, masterVolumeSlider, masterVolumeInput, AudioConfiguration.NotifyVolume));
            UpdateSliderAndInput(Prefs.SoundsVolume, soundVolumeSlider, soundVolumeInput);
            soundVolumeSlider.onValueChanged.AddListener(
                e => ProcessFloat(e, Prefs.SoundsVolume, soundVolumeSlider, soundVolumeInput, NotifySoundVolumeChange));
            soundVolumeInput.onValueChanged.AddListener(
                e => ProcessInput(e, Prefs.SoundsVolume, soundVolumeSlider, soundVolumeInput, NotifySoundVolumeChange));
            UpdateSliderAndInput(Prefs.MusicVolume, musicVolumeSlider, musicVolumeInput);
            musicVolumeSlider.onValueChanged.AddListener(
                e => ProcessFloat(e, Prefs.MusicVolume, musicVolumeSlider, musicVolumeInput, AudioConfiguration.NotifyVolume));
            musicVolumeInput.onValueChanged.AddListener(
                e => ProcessInput(e, Prefs.MusicVolume, musicVolumeSlider, musicVolumeInput, AudioConfiguration.NotifyVolume));

            UpdateSliderAndInput(Prefs.Gamma, gammaSlider, gammaInput);
            gammaSlider.onValueChanged.AddListener(
                e => ProcessFloat(e, Prefs.Gamma, gammaSlider, gammaInput));
            gammaInput.onValueChanged.AddListener(
                e => ProcessInput(e, Prefs.Gamma, gammaSlider, gammaInput));
        }

        void Update()
        {
            if (Time.realtimeSinceStartup >= soundSampleNextTimeToPlay)
            {
                // ReSharper disable once RedundantArgumentDefaultValue
                MyAudioSource.PlayClipAtPoint(soundSample, Camera.main!.transform.position, 1f, AudioConfiguration.Type.Sound);
                soundSampleNextTimeToPlay = float.PositiveInfinity;
            }
        }

        private void ProcessInput(string input, Prefs prefs, Slider slider, TMP_InputField inputField, Action callback = null)
        {
            var stripped = input.Replace("%", "").Trim();
            float.TryParse(stripped, out var parsed);
            var result = Mathf.Clamp01(parsed / 100f);
            ProcessFloat(result, prefs, slider, inputField, callback);
        }

        private void ProcessFloat(float volume, Prefs prefs, Slider slider, TMP_InputField inputField, Action callback = null)
        {
            var volumeRounded = Mathf.RoundToInt(volume * 100) / 100f;
            prefs.Set(volumeRounded);
            UpdateSliderAndInput(prefs, slider, inputField, callback);
        }

        private void UpdateSliderAndInput(Prefs prefs, Slider slider, TMP_InputField inputField, Action callback = null)
        {
            var volume = prefs.GetFloat();
            slider.SetValueWithoutNotify(volume);
            inputField.SetTextWithoutNotify($"{Mathf.RoundToInt(volume * 100)}%");
            callback?.Invoke();
        }

        public void GoToMenu()
        {
            Prefs.SettingsDone.Set(true);
            SceneManager.LoadScene("Menu");
        }
    }
}
