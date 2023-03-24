using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class helth : MonoBehaviour
{
    public int amt;
    public void healthPack()
    {
        //heal for 30 and destroy pbject
        playerHealth.instance.ChangeHealth(amt);
        //can be slow regeneration or instant heal
        Destroy(this.gameObject);
    }
}
