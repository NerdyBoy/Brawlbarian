using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class t_temp_destruction_script : MonoBehaviour {

    private Rigidbody destructible_object_rigidbody;
    [SerializeField]
    float destruction_wait_time = 5f;

	// Use this for initialization
	void Start () {
        destructible_object_rigidbody = GetComponent<Rigidbody> ();
        StartCoroutine (Deactivate_After_Time_And_Stop ());
	}

    IEnumerator Deactivate_After_Time_And_Stop () {
        while(GetComponent<Rigidbody>().velocity != Vector3.zero) {
            yield return new WaitForFixedUpdate ();
        }
        yield return new WaitForSeconds (destruction_wait_time);
        Destroy(this.gameObject);
    }
}
