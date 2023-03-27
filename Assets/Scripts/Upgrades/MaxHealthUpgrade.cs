using System.Collections;

public class MaxHealthUpgrade : Upgrade
{
    public int amt;
    public override IEnumerator use()
    {
        Player.maxHealth += amt;
        yield return null;
        Destroy(this.gameObject);
    }
}
