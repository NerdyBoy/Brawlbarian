using UnityEngine;
using System.Collections;

public class flame_thrower_damage : MonoBehaviour {

    public GameObject fire_start;
    character_controller character_reference;

    private void Start()
    {
        character_reference = this.transform.root.GetComponent<character_controller>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(fire_start != null && other.CompareTag("flammable") == true)
        {
            other.transform.SendMessage("Set_Hit_Root_Character", character_reference, SendMessageOptions.DontRequireReceiver);
            Instantiate(fire_start, other.transform.position, Quaternion.identity);
        }
    }
}
