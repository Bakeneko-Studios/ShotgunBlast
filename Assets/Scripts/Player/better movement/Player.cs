using UnityEngine;
public static class Player
{
    public static readonly bool dev = true;
    public enum contact
    {ground,air,wall,ledge}
    public enum MoveState
    {idle,walk,sprint,crouch,slide,air,freecam}
    public static contact playerStatus;
    public static MoveState state;
    public static bool paused = false;
    public static float hp = 100;
    public static float maxHealth = 100;
    public static float speedMultiplier = 1;
    public static float damageMultiplier = 1; //not sure whether to make it an absolute or percent buff
    public static float damageResistance = 0;
    public static bool invincible = false;
    public static float firerateMultiplier = 1;
    public static int ballz;
    public static float ballzMultiplier;

    public static void reset()
    {
        paused = false;
        hp = 100;
        speedMultiplier = 1;
        damageMultiplier = 1;
        invincible = false;
        firerateMultiplier = 1;
    }

    public static void ChangeBallz(int amt)
    {
        if(amt>0) ballz += Mathf.RoundToInt(amt*ballzMultiplier);
        else ballz += amt;
        if(ballz<0) ballz=0;
    }
    public static void ChangeBallz(char mode, int amt) //'m' for mutliply, 'd' for divide
    {
        if(mode=='m') ballz *= amt;
        else if(mode=='d') ballz = Mathf.RoundToInt((float) ballz / amt);
        else ChangeBallz(amt);
    }
}
