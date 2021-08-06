using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private int enemiesLeft;
    private Enemy[] enemies;
    public Text enemyText;


    // this function will be called by each enemy that dies
    public void updateEnemyCount()
    {
        enemiesLeft--;
    }

    // Start is called before the first frame update
    void Start()
    {
        enemies = FindObjectsOfType<Enemy>();
        enemiesLeft = enemies.Length;
    }

    // Update is called once per frame
    void Update()
    {
        enemyText.text = "Enemies Left : " + enemiesLeft;
    }
}
