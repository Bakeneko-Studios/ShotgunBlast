public class TalkToNPC : Interactable
{
    public override void OnInteract()
    {
        DialogueBox d = GetComponentInChildren<DialogueBox>();
        if(d!=null) d.Talk();
    }
}
