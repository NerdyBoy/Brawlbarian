using UnityEngine;
using System.Collections;

public class destroy_after_time : MonoBehaviour {

    [SerializeField]
    AudioClip[] smash;

    AudioSource source;
    [SerializeField]
    private float lifetime;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        if (smash.Length != 0 && null != source)
        {
            int rand = Random.Range(0, smash.Length);
            source.clip = smash[rand];
            source.Play();
        }
        Destroy(this.gameObject, lifetime);
    }
}
