using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    // Start is called before the first frame update
    public float health;
    private float max_health;
    private Renderer rend;
    [SerializeField] private Color normColor;
    [SerializeField] private Color dmgColor;
    [SerializeField] private float animationTime;

    void Start()
    {
        rend = GetComponent<Renderer>();
        health = max_health;
        rend.material.color = normColor;
    }

    public IEnumerator takeDamage(float amount)
    {
        health-=amount;
        if(health<=0) die();

        rend.material.color = dmgColor;
        float time=0;

        while(time<1)
        {
            rend.material.color = Color.Lerp(dmgColor,normColor,time);
            time += Time.deltaTime/animationTime;
            yield return null;
        }
        rend.material.color = normColor;

        // rend.material.color = new Color(1f, 1f, 1f, 1f);
        // Debug.Log("color changed");
        // yield return new WaitForSeconds(0.21f);
        // rend.material.color = new Color((0.4f + (max_health - health) / max_health) * 0.6f, (health / max_health) * 0.6f, 0.2f + (health / max_health) * 0.8f, 0.6f);
    }
    
    void die()
    {
        StopAllCoroutines();
        Destroy(this.gameObject);
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.gameObject.tag == "projectile")
    //     {
    //         // //change health and color when hit by projectile
    //         // dmg=other.gameObject.GetComponent<Pellet>().dmg;
    //         // health -= dmg;
    //         // //TODO: Add take damage animation?
    //         // StartCoroutine(takeDamage(-dmg));
    //         //rend.material.color = new Color((0.4f+(max_health-health)/max_health)*0.6f, (health / max_health) * 0.6f, 0.2f+(health/max_health)*0.8f, 0.6f);
    //     }
    // }

    // void Update()
    // {
    //     if (health <= 0f)
    //     {
    //         //TODO: Add destroy animation
    //         Destroy(this.gameObject);
    //     }
    // }
}
