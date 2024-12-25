using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    public static GameState Instance { get; private set; }
    private bool isCrateDone = false;
    private bool isGameOver = false;
    private int keysPicked = 0;
    private int circlePointCaptured = 0;
    private readonly int totalKeys = 2;
    private readonly int totalCirclePoint = 2;
    private bool cpHelper = true; // to prevent a condition in infinite loop
    private float courseTime = 0f;
    public int GetKeysPicked() { return keysPicked; }
    public int GetCirclePointCaptured() { return circlePointCaptured; }
    public int GetTotalKeys() { return totalKeys; }
    public int GetTotalCirclePoint() { return totalCirclePoint; }
    public bool IsGameOver() { return isGameOver; }

    public void SetIsCrateDone(bool isCrateDone) { this.isCrateDone = isCrateDone; }
    public void SetGoldKeysPicked(int goldkey) {  keysPicked = goldkey; }
    public void SetCirclePointCaptured(int circlepoint) { circlePointCaptured = circlepoint; }

    [SerializeField] private GameObject endingCP;
    [SerializeField] private GameObject progressUI;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject gameOverUI;

    private GameObject[] questCP;
    private Vector3 spawnpoint;

    public GameObject GetProgressUI() { return progressUI; }
    public GameObject GetEndingCP() { return endingCP; }

    private void Awake()
    {
        GameStart();
    }

    private void Update()
    {
        if(!isGameOver) courseTime = Time.time;

        Instance = this;

        if (isCrateDone)
        {
            EnableLastCirclePoint();

            if (EndingCirclePoint.Instance.GetIsGameOver())
            {
                GameOver();
                isGameOver = true;
            }

        }

        if(totalKeys == keysPicked && cpHelper)
        {
            EnableDisableCirclePoint(0, true);
            cpHelper = false;
        }

    }

    private void GameStart()
    {
        Instance = this;
        try
        {
            spawnpoint = GameObject.FindGameObjectWithTag("Player").transform.position;
            gameOverUI.SetActive(false);       
            progressUI.SetActive(false);
            endingCP.SetActive(false);
            gameOverScreen.SetActive(false);

            questCP = GameObject.FindGameObjectsWithTag("CircleQuestParent");
            EnableDisableCirclePoint(0, false);
        }
        catch (NullReferenceException ex)
        {
            Debug.LogError(ex.ToString());
        }
    }

    public void Respawn()
    {
        print("RESPAWNED");
        GameObject.FindGameObjectWithTag("Player").transform.position = spawnpoint;

    }

    private void GameOver()
    {
        if (!isGameOver)
        {
            print("GAMEOVER");

            gameOverScreen.SetActive(true);
            GameObject[] zombies = GameObject.FindGameObjectsWithTag("ParentAI");
            GameObject.FindGameObjectWithTag("EnemySpawn").SetActive(false);

            for (int i = 0; i < zombies.Length; i++)
            {
                zombies[i].GetComponent<NavMeshAgent>().enabled = false;
                zombies[i].GetComponent<ZombieController>().enabled = false;
                zombies[i].GetComponentInChildren<Animator>().enabled = false;
            }

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().enabled = false;
        }
        else
        {
            if (SemiEnding.Instance.GetIsDone())
            {
                print("GAMOVERUI");
                GameObject.FindGameObjectWithTag("Typewriter")?.SetActive(false);

                gameOverUI.SetActive(true);

                TimeSpan timeSpan = TimeSpan.FromSeconds(courseTime);

                string formattedTime = timeSpan.ToString(@"mm\:ss");

                UISystem.Instance.GameOverDataUI(formattedTime);

            }
        }
    }

    public void RestartGame()
    {
        // Get the name of the current scene
        try
        {
            string currentScene = SceneManager.GetActiveScene().name;

            // Reload the current scene
            SceneManager.LoadScene(currentScene);

            print("DONE RESTART");
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    public void GoBackToMainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }

    private void EnableLastCirclePoint()
    {
        endingCP.SetActive(true);
    }

    public void EnableDisableCirclePoint(int idx, bool isEnable)
    {
        int x = CircleParentHelper(idx);

        if (x != -1)
        {
            print(isEnable);
            questCP[x].SetActive(isEnable);
        }
        else
        {
            Debug.Log("No CircleQuestParent Found");
        }
    }

    public int CircleParentHelper(int idx)
    {
        for (int i = 0; i < questCP.Length; i++)
        {
            print(questCP[i].GetComponentInChildren<CirclePoint>().index + "LOOP");
            if (questCP[i].GetComponentInChildren<CirclePoint>().index == idx)
            {
                print(questCP[i].GetComponentInChildren<CirclePoint>().index + questCP[i].name);
                return i;
            }
        }

        return -1;
    }

}
