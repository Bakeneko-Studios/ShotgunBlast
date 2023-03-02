using System.Collections;
using UnityEngine;

public class Pellet : MonoBehaviour
{
    private float life = 5f;
    public float pelletSpeed;
    public int dmg;
    int num;
    public bool playersBullet = false;
    //!bool collideDetect = false;
    //TODO Pellets will destory cuz when they spawn they trigger OnTriggerEnter, make a tag so that they dont get destroyed by eachother
    //void Start() {StartCoroutine(waitLife());}//? this thing destroys the pellets after 10 seconds, may conflict witht the damage
    // IEnumerator waitLife()
    // {
    //     yield return new WaitForSeconds(life);
    //     Destroy(this.gameObject);
    // }
    private void OnTriggerEnter(Collider other) 
    {

        //? OK so this thing, it makes the damage clunky, but didnt delete cuz may be useful for reference
        // if (other.gameObject.GetComponent<Pellet>() == null)
        // {
        //     if (other.gameObject.GetComponent<Dummy>() != null)
        //     {
        //         other.gameObject.GetComponent<Dummy>().minusHP(50);
        //     }
        //     Destroy(this.gameObject);
        // }

        if (other.gameObject.GetComponent<enemyFramework>() != null && playersBullet)
        {   
            other.gameObject.GetComponent<enemyFramework>().ChangeHealth(-dmg);
        }
        if (other.gameObject.tag == "Player" && !playersBullet)
        {
            other.gameObject.GetComponent<playerHealth>().ChangeHealth(-dmg);
        }
        if (other.gameObject.tag == "Shield")
        {
            Destroy(this.gameObject);
        }
    }
    private void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), 5))
        {
            life = Time.deltaTime * 1f;
        }
    }
    void Update()
    {
        if (life <= 0)
        {
            Destroy(this.gameObject);
        }
        life -= Time.deltaTime;
    }
}
