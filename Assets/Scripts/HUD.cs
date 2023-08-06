using UnityEngine.UI;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public Text DistanceLabel;

    public void SetValues(float distanceTravelled)
    {
        DistanceLabel.text = (System.Math.Round((distanceTravelled * 0.1f), 2)).ToString() + " Light Year";
    }
}
