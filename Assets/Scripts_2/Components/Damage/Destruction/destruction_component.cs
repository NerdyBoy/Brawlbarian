using UnityEngine;
using System.Collections;

public class destruction_component : MonoBehaviour, destruction_interface {

    private void Update()
    {
        if(this.transform.position.y < -100)
        {
            Destroy(this.gameObject);
        }
    }

    public virtual void On_Health_Is_Zero()
    {
        Destroy(this.gameObject);
    }
}
