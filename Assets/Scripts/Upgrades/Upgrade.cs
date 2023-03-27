using System.Collections;

public abstract class Upgrade : Interactable
{
    public override void OnInteract()
    {StartCoroutine(use());}
    public abstract IEnumerator use();
}
