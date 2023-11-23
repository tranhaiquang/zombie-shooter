using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    public int enemiesAlive = 0;

    public int round = 0;

    [SerializeField] private GameObject[] spawnPoints;

    [SerializeField] private GameObject[] enemyPrefab;

    [SerializeField] private TMP_Text roundNumber;
    [SerializeField] private Text roundsSurvived;
    [SerializeField] private GameObject endScreen;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemiesAlive == 0)
        {
            round++;
            NextWave(round);
            roundNumber.text = "Round: " + round.ToString();
        }
    }

    public void NextWave(int round)
    {
        for (var x = 0; x < round; x++)
        {
            GameObject spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            GameObject enemySpawned = Instantiate(enemyPrefab[Random.Range(0, enemyPrefab.Length)], spawnPoint.transform.position, Quaternion.identity);
            enemySpawned.GetComponent<EnemyController>().gameManager = GetComponent<GameManager>();
            enemiesAlive++;
        }

    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void EndGame()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        roundNumber.enabled = false;
        endScreen.SetActive(true);
        roundsSurvived.text = round.ToString();
    }

}
