using HakSeung;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CUIRunningPanel : CUIScene
{
    private const int TOTALPLAYERS = 2;
    private readonly float progressBarStartX = -650.0f;
    private readonly float progressBarEndX = 670.0f;

    [SerializeField] private float imageOffset = -20.0f;

    [SerializeField] private Canvas runningCanvas;
    [SerializeField] private Image progressBarImage;
    [SerializeField] private RectTransform dinosaurImagePos;
    [SerializeField] private RectTransform[] playerImagepos = new RectTransform[TOTALPLAYERS];
    [SerializeField] private RectTransform player1ImagePos;
    [SerializeField] private RectTransform player2ImagePos;
    [SerializeField] private TextMeshProUGUI endDistanceText;
    [SerializeField] private TextMeshProUGUI dinosaurDistanceText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Image[] heartImages;
    protected override void InitUI()
    {
        float playerPosX = progressBarEndX - progressBarStartX;

        for (int i = 0; i < playerImagepos.Length; i++)
            playerImagepos[i].anchoredPosition = new Vector2(playerPosX, playerImagepos[i].anchoredPosition.y);
    }

    private void SetProgressBar(float progressRate)
    {
        progressBarImage.fillAmount = progressRate / 100.0f;
    }
    private void SetPlayerImage(int playerNumber, float playerDistance, float totalRunningDistance)
    {
        float playerX = (progressBarEndX - progressBarStartX)
            * playerDistance / totalRunningDistance + progressBarStartX + imageOffset;

        playerImagepos[playerNumber].anchoredPosition = new Vector2(playerX, player1ImagePos.anchoredPosition.y);
    }

    private void SetDinosaurImage(float dinosaurDistance, float totalRunningDistance)
    {
        float dinosaurX = (progressBarEndX - progressBarStartX)
            * dinosaurDistance / totalRunningDistance + progressBarStartX + imageOffset;

        dinosaurImagePos.anchoredPosition = new Vector2(dinosaurX, dinosaurImagePos.anchoredPosition.y);
    }

    private void SetDinosaurDistanceText(float lastRankerDistance, float dinosaurDistance)
    {
        int distance = (int)Mathf.Round(lastRankerDistance - dinosaurDistance);
        dinosaurDistanceText.text = string.Format("-{0}M", distance);
    }
    private void SetEndLineDistanceText(float lastRankerDistance, float totalRunningDistance)
    {
        int distance = (int)Mathf.Round(totalRunningDistance - lastRankerDistance);
        endDistanceText.text = string.Format("{0}M", distance);
    }
    private void SetTimer(float roundTimeLimit)
    {
        int hour = (int)(roundTimeLimit / 60.0f);
        int minute = (int)(roundTimeLimit % 60.0f);

        timerText.text = string.Format("{0:D2} : {1:D2}", hour, minute);
    }
}
