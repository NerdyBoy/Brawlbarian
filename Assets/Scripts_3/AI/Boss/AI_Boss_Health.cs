using UnityEngine;
using System.Collections;

public class AI_Boss_Health : Health {

    Animator animator;
    public bool invulnerable = false;
    bool dead = false;
    // Use this for initialization


    private void OnLevelWasLoaded(int level)
    {
        if(Application.loadedLevel == 3)
        {
            invulnerable = false;
        }
    }

    void Start () {
        
        animator = GetComponent<Animator>();

        if (Application.loadedLevel == 3)
        {
            invulnerable = false;
        }
        base.Initialize();
    }
    
    public override void Modify_Health(int _amount)
    {
        if (invulnerable == false && animator.GetBool("being_damaged") == false)
        {
            base.Modify_Health(_amount);
            if (health > 0)
            {
                animator.SetTrigger("hit");
            }
        }
    }

    public void Health_Check()
    {
        if(health <= 0)
        {
            Die();
        }
    }

    public override void Die()
    {
        animator.applyRootMotion = true;

        if (dead == false && destruct_object == null)
        {
            dead = true;
            animator.SetTrigger("die");
            StartCoroutine(Die_After_Time());
        }
        else
        {
            destruct_object.Replace();
        }
    }

    IEnumerator Die_After_Time()
    {
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }
}
