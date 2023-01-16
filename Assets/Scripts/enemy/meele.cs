using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meele : MonoBehaviour
{

    public float bladeDamage = 5;
    public Collider[] blades;
    public float attackDuration = 0.5f; //time blades can deal damage

    IEnumerator removeColliders()
    {
        yield return new WaitForSeconds(attackDuration);
        for (var i = 0; i < blades.Length; i++)
            blades[i].enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<playerHealth>().ChangeHealth(-bladeDamage);
        }
    }

    public void attack()
    {
        for (var i = 0; i < blades.Length; i++)
            blades[i].enabled = true;
        StartCoroutine(removeColliders());
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
