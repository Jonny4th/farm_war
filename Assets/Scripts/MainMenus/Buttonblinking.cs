using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Buttonblinking : MonoBehaviour
{

    [SerializeField] private float m_maxAlpha = 255f;
    [SerializeField] private float m_minAlpha = 180f;
    private float m_alpha = 0;
    [SerializeField] private float timeBl = 1f;
    [SerializeField] private float m_timedelay = 0.5f;
    private float m_time => timeBl / 2f;
    private float m_timer = 0;
    [SerializeField] private Image m_image;
    private bool m_up = false;
    private bool m_isDelay = false;
    private float m_old;
    private float m_target;
    void Start()
    {
        if (m_image == null)
            m_image = GetComponent<Image>();
        SwicthTarget();
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_isDelay)
            Lerp();
        else
            Delay();
    }
    private void Lerp()
    {
        m_timer += Time.deltaTime;
        float p = m_timer / m_time;
        SetImageAlpha(Mathf.Lerp(m_old, m_target, p));
        if (p >= 1)
        {
            SwicthTarget();
            m_timer = 0;
            m_isDelay = true;
        }
    }
    private void Delay()
    {
        m_timer += Time.deltaTime;
        if (m_timer >= m_timedelay)
        {
            m_timer = 0;
            m_isDelay = false;
        }
    }
    private void SwicthTarget()
    {
        m_up = !m_up;
        if (m_up)
        {
            m_old = m_minAlpha;
            m_target = m_maxAlpha;
        }
        else
        {
            m_old = m_maxAlpha;
            m_target = m_minAlpha;
        }

    }
    private void SetImageAlpha(float ca)
    {
        m_image.color = new Color(m_image.color.r, m_image.color.g, m_image.color.b, ca / 255f);
    }
}
