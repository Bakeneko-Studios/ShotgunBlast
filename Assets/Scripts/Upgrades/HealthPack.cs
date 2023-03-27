using System.Collections;
using UnityEngine;

public class HealthPack : Upgrade
{
    public int amt;
    public float duration;
    public int ticks;
    public override IEnumerator use()
    {
        GetComponent<Collider>().enabled=false;
        GetComponent<Renderer>().enabled=false;
        for (int i = 0; i < ticks; i++)
        {
            playerHealth.instance.ChangeHealth(amt/duration);
            yield return new WaitForSeconds(duration/ticks);
        }
        Destroy(this.gameObject);
    }
}
