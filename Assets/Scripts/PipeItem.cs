using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeItem : MonoBehaviour
{
    private Transform rotator;
    private Transform item;

    private void Awake()
    {
        rotator = transform.GetChild(0);
        item = rotator.GetChild(0);
    }

    public void Position(Pipe pipe, float curveRotation, float ringRotation)
    {
        transform.SetParent(pipe.transform, false);
        transform.localRotation = Quaternion.Euler(0f, 0f, -curveRotation);
        item.localScale = new Vector3(Random.Range(0.05f, 0.1f), Random.Range(0.05f, 0.1f), Random.Range(0.05f, 0.1f));
        rotator.localPosition = new Vector3(Random.Range(-1f, 1f), (pipe.CurveRadius + Random.Range(-1f,1f)));
        rotator.localRotation = Quaternion.Euler(ringRotation, 0f, 0f);
    }

    private void FixedUpdate()
    {
        if (item != null)
        {
            rotator.Rotate(new Vector3(0f,50f * Time.fixedDeltaTime,0f), Space.Self);
        }
    }
}