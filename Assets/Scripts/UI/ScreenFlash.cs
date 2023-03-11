using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class ScreenFlash : MonoBehaviour
{
    public static ScreenFlash instance;
    private PostProcessVolume volume;
    [SerializeField] private Image flashL,flashR;
    [SerializeField] private Image HealL,HealR;
    public float maxIntensity, duration;
    private float intensity;

    public Color dmgColor, healColor, boostColor, deathColor;
    [SerializeField] private Color flashColor;

    void Awake() {instance=this;}
    void Start()
    {
        volume = Camera.main.GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out Bloom bloom);
        if(bloom!=null) intensity = bloom.intensity;
    }

    public void Flash(string colorName)
    {
        StopAllCoroutines();
        Color effectColor = (Color)this.GetType().GetField(colorName).GetValue(this); //find variable with name
        StartCoroutine(flashEffect(effectColor));
    }

    public void RegenFlash()
    {
        StopAllCoroutines();
        StartCoroutine(HealingEffect(healColor));
    }

    IEnumerator flashEffect(Color flashColor)
    {
        float endTime = Time.time+duration;
        while(Time.time < endTime)
        {
            float t = Mathf.Clamp01((endTime-Time.time)/duration);
            Color color = Color.Lerp(Color.clear,flashColor,t);
            flashL.color = flashR.color = color;
            volume.profile.TryGetSettings(out Bloom bloom);
            if(bloom!=null) bloom.intensity.value = Mathf.Lerp(intensity,maxIntensity,t);
            yield return null;
        }
        flashL.color = flashR.color = Color.clear;
    }

    IEnumerator HealingEffect(Color flashColor)
    {
        duration *= 0.33f;
        float endTime = Time.time+duration;
        while(Time.time < endTime)
        {
            float t = Mathf.Clamp01((endTime-Time.time)/duration);
            Color color = Color.Lerp(Color.clear,flashColor,t);
            Color lightColor = color; lightColor.a = color.a * 0.5f;
            HealL.color = HealR.color = lightColor;
            yield return null;
        }
        HealL.color = HealR.color = Color.clear;
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
