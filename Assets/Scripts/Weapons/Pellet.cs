using System.Collections;
using UnityEngine;

public class Pellet : MonoBehaviour
{
    public int dmg;
    //!bool collideDetect = false;
    //TODO Pellets will destory cuz when they spawn they trigger OnTriggerEnter, make a tag so that they dont get destroyed by eachother
    //void Start() {StartCoroutine(waitLife());}//? this thing destroys the pellets after 10 seconds, may conflict witht the damage
    IEnumerator waitLife()
    {
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }
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

        if (other.gameObject.GetComponent<Dummy>() != null)
        {
            other.gameObject.GetComponent<Dummy>().minusHP(dmg);
        }
    }
}
