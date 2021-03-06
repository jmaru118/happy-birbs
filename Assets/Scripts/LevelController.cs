using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private Bird[] birds;
    private Enemy[] enemies;
    private CameraController cameraController;
    private int birdIndex = 0;
    private int enemiesDefeated = 0;                           // if we defeated totalEnemies we can progress
    private int totalEnemies;                                  // lets us know how many enemies to defeat
    private static int levelIndex = 1;
    private UIController uiController;                        // call our ui controller to show win screen

    // this function will be called by a bird as it is being destroyed
    // and will only work if the player has not yet won
    public void ReadyUpNextBird()
    {
        if (enemiesDefeated < totalEnemies)
        {
            birdIndex++;

            // reload scene if we've expended all our birds
            if (birdIndex == birds.Length)
            {
                string currentSceneName = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene(currentSceneName);
            }

            // activate the next bird
            if (birds[birdIndex] != null)
                birds[birdIndex].ActivateBird();
        }
    }

    public void countEnemyDefeated()
    {
        enemiesDefeated++;
        // check if all enemies defeated and progress to next level
        if (enemiesDefeated == totalEnemies)
        {
            levelIndex++;
            string nextLevel = "Level" + levelIndex;
            //SceneManager.LoadScene(nextLevel);
            ShowWinScreen();
        }
    }

    private void OnEnable()
    {
        // find all enemies so we can progress to the next level
        enemies = FindObjectsOfType<Enemy>();
        // keep track of our birds
        birds = FindObjectsOfType<Bird>();
        // update our totals for birds and enemies
        totalEnemies = enemies.Length;
    }

    // Start is called before the first frame update
    void Start()
    {
        // activate the first bird
        birds[birdIndex].ActivateBird();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ShowWinScreen()
    {
        uiController = FindObjectOfType<UIController>();
        uiController.ShowVictoryScreen();
    }
}
