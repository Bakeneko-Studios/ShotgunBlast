using System.Collections;
using UnityEngine;

public class SpeedBoost : Upgrade
{
    public int amt;
    public float duration;
    public override IEnumerator use()
    {
        GetComponent<Collider>().enabled=false;
        GetComponent<Renderer>().enabled=false;
        Player.speedMultiplier += amt;
        yield return new WaitForSeconds(duration);
        Player.speedMultiplier -= amt;
        Destroy(this.gameObject);
    }
}
