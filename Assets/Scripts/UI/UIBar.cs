using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEditor;

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
                StartCoroutine(HpBerAni(currentPlayerHp, GameManager.instance.PlayerHp, mphp));
                currentPlayerHp = GameManager.instance.PlayerHp;
            }
            if (GameManager.instance.PlayerHp > currentPlayerHp)
            {
                StartCoroutine(HpBerAni(currentPlayerHp, GameManager.instance.PlayerHp, mphp));
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
    IEnumerator HpBerAni(float curr, float target, float maxhp)
    {
        float persent = 0;
        float speed = 100f;
        float time = Mathf.Abs(target - curr) / speed;
        float ontime = 0;

        while (persent < 1)
        {
            ontime += Time.deltaTime;
            persent = ontime / time;
            var g = Mathf.Lerp(curr, target, persent);
            ShowUi((int)Mathf.Floor(g), target);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
