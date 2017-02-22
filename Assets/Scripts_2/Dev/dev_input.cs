using UnityEngine;
using System.Collections;

public class dev_input : MonoBehaviour {

    public static dev_input developer_codes;

    string[] input_codes = { "kfc", "rageon" };
    string input;

    public int chicken_chance = 9;

	// Use this for initialization
	void Start () {
	    if(developer_codes == null)
        {
            developer_codes = this;
        }
	}
	
	// Update is called once per frame
	void Update () {
        Handle_Input();
	}

    void Handle_Input()
    {
        foreach(char c in Input.inputString)
        {
            if(c >= 'a' && c <= 'z')
            {
                input += c;
                Code_Check();
            }
        }
    }

    void Code_Check()
    {
        for(int i = 0; i < input_codes.Length; i++)
        {
            if (input.Contains(input_codes[i]))
            {
                switch(input_codes[i])
                {
                    case "kfc":
                        Chicken_Time();
                        break;
                    case "rageon":
                        Rage_On();
                        break;
                    default:
                        break;
                }
                input = "";
            }
        }
    }

    void Chicken_Time()
    {
        chicken_chance = 100;
    }

    void Rage_On()
    {
        if (rage_component.global_rage_component != null)
        {
            rage_component.global_rage_component.current_rage = 1000;
        }
    }
}
