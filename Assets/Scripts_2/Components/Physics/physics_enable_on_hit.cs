using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class physics_enable_on_hit : MonoBehaviour
{

    private Rigidbody light_rigidbody;
    public float light_lifetime = 5;

    // Use this for initialization
    void Start()
    {
        light_rigidbody = GetComponent<Rigidbody>();
        if (null != light_rigidbody)
        {
            light_rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        physics_object_component physics_object = collision.gameObject.GetComponent<physics_object_component>();
        physics_damage_component physics_damage_object = collision.gameObject.GetComponent<physics_damage_component>();
        if (null != light_rigidbody && (null != physics_object || null != physics_damage_object))
        {
            light_rigidbody.constraints = RigidbodyConstraints.None;
            StartCoroutine(Kill_Light());
            //light_rigidbody.transform.root.GetComponentInChildren<ParticleSystem>().gameObject.SetActive(false);
        }
    }

    IEnumerator Kill_Light()
    {
        yield return new WaitForSeconds(light_lifetime);
        ParticleSystem[] particle_systems = light_rigidbody.gameObject.GetComponentsInChildren<ParticleSystem>();
        for(int i = 0; i < particle_systems.Length; i++)
        {
            particle_systems[i].gameObject.SetActive(false);
        }
    }
}
