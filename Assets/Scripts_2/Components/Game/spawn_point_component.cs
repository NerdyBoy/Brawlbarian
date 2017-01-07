using UnityEngine;
using System.Collections;

public class spawn_point_component : MonoBehaviour {

    private GameObject player_object;

    public bool Claim_Spawn_Point(GameObject _player)
    {
        if(null == player_object)
        {
            player_object = _player;
            return true;
        }
        return false;
    }

    public GameObject Get_Player_Object()
    {
        return player_object;
    }
}
