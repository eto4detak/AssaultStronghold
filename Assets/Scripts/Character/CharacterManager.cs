using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public Health health;
    public CharacterNavMovement movement;
    public CharacterAttack attack;
    public Mission mission;
    public Characteristic characteristic;

    private void Update()
    {
        UpdateCharacteristic();
    }

    private void UpdateCharacteristic()
    {
        movement.SetSpeed(characteristic.Speed);
    }

    public void Move(Transform target)
    {
        attack.Stop();
        movement.SetTargetMovement(target);
    }
    public void Move(Vector3 target)
    {
        attack.Stop();
        movement.MoveToPoint(target);
    }


}
