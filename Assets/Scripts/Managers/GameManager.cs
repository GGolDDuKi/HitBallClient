using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager
{
    GameObject scoreText;
    public GameObject ScoreText
    {
        get
        {
            if (scoreText == null)
                scoreText = GameObject.Find("ScoreText");

            if (scoreText == null)
                Debug.LogError("ScoreText ������Ʈ�� �����ϴ�.");

            return scoreText;
        }
    }

    GameObject timerText;
    public GameObject TimerText
    {
        get
        {
            if (timerText == null)
                timerText = GameObject.Find("TimerText");

            if(timerText == null)
                Debug.LogError("TimerText ������Ʈ�� �����ϴ�.");

            return timerText;
        }
    }

    int TotalRedHitCount { get; set; }

    int score;
    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            if (score == value)
                return;

            score = value;
            ScoreText.GetComponent<TMP_Text>().text = $"Score : {score}";
        }
    }

    float timer;
    public float Timer
    {
        get 
        { 
            return timer;
        }
        set 
        {
            if (timer == value)
                return;

            if (value <= 0)
                value = 0;

            timer = value;
            int ceilingTimer = (int)Math.Ceiling(timer);
            int minutes = (int)(ceilingTimer / 60);
            int seconds = (int)(ceilingTimer % 60);
            TimerText.GetComponent<TMP_Text>().text = $"Timer  {minutes}:{seconds}";

            if (timer <= 0)
                GameOver();
        }
    }

    public bool isFinish;
    public bool isStart;
    int hitCombo;

    GameObject gameStartButton;
    public List<GameObject> balls = new List<GameObject>();

    public void Init()
    {
        Score = 0;
        hitCombo = 0;
        TotalRedHitCount = 0;
        Timer = 120;
        isFinish = false;
        isStart = false;

        //TODO : ���ӽ��� ��ư ������ ���ӽ��� ��ư ����
        if (gameStartButton == null)
        {
            GameObject button = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/GameStartButton"));
            button.transform.SetParent(GameObject.Find("Canvas").transform, false);
            gameStartButton = button;
        }

        //�� ���� ��ġ?
    }

    public void GameStart()
    {
        Init();
        isStart = true;
        gameStartButton = null;
    }

    public void EndTurn()
    {
        hitCombo = 0;
    }

    public void HitRedBall()
    {
        Debug.Log("���� ���� ������ϴ�!");
        TotalRedHitCount++;
        GetScore();
    }

    public void GetScore()
    {
        Score += hitCombo++;
    }

    public void HitYellowBall()
    {
        Debug.Log("��� ���� ������ϴ�..");
        ReduceScore();
    }

    public void ReduceScore()
    {
        Score = Score / 2;
        hitCombo = 0;
    }

    public void GameOver()
    {
        isFinish = true;

        Score newScore = new Score
        {
            TotalScore = Score,
            HitCount = TotalRedHitCount,
            Date = DateTime.Now,
            Id = Managers.Data.Id,
            Major = Managers.Data.Major,
            Name = Managers.Data.Name
        };

        Managers.Instance.StartCoroutine(Managers.Network.CoPostScore(newScore));
    }
}
