using System.Collections;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class UIPanel
{
    public TextMeshProUGUI hpText;
    public Image hpBar;
    public Image backgroundBar;
    public float curr = 0;
    public float maxHp = 0;
    public string HpString { get { return $"{(int)curr}/{(int)maxHp}"; } }
    public float persentHp { get { return curr / maxHp; } }
    public IEnumerator em;
}
