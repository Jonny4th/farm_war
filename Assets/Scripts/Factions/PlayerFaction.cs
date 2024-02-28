using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;



public class PlayerFaction : Faction<AnimalTest>
{

    [SerializeField] private float point;
    public float Point { get { return point; } }

    [SerializeField] private List<AnimalTest> unitInGrouind = new List<AnimalTest>();
    public List<AnimalTest> UnitInGrouind { get { return unitInGrouind; } }
    [SerializeField] private Transform groundParent;
    [SerializeField] private float damage = 1;
    private event Action<PlayerFaction> updateHp;
    public Action<PlayerFaction> UpdateHp { get { return updateHp; } set { updateHp = value; } }
    public override void TakeDamage(float damage)
    {
        currentHp -= damage;
        // UIManager.instance.UpdateUi(this);
        updateHp?.Invoke(this);
    }

    protected override void Start()
    {

        currentHp = maxHp;
        GameManager.instance.ResetEven += Reset;
        // Delay(() => UIManager.instance.UpdateUi(this), 1f);
        Delay(() => updateHp?.Invoke(this), 1f);
    }

    private void Reset()
    {
        currentHp = maxHp;
        foreach (var T in aliveUnit)
            Destroy(T);
        aliveUnit.Clear();
        foreach (var T in unitInGrouind)
            Destroy(T);
        unitInGrouind.Clear();
        // Delay(() => UIManager.instance.UpdateUi(this), 1f);
        lastTime = 0;
    }

    public void Health(float hp)
    {
        currentHp += hp;
        // UIManager.instance.UpdateUi(this);
    }
    [SerializeField] private float attackTime = 0.5f;
    private float lastTime = 0;
    private void Update()
    {
        if (UnitInGrouind.Count > 0)
        {
            if (Time.time - lastTime > attackTime)
            {
                lastTime = Time.time;
                GameManager.instance.EmemyFaction.TakeDamage(damage * UnitInGrouind.Count);
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameObject Obj = new GameObject("Animal", typeof(AnimalTest));
            aliveUnit.Add(Obj.GetComponent<AnimalTest>());
            Obj.transform.parent = UnitParent;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            foreach (var T in aliveUnit.ToArray())
            {
                SentUnitOnGround();
            }
        }
    }

    public void SentUnitOnGround()
    {
        if (aliveUnit.Count <= 0) return;
        Node node = RandomNode();
        AnimalTest animalTest = aliveUnit[0];
        unitInGrouind.Add(animalTest);
        aliveUnit.Remove(animalTest);

        animalTest.transform.parent = groundParent;

        animalTest.NodeTarget = node;
        node.Animas.Add(animalTest);
    }

    public void AnimalDie(AnimalTest animalTest)
    {
        unitInGrouind.Remove(animalTest);
        animalTest.NodeTarget.RemoveAnimal(animalTest);
    }

    public Node RandomNode()
    {
        return GameManager.instance.NodeMana.nodeCollcetion[UnityEngine.Random.Range(0, GameManager.instance.NodeMana)];
    }
    private void OnDestroy()
    {
        GameManager.instance.ResetEven -= Reset;
    }

}
