using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAttack : MonoBehaviour
{
    public List<Spell> arsenal = new List<Spell>();
    public bool isAttack = false;
    public Animator animator;
    private readonly int hashAttackPara = Animator.StringToHash("Attack");
    private Health target;
    private float rotSpeed = 5f;
    private ISpell selectedSpell;
    private void Awake()
    {
        if (arsenal.Count > 0) selectedSpell = arsenal[0];
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject != gameObject)
    //    {
    //        var enemy = other.GetComponent<CharacterManager>();
    //        if (enemy != null)
    //        {
    //            Debug.Log(name + "   " + enemy.name);
    //            Attack();
    //            target = enemy.health;
    //            return;
    //        }

    //        Stop();
    //    }
    //    target = null;
    //}

    public void Stop()
    {
        target = null;
        isAttack = false;
        animator.SetBool(hashAttackPara, isAttack);
    }

    public void Attack(Health newTarget)
    {
        target = newTarget;
        isAttack = true;
        selectedSpell.DoSpell(this, newTarget.transform);
    }

    public void SelectSpell(ISpell select)
    {
        if(select != null &&  arsenal.Exists(x => x.Equals(select))) selectedSpell = select;
    }

    public void EventAttack()
    {
        if (target)
        {
            target.TakeDamage(10f);
            Debug.Log("EventAttack");
        }
        else
        {
            Debug.Log("else EventAttack");
        }
    }

    public void SetTarget(Health newTarget)
    {
        target = newTarget;
    }

    private void LookAtTarget()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.LookRotation(target.transform.position - transform.position), rotSpeed * Time.deltaTime);
    }

    private void Attacking(Health _target)
    {
        animator.SetBool(hashAttackPara, isAttack);
        transform.LookAt(target.transform);
        if(_target)  selectedSpell.DoSpell(this, _target.transform);
    }


}
