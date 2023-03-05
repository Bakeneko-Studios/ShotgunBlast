using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class audioSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer mainMixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider effectsSlider;
    public float volumeMas;
    public float volumeM;
    public float volumeE;

    [SerializeField] private GameObject audioPanel;

    void Awake()
    {
        loadVar();
    }

    public void updateVolume() {
        mainMixer.SetFloat("MasterMix", masterSlider.value);
        mainMixer.SetFloat("MusicMix", musicSlider.value);
        mainMixer.SetFloat("EffectsMix", effectsSlider.value);
        saveVar();
    }

    void loadVar() {
        masterSlider.value = UserSettings.masterVolumeFlt;
        musicSlider.value = UserSettings.musicVolumeFlt;
        effectsSlider.value = UserSettings.effectsVolumeFlt;
    }

    public void saveVar() {
        UserSettings.masterVolumeFlt = masterSlider.value;
        UserSettings.musicVolumeFlt = musicSlider.value;
        UserSettings.effectsVolumeFlt = effectsSlider.value;
        SavingSystem.SaveUser();
    }

    public void resetVar() {
        UserSettings.resetAudio();
        loadVar();
    }
}
