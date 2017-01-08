using UnityEngine;
using System.Collections;

public class destroy_after_time : MonoBehaviour {

    [SerializeField]
    private float lifetime;

    private void Start()
    {
        Destroy(this.gameObject, lifetime);
    }
}
