using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    public static HUD instance;
    public GameObject hint;

    [Header("Weapon Display")]
    public GameObject Weapon;
    public bool yesgun;
    
    [Header("Healthbar Display")]
    [SerializeField] private Image hudHealthBar;
    [SerializeField] private Image hudHealthBarRolling;
    [SerializeField] private GameObject EnergyCells;
    private GameObject[] cellImages;
    float smoothTime = 0.4f;
    // public Image menuHealthBar;
    public TextMeshProUGUI hpText;
    
    void Awake() { instance=this; }
    
    void Start()
    {
        cellImages = new GameObject[5];
        playerHealth.instance.GetComponent<PlayerAction>().hint = hint; //force feed anda's code
        //this is incredibally dumb, hint should be accquired on the interactable object, since diffrent hint texts may be used
        cellImages[0] = EnergyCells.transform.Find("Cell1").gameObject;
        cellImages[1] = EnergyCells.transform.Find("Cell2").gameObject;
        cellImages[2] = EnergyCells.transform.Find("Cell3").gameObject;
        cellImages[3] = EnergyCells.transform.Find("Cell4").gameObject;
        cellImages[4] = EnergyCells.transform.Find("Cell5").gameObject;
        
    }

    public void ChangeGun(bool y) {
        HUD.instance.Weapon.SetActive(y);
        yesgun = y;
    }

    public void ChangeHealth(float healthPercent) {
        hudHealthBar.fillAmount = healthPercent;
        StartCoroutine(SmoothDampCoroutine(healthPercent));        
    }

    public void ChangeCell(int cellnum, bool active) //negative to reduce, positive to add
    {
        cellImages[cellnum-1].SetActive(active);
    }

    private IEnumerator SmoothDampCoroutine(float healthPercent) {
        float velo = 0.0f;
        while (hudHealthBarRolling.fillAmount - healthPercent > 0.001f) {  
            float currentAmount = hudHealthBarRolling.fillAmount;
            hudHealthBarRolling.fillAmount = Mathf.SmoothDamp(currentAmount,healthPercent,ref velo,smoothTime);
            yield return null;
        }
    }
}


