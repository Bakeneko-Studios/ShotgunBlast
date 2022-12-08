using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Player
{
    public enum contact
    {ground,air,wall,ledge}
    public enum MoveState
    {idle,walk,sprint,crouch,slide,air}
    public static contact playerStatus;
    public static MoveState state;
    public static float hp = 100;
}
