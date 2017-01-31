using UnityEngine;
using System.Collections;

public class hitting_component : MonoBehaviour {
    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision _collision)
    {
        if (false == source.isPlaying)
        {
            source.Play();
        }
        hit_tracking_component hit_tracker = _collision.gameObject.GetComponent<hit_tracking_component>();
        if(null != hit_tracker)
        {
            hit_tracker.Set_Hit_Root_Character(this.transform.root.GetComponentInChildren<character_controller>());
        }
    }
}
