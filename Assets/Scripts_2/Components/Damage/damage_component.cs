using UnityEngine;
using System.Collections;

public class damage_component : MonoBehaviour {

    [SerializeField]
    private float damage_amount;
    
    public float Get_Damage_Amount()
    {
        return damage_amount;
    } 
}
