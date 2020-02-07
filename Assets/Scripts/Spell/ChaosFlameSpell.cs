using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaosFlameSpell : Spell
{
    public PhysicsSpell phSpellPrefab;

    private Transform target;
    private CharacterAttack owner;
    private readonly int hashAttack = Animator.StringToHash("Attack");

    public override void DoSpell(CharacterAttack _owner, Transform newTarget)
    {
        owner = _owner;
        target = newTarget;
        if (!target) return;
        owner.animator.SetTrigger(hashAttack);
        owner.transform.LookAt(target.transform);

        Instantiate(phSpellPrefab, target.transform.position, target.transform.rotation);
    }

}
