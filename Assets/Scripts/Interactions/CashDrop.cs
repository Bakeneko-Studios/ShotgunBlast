using UnityEngine;

public class CashDrop : Interactable
{
    public static GameObject drop;
    public int min,max;
    public int amount;
    private void Awake()
    {
        amount = Random.Range(min,max);
    }
    public override void OnInteract()
    {
        Player.ChangeBallz(amount);
        Destroy(this.gameObject);
    }
    public static void DropCash(int min,int max,Transform t)
    {
        CashDrop c = GameObject.Instantiate(drop,t.position,t.rotation).GetComponent<CashDrop>();
        c.min = min;
        c.max = max;
    }
}
