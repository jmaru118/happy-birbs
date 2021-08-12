using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public Text enemyText;
    private int enemiesLeft;
    private Enemy[] enemies;
    private TMP_Text victoryText;

    // this function will be called by each enemy that dies
    public void updateEnemyCount()
    {
        enemiesLeft--;
    }

    public void ShowVictoryScreen()
    {
        // show victory screen
        victoryText = FindObjectOfType<TMP_Text>();
        victoryText.fontSize = 115;
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
