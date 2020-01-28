using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsSpell : MonoBehaviour
{
    public Feature imposed;
    public ParticleSystem p_system;
    public float time;

    private float currneTime;

    private void Awake()
    {
        currneTime = 0;
        p_system = Instantiate(p_system, transform.position, transform.rotation, transform);
    }

    private void Start()
    {
        p_system.Play();
    }

    private void Update()
    {
        currneTime += Time.deltaTime;
        if(currneTime > time)
        {
            OnDest();
        }
    }

    private void OnDest()
    {
        Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        CharacterManager sportman = other.GetComponent<CharacterManager>();
        if(sportman != null && !sportman.characteristic.ExistFeature(imposed.data.featureName))
        {
            var instance = Instantiate(imposed, transform.position, transform.rotation);
            sportman.characteristic.AddFeature(instance);

        }
    }
}
