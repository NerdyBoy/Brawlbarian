using UnityEngine;
using System.Collections;

[RequireComponent(typeof(health_component))]
public class destruction_component : MonoBehaviour, destruction_interface {

    public virtual void On_Health_Is_Zero()
    {
        Destroy(this.gameObject);
    }
}
