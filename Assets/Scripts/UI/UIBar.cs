using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEditor;

public class UIBar : MonoBehaviour
{
    // [System.Serializable]
    // private class BarGroup
    // {
    //     public TextMeshProUGUI hpText;
    //     public Image hpBar;
    //     public float curr = 0;
    // }
    // public static UIBar instance;
    // [Space]
    // [Header("ADD UI")]
    // [SerializeField] private TextMeshProUGUI point;
    // [SerializeField] private BarGroup playerBar;
    // [SerializeField] private BarGroup ememyBar;

    // private IEnumerator playerEmu;
    // private IEnumerator ememyEmu;

    // private float oldPlayerHp;
    // private float oldEmemyHp;

    // void Awake()
    // {
    //     if (instance != null && instance != this)
    //         Destroy(this.gameObject);
    //     else
    //         instance = this;
    // }
    // void Start()
    // {
    //     // oldPlayerHp = GameManager.instance.PlayerFaction.CurrentHP();
    //     // oldEmemyHp = GameManager.instance.EmemyFaction.CurrentHP();



    //     GameManager.instance.playerHealthUpdate += UpdatePlayerUI;
    //     GameManager.instance.ememyHealthUpdate += UpdateEmemyUI;
    //     GameManager.instance.pointUpdate += UpdatePointUI;
    // }
    // void Update()
    // {

    // }
    // private void UpdatePlayerUI(float hp)
    // {
    //     Debug.Log("::::::::");
    //     if (playerEmu != null)
    //     {
    //         StopCoroutine(playerEmu);
    //         playerEmu = HpBerAni(playerBar, playerBar.curr, hp);
    //     }
    //     else
    //         playerEmu = HpBerAni(playerBar, oldPlayerHp, hp);
    //     StartCoroutine(playerEmu);

    //     oldPlayerHp = hp;
    // }

    // public void UpdateEmemyUI(float hp)
    // {
    //     if (ememyEmu != null)
    //     {
    //         StopCoroutine(ememyEmu);
    //         ememyEmu = HpBerAni(ememyBar, ememyBar.curr, hp);
    //     }
    //     else
    //         ememyEmu = HpBerAni(ememyBar, oldEmemyHp, hp);
    //     StartCoroutine(ememyEmu);

    //     oldEmemyHp = hp;
    // }
    // public void UpdatePointUI()
    // {
    //     point.text = GameManager.instance.CurrentPoint.ToString();
    // }

    // IEnumerator HpBerAni(BarGroup barGroup, float curr, float target)
    // {
    //     float persent = 0;
    //     float speed = 300f;
    //     float time = Mathf.Abs(target - curr) / speed;
    //     float ontime = 0;

    //     while (persent < 1)
    //     {
    //         ontime += Time.deltaTime;
    //         persent = ontime / time;
    //         barGroup.curr = Mathf.Lerp(curr, target, persent);
    //         var hp = (int)barGroup.curr;
    //         barGroup.hpText.text = $"{hp.ToString()}/{target.ToString()}";

    //         yield return new WaitForSeconds(Time.deltaTime);
    //     }
    // }
    // void OnDestroy()
    // {
    //     GameManager.instance.playerHealthUpdate -= UpdatePlayerUI;
    //     GameManager.instance.ememyHealthUpdate -= UpdateEmemyUI;
    //     GameManager.instance.pointUpdate -= UpdatePointUI;
    // }
}
