using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraShake : MonoBehaviour
{

    public AnimationCurve curve; // for smoothing camera shakes

    public IEnumerator shakeCamera(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;
        float elasped = 0.0f;
        while (elasped < duration)
        {
            elasped += Time.deltaTime;
            float strength = curve.Evaluate(elasped/duration) * magnitude;
            transform.localPosition = originalPos + Random.insideUnitSphere * strength;
            yield return null;
        }
        transform.localPosition = originalPos;
    }
}
