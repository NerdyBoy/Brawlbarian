using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class t_end_scene_controller : MonoBehaviour {

    [SerializeField]
    private Text final_score_text = null;
    [SerializeField]
    private Text high_score_text = null;

    private character_score_component score = null;

    void OnLevelWasLoaded() {
        StartCoroutine(Get_Player());
    }

    void Start()
    {
        cursor_display.Enable_Cursor();
    }

    IEnumerator Get_Player() {
        while (null == score) {
            score = FindObjectOfType<character_score_component>();
            yield return new WaitForSeconds(0);
        }
        final_score_text.text = "Final score: " + score.Get_Score().ToString();

        Destroy(score.gameObject);
    }
}
