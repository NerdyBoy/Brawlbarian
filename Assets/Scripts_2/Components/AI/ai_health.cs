using UnityEngine;
using System.Collections;

public class ai_health : MonoBehaviour {

    public int health;

    private void OnCollisionEnter(Collision collision)
    {
        Weapon weapon = collision.gameObject.GetComponent<Weapon>();
        if (weapon != null)
        {
            health -= 2;
            if(health <= 0)
            {
                Destroy(this.gameObject);
            }

        }
    }
}
