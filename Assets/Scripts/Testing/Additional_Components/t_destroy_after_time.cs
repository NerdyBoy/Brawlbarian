using UnityEngine;
using System.Collections;

public class t_destroy_after_time : MonoBehaviour {

    [SerializeField]
    private float object_lifespan = 0.0f;

	// Use this for initialization
	void Start () {
        StartCoroutine(Destroy_Object());
	}

    public float Get_Lifespan()
    {
        return object_lifespan;
    }

    public void Set_Lifespan(float _lifespan)
    {
        object_lifespan = _lifespan;
    }
	
	IEnumerator Destroy_Object()
    {
        yield return new WaitForSeconds(object_lifespan);
        Destroy(this.gameObject);
    }
}
