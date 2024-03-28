using System.Collections.Generic;
using UnityEngine;

public class TextSpawner : MonoBehaviour
{
    [SerializeField]
    SimpleNotification m_Blueprint;

    [SerializeField]
    Transform m_Container;

    [SerializeField]
    private List<SimpleNotification> m_Reserved = new();

    public void ShowText(string text)
    {
        var display = m_Reserved.Find(x => !x.gameObject.activeSelf);

        if(display == null)
        {
            display = Instantiate(m_Blueprint, m_Container);
            display.gameObject.SetActive(false);
            m_Reserved.Add(display);
        }

        display.SetText(text);
        display.transform.SetAsLastSibling();
        display.gameObject.SetActive(true);
    }

}
