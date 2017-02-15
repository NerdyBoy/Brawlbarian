﻿using UnityEngine;
using System.Collections;

public class destruction_object_component : destruction_component {

    [SerializeField]
    private GameObject replacement_object;

    public float explosive_force;
    public float explosion_radius;

    public GameObject particle_emitter_prefab;

    character_score_component score_component;

    void Start()
    {
        score_component = FindObjectOfType<character_score_component>();
    }

    public override void On_Health_Is_Zero()
    {
        if(particle_emitter_prefab != null)
        {
            GameObject obj = Instantiate(particle_emitter_prefab, this.transform.position, Quaternion.identity) as GameObject;
            obj.transform.Rotate(new Vector3(90, 0, 0));
        }
        if(replacement_object != null)
        {
            Rigidbody object_rigidbody = GetComponent<Rigidbody>();
            GameObject destroyed = Instantiate(replacement_object, this.transform.position, this.transform.rotation) as GameObject;
            for(int i = 0; i < destroyed.transform.childCount; i++)
            {
                Rigidbody rbody = destroyed.transform.GetChild(i).gameObject.GetComponentInChildren<Rigidbody>();
                if (null != rbody && object_rigidbody != null)
                {
                    rbody.AddExplosionForce(explosive_force, this.transform.position, explosion_radius);
                }
            }
            rage_component.global_rage_component.total_rage += 10;
        }
        if(score_component == null)
        {
            score_component = FindObjectOfType<character_score_component>();
        }

        if(score_component != null)
        {
            score_component.Modify_Score(10);
        }
        Destroy(this.gameObject);
    }
}
