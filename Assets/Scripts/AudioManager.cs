using UnityEngine.Audio;    
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    GameObject player;
    public AudioSource AS;
    public AudioClip backGround;
    void Start() 
    {
        player = this.gameObject;
        AS = GetComponent<AudioSource>();
        AS.loop = true;
        AS.PlayOneShot(backGround);
    }
    void Update() 
    {
        
    }
}
