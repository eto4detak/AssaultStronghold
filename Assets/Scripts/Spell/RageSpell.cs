using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageSpell : Spell
{
    public Feature imposed;

    private Transform target;
    private CharacterAttack owner;
    private bool fired;
    private readonly int hashAttackPara = Animator.StringToHash("Attack");

    private Rigidbody shell;
    private float currentLaunchForce = 10f;
    private float rechargeTime = 1;
    private float currentRechargeTime = 0f;

    public override void DoSpell(CharacterAttack _owner, Transform newTarget)
    {
        owner = _owner;
        target = newTarget;
        if (!target) return;
        owner.animator.SetBool(hashAttackPara, true);
        owner.transform.LookAt(target.transform);
        Fire();
    }

    private void Fire()
    {
        fired = true;
        CharacterManager ch_target = target.GetComponent<CharacterManager>();
        if (target != null && !ch_target.characteristic.ExistFeature(imposed.data.featureName))
        {
            var feature = Instantiate(imposed, transform.position, transform.rotation);
            ch_target.characteristic.AddFeature(feature);
        }
    }

}