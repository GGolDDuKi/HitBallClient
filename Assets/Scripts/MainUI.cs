using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{

    public GameObject LoginPanel;
    public GameObject RankingPanel; // �α��� & ��ŷ �г�
    public GameObject LoginFailPanel;

    public Button LoginExitButton;
    public Button RankingExitButton; // �α��� & ��ŷ ������ ��ư
    public Button FailButton;

    public Button StartButton;
    public Button RankingButton;    // ���� ��ư�� ��ŷ ��ư

    public Button GameStartButton; // ���� ���� ��ư (�α��� ����)


    void Start()
    {
        LoginPanel.SetActive(false); 
        RankingPanel.SetActive(false); // �α��� & ��ŷ �г� ��Ȱ��ȭ
        LoginFailPanel.SetActive(false);

        StartButton.onClick.AddListener(StartButtonClick);
        RankingButton.onClick.AddListener(RankingButtonClick);

        LoginExitButton.onClick.AddListener(LoginExit);
        RankingExitButton.onClick.AddListener(RankingExit);
        FailButton.onClick.AddListener(FailExit);

        // GameStartButton.onClick.AddListener(GameStart);
    }

    void StartButtonClick()
    {
        LoginPanel.SetActive(true); // �α��� �г� Ȱ��ȭ
    }

    void RankingButtonClick()
    {
        RankingPanel.SetActive(true); // ��ŷ �г� Ȱ��ȭ
    }

    void LoginExit()
    {
        LoginPanel.SetActive(false); // �α��� �г� ����
    }

    void RankingExit()
    {
        RankingPanel.SetActive(false); // ��ŷ �г� ����
    }

    void FailExit()
    {
        LoginFailPanel.SetActive(false);
    }

}
