using UnityEngine;
using System.Collections;

public class destroy_after_time : MonoBehaviour {
    [SerializeField]
    GameObject[] powerups;

    [SerializeField]
    AudioClip[] smash;

    AudioSource source;
    [SerializeField]
    private float lifetime;

    private void Start()
    {
        int power_rand = Random.Range(0, 20);
        if(1 == power_rand)
        {
            if (powerups.Length > 0)
            {
                Instantiate(powerups[Random.Range(0, powerups.Length)], this.transform.position, Quaternion.identity);
            }
        }
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
