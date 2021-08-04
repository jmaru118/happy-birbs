using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private Enemy[] enemies;
    private static int levelIndex = 1;

    private void OnEnable()
    {
        enemies = FindObjectsOfType<Enemy>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Enemy enemy in enemies)
        {
            if (enemy != null)
                return;

        }

        levelIndex++;
        string nextLevel = "Level" + levelIndex;
        SceneManager.LoadScene(nextLevel);
    }
}
