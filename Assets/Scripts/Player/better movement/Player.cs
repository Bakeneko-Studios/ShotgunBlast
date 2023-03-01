using System.Collections;
using System.Collections.Generic;
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
}
