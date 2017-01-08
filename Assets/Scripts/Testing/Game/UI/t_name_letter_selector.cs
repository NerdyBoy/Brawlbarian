using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class t_name_letter_selector : MonoBehaviour {

    [SerializeField]
    private Text letter;

    string[] alphabet = new string[] {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

    int current_letter = 0;

	// Use this for initialization
	void Start () {
        letter.text = alphabet[current_letter];
	}

    void Update() {
        print(current_letter);
    }

    public void Increment() {
        if(25 == current_letter) {
            current_letter = 0;
        }
        else {
            current_letter++;
        }
        letter.text = alphabet[current_letter];
    }

    public void Decrement() {
        if(0 == current_letter) {
            current_letter = 25;
        }
        else {
            current_letter--;
        }
        letter.text = alphabet[current_letter];
    }
}
