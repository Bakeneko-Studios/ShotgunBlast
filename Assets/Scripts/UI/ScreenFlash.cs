using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class ScreenFlash : MonoBehaviour
{
    public static ScreenFlash instance;
    [SerializeField] private PostProcessVolume volume;
    [SerializeField] private Image image;
    public float maxIntensity, duration;
    private float intensity;

    public Color dmgColor, healColor, boostColor, deathColor;
    [SerializeField] private Color flashColor;

    void Awake() {instance=this;}
    void Start()
    {
        volume.profile.TryGetSettings(out Bloom bloom);
        if(bloom!=null) intensity = bloom.intensity;
    }

    public void DmgFlash()
    {
        StopAllCoroutines();
        StartCoroutine(flashEffect(dmgColor));
    }
    public void HealFlash()
    {
        StopAllCoroutines();
        StartCoroutine(flashEffect(healColor));
    }
    public void BoostFlash()
    {
        StopAllCoroutines();
        StartCoroutine(flashEffect(boostColor));
    }
    public void DeathFlash()
    {
        StopAllCoroutines();
        StartCoroutine(flashEffect(deathColor));
    }

    IEnumerator flashEffect(Color flashColor)
    {
        float endTime = Time.time+duration;
        while(Time.time < endTime)
        {
            float t = Mathf.Clamp01((endTime-Time.time)/duration);
            Color color = Color.Lerp(Color.clear,flashColor,t);
            image.color = color;
            volume.profile.TryGetSettings(out Bloom bloom);
            if(bloom!=null) bloom.intensity.value = Mathf.Lerp(intensity,maxIntensity,t);
            yield return null;
        }
        image.color = Color.clear;
    }


    // IEnumerator flashEffect(Color flashColor)
    // {
    //     // if(bloom!=null) bloom.active = true;

    //     // float startTime = Time.time;
    //     // float endTime = startTime+duration;

    //     // while(Time.time < endTime)
    //     // {
    //     //     float l = Mathf.PingPong((Time.time-startTime)*intensity, 1f);
    //     //     Color flash = Color.Lerp(originalColor,flashColor,l);
    //     //     material.color = flash;
    //     //     yield return null;
    //     // }

    //     // material.color = originalColor;
    //     // if(bloom!=null) bloom.active = false;
    // }
}
