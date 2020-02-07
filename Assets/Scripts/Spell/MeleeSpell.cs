using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSpell : Spell
{
    private Transform target;
    private CharacterAttack owner;
    private CharacterNavMovement movement;
    private readonly int hashAttackPara = Animator.StringToHash("Attack");
    private float attackRadius = 3f;

    private void Update()
    {
        if (target)
        {
            Vector3 attckTarget = target.transform.position - owner.transform.position;
            if (attckTarget.magnitude < attackRadius)
            {
                owner.animator.SetBool(hashAttackPara, true);
                owner.transform.LookAt(target.transform);
            }
            else
            {
                owner.animator.SetBool(hashAttackPara, false);
            }
        }
    }

    public override void DoSpell(CharacterAttack _owner, Transform newTarget)
    {
        owner = _owner;
        movement = owner.GetComponent<CharacterNavMovement>();
        target = newTarget;
    }

}
