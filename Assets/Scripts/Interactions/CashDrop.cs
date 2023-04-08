using UnityEngine;

public class CashDrop : Interactable
{
    private int min,max;
    public override void OnInteract()
    {
        Player.ChangeBallz(Random.Range(min,max));
        Debug.Log("Ballz: "+Player.ballz);
        Destroy(this.gameObject);
    }
    public static void DropCash(GameObject drop,int min,int max,Transform t)
    {
        CashDrop c = GameObject.Instantiate(drop,t.position,t.rotation).GetComponent<CashDrop>();
        c.min = min;
        c.max = max;
    }
}
