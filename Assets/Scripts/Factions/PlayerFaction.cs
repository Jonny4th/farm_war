using System;
using UnityEngine;
using Random = UnityEngine.Random;
public class PlayerFaction : Faction<Raid>
{

    [SerializeField] private float maxCoin;
    public float MaxCoin { get { return maxCoin; } }
    private float m_coin;
    public float Coin { get { return m_coin; } }
    [SerializeField] private float startCoin = 1000;

    [SerializeField] private RaidController raidCon;
    public RaidController RaidCon { get { return raidCon; } }

    [SerializeField] private ShieldContoller shieldCon;
    public ShieldContoller ShieldContoller { get {  return shieldCon; } }

    [SerializeField] private RaidableHealingController healingCon;
    public RaidableHealingController HealingCon { get { return healingCon; } }
    
    [SerializeField] private Transform groundParent;

    [SerializeField] private float attackTime = 1;
    [SerializeField] private float damage = 1;
    private float timerAttack = 0;

    [SerializeField] private float timeSteal = 1;
    [SerializeField] private float quantityCoin = 10;
    private float timerSteal;


    private event Action<PlayerFaction> updateHp;
    public Action<PlayerFaction> UpdateHp { get { return updateHp; } set { updateHp = value; } }
    private event Action<PlayerFaction> updateCoin;
    public Action<PlayerFaction> UpdateCoin { get { return updateCoin; } set { updateCoin = value; } }

    private Action attackEvent;
    public Action AttackEvent { get { return attackEvent; } set { attackEvent = value; } }
    private Action poisonEvent;
    public Action PoisonEvent { get { return poisonEvent; } set { poisonEvent = value; } }
    private Action healingEvent;
    public Action HealingEvent { get { return healingEvent; } set { healingEvent = value; } }

    public bool UnitOnGround { get { return CheckUnitOnGround(); } }
    public override void TakeDamage(float damage)
    {
        if (GameManager.instance.State != GameState.Action) return;
        currentHp -= damage;
        // UIManager.instance.UpdateUi(this);
        updateHp?.Invoke(this);
        attackEvent?.Invoke();
        // Debug.Log("FFFF");
    }

    private bool CheckUnitOnGround()
    {
        // return raidCon.RaidList.Find(x => x.gameObject.activeSelf);
        return raidCon.RaidActive > 0;
    }

    protected override void Start()
    {

        currentHp = maxHp;
        m_coin = startCoin;
        GameManager.instance.ResetEven += ResetGame;
        // Delay(() => UIManager.instance.UpdateUi(this), 1f);
        Delay(() =>
        {
            updateHp?.Invoke(this);
            updateCoin?.Invoke(this);
        }, 0.2f);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            attackEvent?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            healingEvent?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            poisonEvent?.Invoke();
        }
    }
    private void FixedUpdate()
    {
        if (raidCon.RaidActive > 0)
        {
            AttackPlayer();
            Steal();

        }
        else if (raidCon.RaidActive == 0 && timerAttack > 0)
        {
            timerAttack = 0;
        }
    }
    private void ResetGame(GameManager gameManager)
    {
        timerAttack = 0;
        currentHp = maxHp;
        m_coin = startCoin;
        Delay(() =>
      {
          updateHp?.Invoke(this);
          updateCoin?.Invoke(this);
      }, 0.1f);

        raidCon.ClearAllRaidList();

        // currentHp = maxHp;
        // foreach (var T in aliveUnit)
        //     Destroy(T);
        // aliveUnit.Clear();
        // foreach (var T in unitInGrouind)
        //     Destroy(T);
        // unitInGrouind.Clear();
        // // Delay(() => UIManager.instance.UpdateUi(this), 1f);
        // lastTime = 0;
    }

    public void Health(float hp)
    {
        currentHp += hp;
        healingEvent?.Invoke();
        // UIManager.instance.UpdateUi(this);
    }


    private void AttackPlayer()
    {
        timerAttack += Time.fixedDeltaTime;
        if (timerAttack >= attackTime)
        {
            timerAttack = 0;
            GameManager.instance.EmemyFaction.TakeDamage(damage * raidCon.RaidActive);
        }

    }
    private void Steal()
    {
        timerSteal += Time.deltaTime;
        if (timerSteal >= timeSteal)
        {
            timerSteal = 0;
            AddCoin(quantityCoin * raidCon.RaidActive);
        }
    }

    public bool HaveCoin(float coin)
    {
        return m_coin >= coin;
    }

    public void AddCoin(float coin)
    {
        m_coin += coin;
        if (m_coin > maxCoin)
            m_coin = maxCoin;
        updateCoin?.Invoke(this);
    }

    public void ReduceCoin(float coin)
    {
        m_coin -= coin;
        updateCoin?.Invoke(this);
    }

    public void AttackCommand() // use by ui btn
    {
        if (!HaveCoin(raidCon.Cost)) return; // not enough coin.
        if (!raidCon.IsReady) return; // no fully grown veggies.
        ReduceCoin(raidCon.Cost);

        var raid = raidCon.RandomSpawnOnGround();
        aliveUnit.Add(raid);
        raid.OnRaidCompleted.AddListener((r) => aliveUnit.Remove(r));
    }

    public void ShieldCommand() // used by ui button
    {
        if (!HaveCoin(shieldCon.Cost)) return;

        ReduceCoin(shieldCon.Cost);
        int hitPoint = 2; // default hit point for shield.

        var unitNodes = GameManager.instance.NodeMana.nodeCollcetion.FindAll(x => x.IsRaided && !x.IsShielded);

        if (unitNodes.Count == 0)
        {
            shieldCon.ActivateShieldRandomly(hitPoint);
            return;
        }

        unitNodes[Random.Range(0, unitNodes.Count)].ActivateShield(hitPoint);
    }

    public void HealCommand()
    {
        if (!HaveCoin(shieldCon.Cost)) return;
        if (!healingCon.IsReady) return;

        ReduceCoin(healingCon.Cost);

        healingCon.HealRandomly();
    }

    public void AnimalDie(AnimalTest animalTest)
    {
    }

    private void OnDestroy()
    {
        GameManager.instance.ResetEven -= ResetGame;
    }

}
