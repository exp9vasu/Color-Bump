﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Tabtale.TTPlugins;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool GameStarted { get; private set; }
    public bool GameEnded { get; private set; }

    [SerializeField] private float slowMotionFactor = 0.1f;
    [SerializeField] private Transform startTransform;
    [SerializeField] private Transform goalTransform;
    [SerializeField] private Transform ball;
    [SerializeField] private Transform enemy;

    public GameObject Crown_Player;
    public GameObject Crown_Enemy, Confetti;


    public float EntireDistance { get; private set; }
    public float DistanceLeft { get; private set; }

    private void Start()
    {
        EntireDistance = goalTransform.position.z - startTransform.position.z;
    }

    // Start is called before the first frame update
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        TTPCore.Setup();

        Debug.Log("Setup Done!!");

        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
    }

    public void StartGame()
    {
        GameStarted = true;
        Debug.Log("game started");
    }

    public void EndGame(bool win)
    {
        GameEnded = true;
        Debug.Log("game ended");

        if (!win)
        {
            Invoke("ShowRetryPanel", 0.1f);
            Time.timeScale = slowMotionFactor;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
        else if(win)
        {
            //UIManager.instance.ShowWinPanel();
            //Time.timeScale = slowMotionFactor;
            //Time.fixedDeltaTime = 0.02f * Time.timeScale;
            //ball.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    public void StopTime()
    {
        Time.timeScale = 0;
    }

    public void ShowRetryPanel()
    {
        UIManager.instance.ShowRetryPanel();
        StopTime();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PlayerWon()
    {
        Crown_Player.SetActive(true);
        Crown_Enemy.SetActive(false);

        
    }

    public void PlayerLost()
    {
        Crown_Player.SetActive(false);
        Crown_Enemy.SetActive(true);

       
    }

    // Update is called once per frame
    void Update()
    {
        DistanceLeft = Vector3.Distance(ball.transform.position, goalTransform.position);

        if (DistanceLeft > EntireDistance)
        {
            DistanceLeft = EntireDistance;
        }
        
        if(ball.transform.position.z > goalTransform.transform.position.z)
        {
            DistanceLeft = 0;
        }
        Debug.Log("Traveled distance is " + DistanceLeft + "entire distance is" + EntireDistance);

        if (Crown_Enemy.transform.position.z < goalTransform.position.z-1 && Crown_Player.transform.position.z < goalTransform.position.z-1) 
        { if (Crown_Enemy.transform.position.z > Crown_Player.transform.position.z)
            {
                PlayerLost();
            }
            else
            {
                PlayerWon();
            } 
        }
        }
    }

