using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraShake : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator shakeCamera(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;
        float elasped = 0.0f;
        while (elasped < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            transform.localPosition = new Vector3(x, y, originalPos.z);
            elasped += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalPos;
    }
}
