using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEditor;
using System.Xml.Schema;

public class UIBar : MonoBehaviour
{
    [Space]
    [Header("ADD UI")]
    [SerializeField] private TextMeshProUGUI playerHpText;
    [SerializeField] private Image playerHpBar;
    [SerializeField] private TextMeshProUGUI ememyHpText;
    [SerializeField] private Image ememyHpBar;
    [SerializeField] private TextMeshProUGUI point;

    private float currentPlayerHp;
    private float maxPlayerHp;
    private float currentEmemyHp;
    private float maxEmemyHp;


    void Start()
    {
        currentPlayerHp = GameManager.instance.PlayerHp;
        currentEmemyHp = GameManager.instance.EmenyHp;
        maxPlayerHp = GameManager.instance.MaxPlayerHp;
        maxEmemyHp = GameManager.instance.MaxEmemyHp;
    }

    void FixedUpdate()
    {
        if (GameManager.instance == null) return;
        UpdatePlayerUI();
        UpdateEmemyUI();
        UpdatePoint();
    }

    private void UpdatePlayerUI()
    {
        float php = GameManager.instance.PlayerHp;
        float mphp = maxPlayerHp;


        if (currentPlayerHp != GameManager.instance.PlayerHp)
        {

            if (GameManager.instance.PlayerHp < currentPlayerHp)
            {
                StartCoroutine(HpBerAni(currentPlayerHp, GameManager.instance.PlayerHp, true));
                currentPlayerHp = GameManager.instance.PlayerHp;
                maxPlayerHp = GameManager.instance.MaxPlayerHp;
            }
            if (GameManager.instance.PlayerHp > currentPlayerHp)
            {
                StartCoroutine(HpBerAni(currentPlayerHp, GameManager.instance.PlayerHp, false));
                currentPlayerHp = GameManager.instance.PlayerHp;
                php = GameManager.instance.PlayerHp;
                maxPlayerHp = GameManager.instance.MaxPlayerHp;
                mphp = maxPlayerHp;
            }

        }
    }
    private void ShowUi(float php, float mphp)
    {
        playerHpText.text = $"{php} / {mphp}";
        playerHpBar.fillAmount = php == 0 && mphp == 0 ? 0 : php / mphp;
    }
    public void UpdateEmemyUI()
    {
        var eh = GameManager.instance.EmenyHp;
        var meh = maxEmemyHp;

        if (currentEmemyHp != GameManager.instance.EmenyHp)
        {
            if (GameManager.instance.EmenyHp < currentEmemyHp)
            {
                currentEmemyHp = GameManager.instance.EmenyHp;
            }
            if (GameManager.instance.EmenyHp > currentEmemyHp)
            {
                currentEmemyHp = GameManager.instance.EmenyHp;
                eh = GameManager.instance.EmenyHp;
                maxEmemyHp = GameManager.instance.MaxEmemyHp;
                meh = maxEmemyHp;
            }
        }

        ememyHpText.text = $"{eh} / {meh}";
        ememyHpBar.fillAmount = eh == 0 && meh == 0 ? 0 : eh / meh;
    }
    public void UpdatePoint()
    {
        point.text = GameManager.instance.CurrentPoint.ToString();
    }
    IEnumerator HpBerAni(float curr, float target, bool u)
    {
        Debug.Log(curr);
        Debug.Log(target);

        float persent = 0;
        float speed = 100f;
        float time = Mathf.Abs(target - curr) / speed;
        float ontime = 0;

        while (persent < 1)
        {
            ontime += Time.deltaTime;
            persent = ontime / time;
            var g = Mathf.Lerp(curr, target, persent);

            var php = (int)Mathf.Floor(g);

            if (u)
            {
                playerHpText.text = $"{(int)Mathf.Floor(g)} / {target}";
                if (persent >= 1)
                    playerHpBar.fillAmount = php == 0 && curr == 0 ? 0 : php / target;
                else
                    playerHpBar.fillAmount = php == 0 && curr == 0 ? 0 : php / curr;
            }
            else
            {
                playerHpText.text = $"{(int)Mathf.Floor(g)} / {target}";
                playerHpBar.fillAmount = php == 0 && target == 0 ? 0 : php / target;
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
