using UnityEngine;
using System.Collections;

public class Character_Health : Health {

    public override void Die()
    {
        Application.LoadLevel(4);
    }
}
