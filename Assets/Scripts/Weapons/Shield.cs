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
        rend.material.color = new Color(0.4f, 0.6f, 1f, 0.6f);
    }

    IEnumerator takeDamage()
    {
        rend.material.color = new Color(1f, 1f, 1f, 1f);
        Debug.Log("color changed");
        yield return new WaitForSeconds(0.21f);
        rend.material.color = new Color((0.4f + (max_health - health) / max_health) * 0.6f, (health / max_health) * 0.6f, 0.2f + (health / max_health) * 0.8f, 0.6f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "projectile")
        {
            //change health and color when hit by projectile
            dmg=other.gameObject.GetComponent<Pellet>().dmg;
            health -= dmg;
            //TODO: Add take damage animation?
            StartCoroutine(takeDamage());
            //rend.material.color = new Color((0.4f+(max_health-health)/max_health)*0.6f, (health / max_health) * 0.6f, 0.2f+(health/max_health)*0.8f, 0.6f);
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
