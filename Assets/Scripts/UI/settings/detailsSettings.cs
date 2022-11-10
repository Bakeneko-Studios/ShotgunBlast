using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detailsSettings : MonoBehaviour
{    
    private bool enableEnemyHealthBar;
    private GameObject[] enemytags;

    void Start() {
        loadVar();
        enemytags = GameObject.FindGameObjectsWithTag("enemyTag");
        enemyHealthBar();
    }

    void Update() {
        
    }

    public void enemyHealthBarToggle() {
        enableEnemyHealthBar = !enableEnemyHealthBar;
        enemyHealthBar();
    }

    void enemyHealthBar() {
        foreach(GameObject tag in enemytags) {
            tag.SetActive(enableEnemyHealthBar);
        }
    }

    void loadVar() {
        SavedData data = SavingSystem.LoadUser();
        enableEnemyHealthBar = data.healthBarBool;
    }

    public void saveVar() {
        UserSettings UD = GameObject.FindGameObjectWithTag("userSettings").GetComponent<UserSettings>();
        UD.healthBarBool = enableEnemyHealthBar;
    }
}
