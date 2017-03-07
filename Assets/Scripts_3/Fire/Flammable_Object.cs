using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Flammable_Object : MonoBehaviour {

    List<Flammable_Object> flammable_objects;
    public bool is_burning = false;
    public GameObject fire_prefab;
    List<GameObject> flammable_particles;
    List<Health> damagable_objects;
    public float lifetime = 15;

    private void Start()
    {
        lifetime = 3;
        List_Check();
        if(is_burning == true)
        {
            Ignite();
        }
    }

    private void List_Check()
    {
        if(flammable_objects == null)
        {
            flammable_objects = new List<Flammable_Object>();
            Find_Surrounding_Flammable_Objects();
        }

        if(flammable_particles == null)
        {
            flammable_particles = new List<GameObject>();
        }

        if(damagable_objects == null)
        {
            damagable_objects = new List<Health>();
            Find_Surrounding_Damagable_Objects();
        }
    }

    void Find_Surrounding_Flammable_Objects()
    {
        Flammable_Object[] all_flammable_objects = FindObjectsOfType<Flammable_Object>();
        for(int i = 0; i < all_flammable_objects.Length; i++)
        {
            float distance = Vector3.Distance(this.transform.position, all_flammable_objects[i].transform.position);
            if(distance > 0 && distance < 3)
            {
                flammable_objects.Add(all_flammable_objects[i]);
            }
        }
    }

    void Find_Surrounding_Damagable_Objects()
    {
        Health[] all_damagable_objects = FindObjectsOfType<Health>();
        for (int i = 0; i < all_damagable_objects.Length; i++)
        {
            float distance = Vector3.Distance(this.transform.position, all_damagable_objects[i].transform.position);
            if (distance > 0 && distance < 1)
            {
                damagable_objects.Add(all_damagable_objects[i]);
            }
        }
    }

    public void Ignite()
    {
        List_Check();
        StartCoroutine(Burn());
    }

    IEnumerator Burn()
    {
        is_burning = true;
        StartCoroutine(Spread());
        Spawn_Fire();
        yield return new WaitForSeconds(lifetime);
        Damage_Objects();
        Despawn_Fire();
        is_burning = false;
    }

    void Damage_Objects()
    {
        for(int i = 0; i < damagable_objects.Count; i++)
        {
            damagable_objects[i].Modify_Health(-100);
            if(damagable_objects[i] == null)
            {
                damagable_objects.RemoveAt(i);
            }
        }
    }

    IEnumerator Spread()
    {
        for(int i = 0; i < flammable_objects.Count; i++)
        {
            yield return new WaitForSeconds(0.05f);
            if(flammable_objects[i] != null && flammable_objects[i].is_burning == false)
            {
                if (Random.Range(1, 5) % 2 == 0)
                {
                    flammable_objects[i].Ignite();
                }
            }
        }
    }

    void Spawn_Fire()
    {
        if(fire_prefab != null)
        {
            if (flammable_particles != null)
            {
                GameObject fire = Instantiate(fire_prefab, transform.position, transform.rotation) as GameObject;
                fire.transform.parent = this.transform;
                flammable_particles.Add(fire);
            }
        }
    }

    void Despawn_Fire()
    {
        if(flammable_particles != null)
        {
            for(int i = 0; i < flammable_particles.Count; i++)
            {
                Destroy(flammable_particles[i]);
            }
        }
    }
}
