using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Destructible_Object))]
public class Health : MonoBehaviour
{

    public Destructible_Object destruct_object;
    public int health = 100;

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        destruct_object = GetComponent<Destructible_Object>();
    }

    public virtual void Modify_Health(int _amount)
    {
        health += _amount;
        Health_Check();
        if (Score.score != null)
        {
            Score.score.Update_Score(Mathf.Abs(_amount));
        }

    }

    void Health_Check()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        destruct_object.Replace();
    }

}
