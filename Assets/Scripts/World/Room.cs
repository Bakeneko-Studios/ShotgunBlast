using UnityEngine;

[System.Serializable] public class Room
{
    public GameObject structure;
    public Vector3 northDoor;
    public Vector3 eastDoor;
    public Vector3 southDoor;
    public Vector3 westDoor;
    public Room(Room r)
    {
        structure=r.structure;
        northDoor=r.northDoor;
        eastDoor=r.eastDoor;
        southDoor=r.southDoor;
        westDoor=r.westDoor;
    }
}
