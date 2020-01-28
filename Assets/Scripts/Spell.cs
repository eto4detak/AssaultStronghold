using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ISpell
{
    void DoSpell(CharacterAttack owner , Transform target);
    FeatureData GetData();
}
public class Spell : MonoBehaviour, ISpell
{
    public FeatureData data;
    public void DoSpell(CharacterAttack owner, Transform target)
    {
    }

    public FeatureData GetData()
    {
        return data;
    }
}

public interface IPoint
{
    Vector3 GetPoint();
}
public interface IParticles
{
    Vector3 StartParticle();
}

public class StormSpell : Spell, IPoint
{
    public Vector3 GetPoint()
    {
        return Vector3.back;
    }
}



public class IceArrow : Spell
{

    private Transform target;
    private CharacterAttack owner;
    private bool fired;
    private readonly int hashAttackPara = Animator.StringToHash("Attack");

    private Rigidbody shell;
    private float currentLaunchForce = 10f;
    private float rechargeTime = 1;
    private float currentRechargeTime = 0f;

    public void DoSpell(CharacterAttack _owner, Transform newTarget)
    {
        owner = _owner;
        target = newTarget;
        if (!target) return;
        owner.animator.SetBool(hashAttackPara, true);
        owner.transform.LookAt(target.transform);

        if (currentRechargeTime > rechargeTime)
            Fire();
        else
            currentRechargeTime += Time.deltaTime;
    }

    private void Fire()
    {
        fired = true;
        Rigidbody shellInstance =
            GameObject.Instantiate(shell, owner.transform.position, owner.transform.rotation) as Rigidbody;
        shellInstance.velocity = currentLaunchForce * owner.transform.forward;
    }

    public Vector3 GetPoint()
    {
        return Vector3.back;
    }

}