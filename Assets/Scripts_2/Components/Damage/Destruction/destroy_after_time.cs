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
        StartCoroutine(Destroy_After_Lifetime());
        source = GetComponent<AudioSource>();
        if (smash.Length != 0 && null != source)
        {
            int rand = Random.Range(0, smash.Length);
            source.clip = smash[rand];
            source.Play();
        }
        //Destroy(this.gameObject, lifetime);
    }

    IEnumerator Destroy_After_Lifetime()
    {
        yield return new WaitForSeconds(lifetime);
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Rigidbody rig = this.transform.GetChild(i).GetComponent<Rigidbody>();
            Collider col = this.transform.GetChild(i).GetComponent<Collider>();
            if(rig != null)
            {
                rig.isKinematic = true;
            }
            if(col != null)
            {
                col.enabled = false;
            }
        }
        if(destroyed_fragment_cleaner.fragment_cleaner != null)
        {
            destroyed_fragment_cleaner.fragment_cleaner.Remove_Self(this.gameObject);
        }
        Destroy(this.gameObject);
    }
}
