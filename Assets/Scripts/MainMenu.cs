using System;
using UnityEngine.UI;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public Player MainPlayer;
    public Text ScoreLabel;

    public void StartGame(int mode)
    {
        MainPlayer.StartGame(mode);
        gameObject.SetActive(false);
    }

    public void EndGame(float distanceTravelled)
    {
        ScoreLabel.text = "Distance Travelled :"+ (System.Math.Round((distanceTravelled * 0.1f), 2)).ToString() + " Light Year";
        gameObject.SetActive(true);
    }
}
