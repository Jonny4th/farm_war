using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;



public class PlayerFaction : Faction<AnimalTest>
{

    [SerializeField] private float maxCoin;
    public float MaxCoin { get { return maxCoin; } }
    private float coin;
    public float Coin { get { return coin; } }
    [SerializeField] private float startCoin = 1000;

    // [SerializeField] private List<AnimalTest> unitInGrouind = new List<AnimalTest>();
    // public List<AnimalTest> UnitInGrouind { get { return unitInGrouind; } }

    [SerializeField] private RaidController raidCon;
    public RaidController RaidCon { get { return raidCon; } }
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
    private Action attackEven;
    public Action AttackEven { get { return attackEven; } set { attackEven = value; } }
    private Action healingEven;
    public Action HealingEven { get { return healingEven; } set { healingEven = value; } }

    public bool UnitOnGround { get { return CheckUnitOnGround(); } }
    public override void TakeDamage(float damage)
    {
        if (GameManager.instance.State != GameState.Action) return;
        currentHp -= damage;
        // UIManager.instance.UpdateUi(this);
        updateHp?.Invoke(this);
        attackEven?.Invoke();
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
        coin = startCoin;
        GameManager.instance.ResetEven += ResetGame;
        // Delay(() => UIManager.instance.UpdateUi(this), 1f);
        Delay(() =>
        {
            updateHp?.Invoke(this);
            updateCoin?.Invoke(this);
        }, 0.2f);
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
        coin = startCoin;
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
        if (currentHp + hp > maxHp)
            currentHp = maxHp;
        else
            currentHp += hp;
        healingEven?.Invoke();
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
        if (this.coin - coin <= 0)
            return false;
        else
            return true;
    }
    public void AddCoin(float coin)
    {
        this.coin += coin;
        if (this.coin > maxCoin)
            this.coin = maxCoin;
        updateCoin?.Invoke(this);
    }
    public void ReducCoin(float coin)
    {
        this.coin -= coin;
        updateCoin?.Invoke(this);
    }


    // private void Update()
    // {
    //     if (CheckUnitOnGround())
    //     {

    //     }








    // if (UnitInGrouind.Count > 0)
    // {
    //     if (Time.time - lastTime > attackTime)
    //     {
    //         lastTime = Time.time;
    //         GameManager.instance.EmemyFaction.TakeDamage(damage * UnitInGrouind.Count);
    //     }
    // }

    // if (Input.GetKeyDown(KeyCode.Q))
    // {
    //     GameObject Obj = new GameObject("Animal", typeof(AnimalTest));
    //     aliveUnit.Add(Obj.GetComponent<AnimalTest>());
    //     Obj.transform.parent = UnitParent;
    // }
    // if (Input.GetKeyDown(KeyCode.W))
    // {
    //     foreach (var T in aliveUnit.ToArray())
    //     {
    //         SentUnitOnGround();
    //     }
    // }
    // }

    public void SentUnitOnGround()
    {
        // if (aliveUnit.Count <= 0) return;
        // Node node = RandomNode();
        // AnimalTest animalTest = aliveUnit[0];
        // unitInGrouind.Add(animalTest);
        // aliveUnit.Remove(animalTest);

        // animalTest.transform.parent = groundParent;

        // animalTest.NodeTarget = node;
        // node.Animas.Add(animalTest);
    }


    public void AttackCommand(float coin) // use by ui btn
    {
        if (!HaveCoin(coin)) return;

        // raidCon.RandomSpawnOnGround();
        if (raidCon.RandomSpawnOnGround())
            ReducCoin(coin);
    }
























    public void AnimalDie(AnimalTest animalTest)
    {
        // unitInGrouind.Remove(animalTest);
        // animalTest.NodeTarget.RemoveAnimal(animalTest);
    }

    public Node RandomNode()
    {
        return GameManager.instance.NodeMana.nodeCollcetion[UnityEngine.Random.Range(0, GameManager.instance.NodeMana)];
    }
    private void OnDestroy()
    {
        GameManager.instance.ResetEven -= ResetGame;
    }

}
