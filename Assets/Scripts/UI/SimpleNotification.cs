using System.Collections;
using TMPro;
using UnityEngine;

public class SimpleNotification : MonoBehaviour
{
    [SerializeField]
    private TMP_Text m_Display;

    [SerializeField]
    private float m_LifeTime;

    [SerializeField]
    Animator animator;

    private void OnEnable()
    {
        StartCoroutine(Countdown());
    }

    public void SetText(string text)
    {
        m_Display.text = text;
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(m_LifeTime);

        animator.SetTrigger("End");
    }

    public void OnAnimationOutDone() // Used by animation event
    {
        gameObject.SetActive(false);
    }
}
