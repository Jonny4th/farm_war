using System;
using UnityEngine;
public class EmemyFaction : Faction<Farmer>
{
    private event Action<EmemyFaction> updateHp;
    public Action<EmemyFaction> UpdateHp { get { return updateHp; } set { updateHp = value; } }
    [SerializeField] private ToxicController m_toxicController;
    public override void TakeDamage(float damage)
    {
        if (GameManager.instance.State != GameState.Action) return;
        currentHp -= damage;
        // UIManager.instance.UpdateUi(this);
        // Debug.Log("Hit");
        updateHp?.Invoke(this);
    }

    protected override void Start()
    {

        SetMaxHP();
        GameManager.instance.ResetEven += ResetGame;

        // Delay(() => UIManager.instance.UpdateUi(this), 1f);
        Delay(() => updateHp?.Invoke(this), 1f);
    }

    private void SetMaxHP()
    {

        if (aliveUnit.Count != 0)
        {
            maxHp = 0;
            foreach (var T in aliveUnit)
            {
                maxHp += T.MaxHp;
            }
        }
        currentHp = maxHp;
    }

    private void ResetGame(GameManager gameManager)
    {
        SetMaxHP();
        updateHp?.Invoke(this);
        // Delay(() => UIManager.instance.UpdateUi(this), 1f);
    }

    private void OnDestroy()
    {
        GameManager.instance.ResetEven -= ResetGame;
    }
}
