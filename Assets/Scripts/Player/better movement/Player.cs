using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Player
{
    public enum contact
    {ground,air,wall,ledge}
    public enum MoveState
    {idle,walk,sprint,crouch,air}
    public static contact playerStatus;
    public static MoveState state;
}
