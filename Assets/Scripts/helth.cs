using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class helth : MonoBehaviour
{
    public void healthPack()
    {
        //heal for 30 and destroy pbject
        playerHealth.instance.ChangeHealth(30);
        //can be slow regeneration or instant heal
        Destroy(this.gameObject);
    }
}
