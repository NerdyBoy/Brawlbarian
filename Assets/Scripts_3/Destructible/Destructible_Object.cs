using UnityEngine;
using System.Collections;

public class Destructible_Object : MonoBehaviour {

    public GameObject replacement_object;
    public GameObject particle_system_object;

    public void Replace()
    {
        if (this.replacement_object != null && this != null)
        {
            Instantiate(replacement_object, this.transform.position, this.transform.rotation);
            if (particle_system_object != null)
            {
                Instantiate(particle_system_object, this.transform.position, this.transform.rotation);
            }
            Destruction_Monitor.destruction_monitor.Destroy_Object();
            Destroy(this.gameObject);
        }
        Destroy(this.gameObject);
    }
}
