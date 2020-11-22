using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITextFlicker : MonoBehaviour
{
    [SerializeField]
    private float m_flickerTime;

    private float m_flickerStartTime;
    private Text m_text;
    private Color m_color;

    private void Awake()
    {
        m_text = GetComponent<Text>();
        m_color = m_text.color;
    }

    private void Update()
    {
        float interpolation = Mathf.PingPong ((Time.time - m_flickerStartTime) / m_flickerTime, 1.0f);
        float alpha = Mathf.SmoothStep(0.0f, m_color.a, interpolation);

        m_text.color = new Color(m_color.r, m_color.g, m_color.b, alpha);
    }

    private void OnEnable()
    {
        m_flickerStartTime = Time.time;
    }

    private void OnDisable()
    {
        m_text.color = m_color;
    }
}
