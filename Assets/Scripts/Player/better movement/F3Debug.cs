using UnityEngine;
using TMPro;

public class F3Debug : MonoBehaviour
{
    public static F3Debug instance;
    public GameObject debugPanel;
    public float maxy=-20;
    [SerializeField] private TextMeshProUGUI pos;
    [SerializeField] private TextMeshProUGUI ang;
    [SerializeField] private TextMeshProUGUI dir;
    [SerializeField] private TextMeshProUGUI vl1;
    [SerializeField] private TextMeshProUGUI vxz;
    [SerializeField] private TextMeshProUGUI vly;
    [SerializeField] private TextMeshProUGUI con;
    [SerializeField] private TextMeshProUGUI sta;

    void Awake() {
        instance=this;
        InvokeRepeating("reset",0,5f);
    }
    void Update()
    {
        Vector3 p = movement.instance.transform.position;
        pos.text = "Pos: ["+p.x.ToString("F")+", "+p.y.ToString("F")+", "+p.z.ToString("F")+"]";
        Vector3 d = movement.instance.transform.localEulerAngles;
        Vector3 dd = Camera.main.transform.localEulerAngles;
        ang.text = "Ang: ["+(dd.x>180?dd.x-360:dd.x).ToString("F")+", "+(d.y>180?d.y-360:d.y).ToString("F")+", "+(d.z>180?d.z-360:d.z).ToString("F")+"]";
        dir.text = "Dir : ["+(d.y>180?"-X":"+X")+", "+(dd.x<180?"-Y":"+Y")+", "+(d.y<270&&d.y>90?"-Z":"+Z")+"]";
        vl1.text = "Vel: "+movement.velocity.ToString("F");
        vxz.text = "VXY: "+movement.nvvelocity.ToString("F");
        vly.text = "VlY: "+movement.yvelocity.ToString("F");
        if(movement.instance.transform.position.y>maxy) maxy=movement.instance.transform.position.y;
        con.text = "Con: "+Player.playerStatus.ToString();
        sta.text = "Sta: "+Player.state.ToString();
    }
    void reset()
    {maxy=-20;}
}
