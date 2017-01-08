using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ui_damage_component : MonoBehaviour {

    private Text damage_text;

    // Use this for initialization
    void Start()
    {
        damage_text = GetComponent<Text>();
    }

    void Update_Damage(float _amount)
    {
        damage_text.text = "Damage: x" + _amount.ToString();
    }
}
