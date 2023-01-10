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
        updateVolume();
    }

    void updateVolume() {
        mainMixer.SetFloat("MasterMix", masterSlider.value);
        mainMixer.SetFloat("MusicMix", musicSlider.value);
        mainMixer.SetFloat("EffectsMix", effectsSlider.value);
    }

    void loadVar() {
        SavedData data = SavingSystem.LoadUser();
        masterSlider.value = data.masterVolumeFlt;
        musicSlider.value = data.musicVolumeFlt;
        effectsSlider.value = data.effectsVolumeFlt;
    }

    public void saveVar() {
        UserSettings UD = GameObject.FindGameObjectWithTag("userSettings").GetComponent<UserSettings>();
        UD.masterVolumeFlt = masterSlider.value;
        UD.musicVolumeFlt = musicSlider.value;
        UD.effectsVolumeFlt = effectsSlider.value;
    }

    public void resetVar() {
        masterSlider.value = 0;
        musicSlider.value = 0;
        effectsSlider.value = 0;
        saveVar();
    }

    public void masterChange()
    {
        mainMixer.SetFloat("MasterMix", masterSlider.value);
    }
    public void musicChange()
    {
        mainMixer.SetFloat("MusicMix", masterSlider.value);
    }
    public void effectsChange()
    {
        mainMixer.SetFloat("EffectsMix", masterSlider.value);
    }
    
}
