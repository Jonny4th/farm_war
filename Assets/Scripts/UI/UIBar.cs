using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEditor;

public class UIBar : MonoBehaviour
{
    [System.Serializable]
    private class BarGroup
    {
        public TextMeshProUGUI hpText;
        public Image hpBar;
    }

    [Space]
    [Header("ADD UI")]
    [SerializeField] private TextMeshProUGUI point;

    [SerializeField] private BarGroup playerBar;
    [SerializeField] private BarGroup ememyBar;

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

        GameManager.instance.playerHealthUpdate += UpdatePlayerUI;
        GameManager.instance.ememyHealthUpdate += UpdateEmemyUI;
        GameManager.instance.pointUpdate += UpdatePointUI;
    }

    void FixedUpdate()
    {
        // if (GameManager.instance == null) return;
        // UpdatePlayerUI();
        // UpdateEmemyUI();
        // UpdatePointUI();
    }

    private void UpdatePlayerUI()
    {
        float php = GameManager.instance.PlayerHp;
        float mphp = maxPlayerHp;

        if (currentPlayerHp != GameManager.instance.PlayerHp)
        {
            if (GameManager.instance.PlayerHp < currentPlayerHp)
            {
                StartCoroutine(HpBerAni(playerBar, currentPlayerHp, GameManager.instance.PlayerHp, true));
                currentPlayerHp = GameManager.instance.PlayerHp;
                maxPlayerHp = GameManager.instance.MaxPlayerHp;
            }
            if (GameManager.instance.PlayerHp > currentPlayerHp)
            {
                StartCoroutine(HpBerAni(playerBar, currentPlayerHp, GameManager.instance.PlayerHp, false));
                currentPlayerHp = GameManager.instance.PlayerHp;
                php = GameManager.instance.PlayerHp;
                maxPlayerHp = GameManager.instance.MaxPlayerHp;
                mphp = maxPlayerHp;
            }
        }
    }

    public void UpdateEmemyUI()
    {
        var eh = GameManager.instance.EmenyHp;
        var meh = maxEmemyHp;

        if (currentEmemyHp != GameManager.instance.EmenyHp)
        {
            if (GameManager.instance.EmenyHp < currentEmemyHp)
            {
                StartCoroutine(HpBerAni(ememyBar, currentEmemyHp, GameManager.instance.EmenyHp, true));
                currentEmemyHp = GameManager.instance.EmenyHp;
                maxEmemyHp = GameManager.instance.EmenyHp;
            }
            if (GameManager.instance.EmenyHp > currentEmemyHp)
            {
                StartCoroutine(HpBerAni(ememyBar, currentEmemyHp, GameManager.instance.EmenyHp, false));
                currentEmemyHp = GameManager.instance.EmenyHp;
                eh = GameManager.instance.EmenyHp;
                maxEmemyHp = GameManager.instance.MaxEmemyHp;
                meh = maxEmemyHp;
            }
        }
    }
    public void UpdatePointUI()
    {
        point.text = GameManager.instance.CurrentPoint.ToString();
    }

    IEnumerator HpBerAni(BarGroup barGroup, float curr, float target, bool u)
    {
        float persent = 0;
        float speed = 300f;
        float time = Mathf.Abs(target - curr) / speed;
        float ontime = 0;

        while (persent < 1)
        {
            ontime += Time.deltaTime;
            persent = ontime / time;
            var g = Mathf.Lerp(curr, target, persent);

            var hp = (int)Mathf.Floor(g);

            if (u)
            {
                barGroup.hpText.text = $"{hp} / {target}";
                if (persent >= 1)
                    barGroup.hpBar.fillAmount = hp == 0 && curr == 0 ? 0 : hp / target;
                else
                    barGroup.hpBar.fillAmount = hp == 0 && curr == 0 ? 0 : hp / curr;
            }
            else
            {
                barGroup.hpText.text = $"{(int)Mathf.Floor(g)} / {target}";
                barGroup.hpBar.fillAmount = hp == 0 && target == 0 ? 0 : hp / target;
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
    void OnDestroy()
    {
        GameManager.instance.playerHealthUpdate -= UpdatePlayerUI;
        GameManager.instance.ememyHealthUpdate -= UpdateEmemyUI;
        GameManager.instance.pointUpdate -= UpdatePointUI;
    }
}
