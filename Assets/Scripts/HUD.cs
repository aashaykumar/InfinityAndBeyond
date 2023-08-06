using UnityEngine.UI;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public Text DistanceLabel, VelocityLabel;

    public void SetValues(float distanceTravelled, float velocity)
    {
        DistanceLabel.text = ((int)(distanceTravelled * 10f)).ToString();
        VelocityLabel.text = ((int)(velocity * 10f)).ToString();
    }
}
