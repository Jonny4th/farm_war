using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public abstract class StateBase : MonoBehaviour
{
    protected Animator animator;
    protected GameManager manager;
    protected Farmer farmer;
    [SerializeField] protected FarmerStrate stateName;

    [SerializeField]
    [Tooltip("Time To change State")]
    protected float time = 2f;
    protected float timer = 0;
    public float Timer { get { return timer; } }
    [SerializeField] protected float lookAtSpeed = 0.8f;


    protected StateManager swichState;
    protected NavMeshAgent agent;
    protected IEnumerator ieRotate;
    public void Init(Farmer farmer, Animator animator, GameManager gameManager)
    {
        this.farmer = farmer;
        this.animator = animator;
        this.manager = gameManager;
        swichState = farmer.StateManager;
        agent = farmer.Agent;
    }

    public virtual void StartState()
    {
        farmer.currentState = stateName;
        Debug.Log(stateName);
    }
    public virtual void EndState() { }
    public virtual void LogiUpdate() { }
    public virtual void PhysiUpdate() { }

    protected virtual void FormOtherColl() { }
    protected virtual private void OnTriggerEnter(Collider other) { }

    protected bool CheckUnitOnGround()
    {
        if (manager.PlayerFaction.UnitInGrouind.Count > 0)
            return true;
        else
            return false;
    }


    protected void CountToSwicthState(float t, Action callback)
    {
        timer += Time.deltaTime;
        if (timer >= t)
        {
            timer = 0;
            // farmer.targetMoving = RandomNode();
            // manager.NodeMana.SetIndexToMove(farmer, nodeIndex);
            // farmer.StateManager.SwitchState(farmer.moveState);
            // farmer.Agent.SetDestination(RandomNode());
            callback?.Invoke();
        }
    }

    protected void LookAt(Quaternion start, Quaternion targt, float ratateTime, Action callback)
    {
        if (ieRotate != null)
            StopCoroutine(ieRotate);
        ieRotate = RotateTO(start, targt, ratateTime, callback);
        StartCoroutine(ieRotate);
    }
    protected void LookAt(Quaternion start, Quaternion targt, float ratateTime)
    {
        if (ieRotate != null)
            StopCoroutine(ieRotate);
        ieRotate = RotateTO(start, targt, ratateTime, () => { });
        StartCoroutine(ieRotate);
    }

    protected Quaternion RotaAngle(Vector3 pos)
    {
        Vector3 dir = (pos - farmer.transform.position).normalized;
        float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        return Quaternion.Euler(0f, angle, 0f);
    }
    protected IEnumerator RotateTO(Quaternion start, Quaternion target, float duration, Action callback)
    {
        float timer = 0;
        while (timer < duration)
        {

            timer += Time.deltaTime;
            farmer.transform.rotation = Quaternion.Lerp(start, target, timer / duration);
            yield return null;
        }
        farmer.transform.rotation = target;
        callback?.Invoke();
    }
}
