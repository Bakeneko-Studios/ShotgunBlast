using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class audioSettings : MonoBehaviour
{
    // [SerializeField] private AudioMixer mainMixer;
    // [SerializeField] private Slider masterSlider;
    // [SerializeField] private Slider musicSlider;
    // [SerializeField] private Slider effectsSlider;

    [SerializeField] private GameObject audioPanel;

    // void Start()
    // {
    //     loadVar();
    //     updateVolume();
    // }

    // void Update()
    // {
    //     if(audioPanel.activeInHierarchy) {
    //         updateVolume();
    //     }
    // }

    // void updateVolume() {
    //     mainMixer.SetFloat("MasterMixer", masterSlider.value);
    //     mainMixer.SetFloat("MusicMixer", musicSlider.value);
    //     mainMixer.SetFloat("EffectsMixer", effectsSlider.value);
    // }

    // void loadVar() {
    //     SavedData data = SavingSystem.LoadUser();
    //     masterSlider.value = data.masterVolumeFlt;
    //     musicSlider.value = data.musicVolumeFlt;
    //     effectsSlider.value = data.effectsVolumeFlt;
    // }

    // public void saveVar() {
    //     UserSettings UD = GameObject.FindGameObjectWithTag("userSettings").GetComponent<UserSettings>();
    //     UD.masterVolumeFlt = masterSlider.value;
    //     UD.musicVolumeFlt = musicSlider.value;
    //     UD.effectsVolumeFlt = effectsSlider.value;
    // }
}
