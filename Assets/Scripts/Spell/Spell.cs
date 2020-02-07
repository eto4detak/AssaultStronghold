using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ISpell
{
    void DoSpell(CharacterAttack owner, Transform target);
    FeatureData GetData();
}
public class Spell : MonoBehaviour, ISpell
{
    public FeatureData data;
    public virtual void DoSpell(CharacterAttack owner, Transform target)
    {
    }

    public FeatureData GetData()
    {
        return data;
    }

    public virtual bool IsMayApply(Transform owner, Transform target)
    {
        if ( (target.transform.position - owner.transform.position).magnitude < 5f )
        {
            return true;
        }
        return false;
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



