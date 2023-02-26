using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmManager : MonoBehaviour
{
    public static bool isBusy, canPunch;
    public static readonly Arm[] arms = {new Arm(),new ReinforcedArm(),new Quake(),new Forcefield(),new EMPArm(),new Grapple(),new Tornado(),new Mech(),new Parasite()};
    public List<Arm> unlockedArms = new List<Arm>();
    public Queue<Arm> equippedArms = new Queue<Arm>();
    public bool randomArms;
    public float swapCooldown;
    public const int maxArms = 5;
    void Awake()
    {
        if(Player.dev) unlockedArms = new List<Arm>(arms);
        else unlockedArms = UserSettings.unlockedArms;
        if(randomArms)
        {
            equippedArms = new Queue<Arm>();
            for (int i = 0; i < maxArms; i++)
            {
                if(unlockedArms.Count==0) break;
                int a = Random.Range(0,unlockedArms.Count);
                equippedArms.Enqueue(unlockedArms[a]);
                unlockedArms.RemoveAt(a);
            }
        }
    }
    void Start()
    {
        isBusy=false;
        canPunch=true;
    }

    void Update()
    {
        if(!isBusy && equippedArms.Count>0 && Time.timeScale>0)
        {
            if(canPunch && Input.GetKeyDown(UserSettings.keybinds["punch"]))
                equippedArms.Peek().punch();
            else if(canPunch && Input.GetKeyDown(UserSettings.keybinds["ability"]))
            {
                equippedArms.Enqueue(equippedArms.Peek());
                equippedArms.Dequeue().useAbility();
            }
            else if(Input.GetKeyDown(UserSettings.keybinds["cycle arm"]))
                equippedArms.Enqueue(equippedArms.Dequeue());
        }
    }
    IEnumerator swapCD()
    {
        isBusy=true;
        yield return new WaitForSeconds(swapCooldown);
        isBusy=false;
    }
}
