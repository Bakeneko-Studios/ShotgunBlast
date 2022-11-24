using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pos;
    [SerializeField] private TextMeshProUGUI ang;
    [SerializeField] private TextMeshProUGUI dir;
    [SerializeField] private TextMeshProUGUI vl1;
    [SerializeField] private TextMeshProUGUI vl2;
    [SerializeField] private TextMeshProUGUI con;
    [SerializeField] private TextMeshProUGUI sta;
    [SerializeField] private Transform cam;

    void Update()
    {
        Vector3 p = transform.position;
        pos.text = "Pos: ["+p.x.ToString("F")+", "+p.y.ToString("F")+", "+p.z.ToString("F")+"]";
        Vector3 d = transform.localEulerAngles;
        Vector3 dd = cam.transform.localEulerAngles;
        ang.text = "Ang: ["+(dd.x>180?dd.x-360:dd.x).ToString("F")+", "+(d.y>180?d.y-360:d.y).ToString("F")+", "+(d.z>180?d.z-360:d.z).ToString("F")+"]";
        dir.text = "Dir : ["+(d.y>180?"-X":"+X")+", "+(dd.x<180?"-Y":"+Y")+", "+(d.y<270&&d.y>90?"-Z":"+Z")+"]";
        vl1.text = "Vel: "+movement.velocity.ToString("F");
        vl2.text = "Vl2: "+movement.nvvelocity.ToString("F");
        con.text = "Con: "+Player.playerStatus.ToString();
        sta.text = "Sta: "+Player.state.ToString();
    }
}
