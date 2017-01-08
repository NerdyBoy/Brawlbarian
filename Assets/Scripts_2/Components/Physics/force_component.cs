using UnityEngine;
using System.Collections;

public class force_component : MonoBehaviour {

    [SerializeField]
    private float force_amount = 1.0f;

    public float Get_Force_Amount()
    {
        return force_amount;
    }
}
