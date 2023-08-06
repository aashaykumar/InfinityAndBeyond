using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class animate : MonoBehaviour
{
    TextMeshProUGUI m_TextMeshProUGUI;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        m_TextMeshProUGUI = GetComponent<TextMeshProUGUI>();
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= 2f)
        {
            m_TextMeshProUGUI.colorGradient = new VertexGradient(Random.ColorHSV(), Random.ColorHSV(), Random.ColorHSV(), Random.ColorHSV());
            timer = 0f;
        }

        else
        {
            timer += Time.deltaTime;
        }
    }
}
