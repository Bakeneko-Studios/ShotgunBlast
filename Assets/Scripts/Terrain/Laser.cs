using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float startX;
    public float startZ;
    public float maxY;
    public float minY;
    private float direction;
    private float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = new Vector3(startX, Random.Range(minY,maxY), startZ);
        direction = 1f;
        speed = Random.Range(0.004f, 0.02f);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y >= maxY)
        {
            direction = -1f;
            speed = Random.Range(0.04f, 0.2f);
        }
        if (gameObject.transform.position.y <= minY)
        {
            direction = 1f;
            speed = Random.Range(0.04f, 0.2f);
        }

        gameObject.transform.position += new Vector3(0f, direction*speed, 0f);
    }
}
