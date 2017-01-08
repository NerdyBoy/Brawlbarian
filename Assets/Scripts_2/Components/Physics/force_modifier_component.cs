using UnityEngine;
using System.Collections;

public class force_modifier_component : MonoBehaviour {

    [SerializeField]
    private float force_modifier = 1.0f;

	public float Get_Force_Modifier()
    {
        return force_modifier;
    }
}
