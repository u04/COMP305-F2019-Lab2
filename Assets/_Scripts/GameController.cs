﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;
using System;


public class GameController : MonoBehaviour
{
    [Header("Scene Game Objects")]
    public GameObject cloud;
    public GameObject island;
    public int numberOfClouds;
    public List<GameObject> clouds;

    [Header("Audio Sources")]
    public SoundClip activeSoundClip;
    public AudioSource[] audioSources;

    [Header("Scoreboard")]
    [SerializeField]
    private int _lives;

    [SerializeField]
    private int _score;

    public Text livesLabel;
    public Text scoreLabel;
    public Text highScoreLabel;



    //public HighScoreSO highScoreSO;

    [Header("UI Control")]
    public GameObject startLabel;
    public GameObject startButton;
    public GameObject endLabel;
    public GameObject restartButton;

    [Header("Game Settings")]
    public ScoreBoard scoreBoard;

    // public properties
    [Header("Scene Settings")]
    public SceneSettings activeSceneSettiings;
    public List<SceneSettings> SceneSettings;
    public int Score
    {
        get
        {
            return _score;
        }

        set
        {
            _score = value;
            scoreBoard.score = _score;


            if (scoreBoard.highScore < _score)
            {
                scoreBoard.highScore = _score;
            }
            scoreLabel.text = "Score: " + _score;
        }
    }
    public int Lives
    {
        get
        {
            return _lives;
        }
        set
        {
            _lives = value;
            scoreBoard.lives = _lives;
            if (_lives < 1)
            {
                SceneManager.LoadScene("End");
            }
            else
            {
                livesLabel.text = "Lives: " + _lives;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObjectInitialization();
        SceneConfiguration();
    }

    private void GameObjectInitialization()
    {
        startLabel = GameObject.Find("StartLabel");
        endLabel = GameObject.Find("EndLabel");
        startButton = GameObject.Find("StartButton");
        restartButton = GameObject.Find("RestartButton");

        //scoreBoard = Resources.FindObjectsOfTypeAll<ScoreBoard>()[0] as ScoreBoard;
    }


    private void SceneConfiguration()
    {
        var query = from settings in SceneSettings
                    where settings.scene == (Scene)Enum.Parse(typeof(Scene), SceneManager.GetActiveScene().name.ToUpper())
                    select settings;

        activeSceneSettiings = query.ToList().First();
        {


            if (activeSceneSettiings.scene == Scene.MAIN)
            {
                Lives = 5;
                Score = 0;
            }
             activeSoundClip = activeSceneSettiings.activeSoundClip;
            scoreLabel.enabled = activeSceneSettiings.scoreLabelEnabled;
            livesLabel.enabled = activeSceneSettiings.livesLabelEnabled;
            highScoreLabel.enabled = activeSceneSettiings.highScoreLabelEnabled;
            startLabel.SetActive(activeSceneSettiings.startLabelActive);
            endLabel.SetActive(activeSceneSettiings.endLabelActive);
            startButton.SetActive(activeSceneSettiings.startButtonActive);
            restartButton.SetActive(activeSceneSettiings.restartButtonActive);
            // assigns text values to labels from the scoreboard Scriptable object
            livesLabel.text = "Lives: " + scoreBoard.lives;
            scoreLabel.text = "Score: " + scoreBoard.score;
            highScoreLabel.text = "High Score: " + scoreBoard.highScore;
        }


        // switch (SceneManager.GetActiveScene().name)
        // {
        //     case "Start":
        //         scoreLabel.enabled = false;
        //         livesLabel.enabled = false;
        //         highScoreLabel.enabled = false;
        //         endLabel.SetActive(false);
        //         restartButton.SetActive(false);
        //         activeSoundClip = SoundClip.NONE;
        //         break;
        //     case "Main":
        //         highScoreLabel.enabled = false;
        //         startLabel.SetActive(false);
        //         startButton.SetActive(false);
        //         endLabel.SetActive(false);
        //         restartButton.SetActive(false);
        //         activeSoundClip = SoundClip.ENGINE;
        //         break;
        //     case "End":
        //         scoreLabel.enabled = false;
        //         livesLabel.enabled = false;
        //         startLabel.SetActive(false);
        //         startButton.SetActive(false);
        //         activeSoundClip = SoundClip.NONE;
        //         highScoreLabel.text = "High Score: " + scoreBoard.highScore;
        //         break;
        // }

        if ((activeSoundClip != SoundClip.NONE) && (activeSoundClip != SoundClip.NUM_OF_CLIPS))
        {
            AudioSource activeAudioSource = audioSources[(int)activeSoundClip];
            activeAudioSource.playOnAwake = true;
            activeAudioSource.loop = true;
            activeAudioSource.volume = 0.5f;
            activeAudioSource.Play();
        }







        // creates an empty container (list) of type GameObject

        clouds = new List<GameObject>();



        for (int cloudNum = 0; cloudNum < numberOfClouds; cloudNum++)

        {

            clouds.Add(Instantiate(cloud));

        }



        Instantiate(island);
    }
    // Event Handlers
    public void OnStartButtonClick()
    {
        SceneManager.LoadScene("Main");
    }

    public void OnRestartButtonClick()
    {
        SceneManager.LoadScene("Main");
    }
}
