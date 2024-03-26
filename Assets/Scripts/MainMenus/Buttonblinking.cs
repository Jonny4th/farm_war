using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttonblinking : MonoBehaviour
{
    [Range(0, 255)]
    [SerializeField] private float m_alp = 255;
    [SerializeField] private Image m_image;

    void Start()
    {
        m_image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        m_image.color = new Color(m_image.color.r, m_image.color.g, m_image.color.b, m_alp / 255f);
    }
}
