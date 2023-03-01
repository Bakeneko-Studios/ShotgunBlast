using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    // Start is called before the first frame update
    public float health;
    private float max_health;
    private Renderer rend;
    private int dmg;

    void Start()
    {
        rend = GetComponent<Renderer>();
        max_health = health;
        rend.material.color = new Color(0f, 0f, 0.33f, 1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "projectile")
        {
            //change health and color when hit by projectile
            dmg=other.gameObject.GetComponent<Pellet>().dmg;
            health -= dmg;
            //TODO: Add take damage animation?
            rend.material.color = new Color(((max_health-health)/max_health)*0.85f, 0f, (health/(max_health*3)), 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0f)
        {
            //TODO: Add destroy animation
            Destroy(this.gameObject);
        }
    }
}
