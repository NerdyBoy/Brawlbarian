using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class destroyed_fragment_cleaner : MonoBehaviour {

    public static destroyed_fragment_cleaner fragment_cleaner;

    List<GameObject> destroyed_fragments;
    List<GameObject> reversed_fragments;
    public int fragment_limit = 300;

	// Use this for initialization
	void Start () {
        if(fragment_cleaner == null)
        {
            fragment_cleaner = this;
        }

        destroyed_fragments = new List<GameObject>();
	}
	
	public void Add_Fragment(GameObject _object)
    {
        if (destroyed_fragments.Count < fragment_limit)
        {
            destroyed_fragments.Add(_object);
        }
        else
        {
            Destroy(_object);
        }
    }

    public void Remove_Self(GameObject _object)
    {
        if(destroyed_fragments.Contains(_object) == true)
        {
            destroyed_fragments.Remove(_object);
        }
    }
}
