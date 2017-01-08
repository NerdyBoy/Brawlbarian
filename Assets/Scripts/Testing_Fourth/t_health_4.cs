using UnityEngine;
using System.Collections;

public class t_health_4 : MonoBehaviour {

    private int health;

    public delegate void On_Health_Changed(int _health, int _health_change);
    public event On_Health_Changed on_health_changed;

	public void Set_Health(int _amount)
    {
        int old_health = health;
        health = _amount;
        if(null != on_health_changed)
        {
            on_health_changed(health, health - old_health);
        }
    }

    public void Add_Health(int _amount)
    {
        int old_health = health;
        health += _amount;
        if(null != on_health_changed)
        {
            on_health_changed(health, health - old_health);
        }
    }

    public int Get_Health()
    {
        return health;
    }
}
