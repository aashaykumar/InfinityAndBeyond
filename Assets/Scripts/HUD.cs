using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    public Text DistanceLabel;

    public void SetValues(float distanceTravelled)
    {
        DistanceLabel.text = (System.Math.Round((distanceTravelled * 0.1f), 2)).ToString();
    }

    public void pauseGame()
    {
        Time.timeScale = 0f;
    }

    public void resumeGame()
    {
        Time.timeScale = 1f;
    }
}
