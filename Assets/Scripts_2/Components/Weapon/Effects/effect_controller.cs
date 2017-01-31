using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(fire_element_component))]
public class effect_controller : MonoBehaviour {

    BoxCollider effect_collider;
    public float effect_time;
    public elemental_base_component[] elemental_components;
    elemental_base_component current_effect;

	// Use this for initialization
	void Start () {
        //Physics.IgnoreCollision(effect_collider, FindObjectOfType<character_controller>().gameObject.GetComponent<Collider>(), true);
        effect_collider = GetComponent<BoxCollider>();
        elemental_components = GetComponents<elemental_base_component>();
        Set_Effect_Active(true);
	}

    public void Set_Effect_Active(bool _active)
    {
        if(true == _active)
        {
            Select_Random_Component();
            StartCoroutine(Toggle_Effect());
        }
        else
        {
            StopCoroutine(Toggle_Effect());
            effect_collider.enabled = false;
        }
    }

    void Select_Random_Component()
    {
        if (elemental_components.Length > 0)
        {
            current_effect = elemental_components[Random.Range(0, elemental_components.Length)];
        }
    }

    IEnumerator Toggle_Effect()
    {
        effect_collider.enabled = true;
        yield return new WaitForSeconds(effect_time);
        effect_collider.enabled = false;
        current_effect = null;
    }

    private void OnCollisionEnter(Collision _collision)
    {
        if(null != current_effect)
        {
            current_effect.Activate_On_Collision(_collision);
        }
    }
}
