using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class level_management : MonoBehaviour {

    public static level_management current_level_management;

    private List<Scene> loaded_scenes;
    Scene last_scene;

    // Use this for initialization
    void Start() {
        current_level_management = this;
    }

    public void Load_Scene(int _scene_index)
    {
        SceneManager.LoadScene(_scene_index);
        last_scene = Get_Active_Scene();
    }

    public Scene Load_And_Return_Scene(int _scene_index)
    {
        SceneManager.LoadScene(_scene_index);
        last_scene = Get_Active_Scene();
        return SceneManager.GetActiveScene();
    }

    public void Load_Next_Scene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        last_scene = Get_Active_Scene();
    }

    public Scene Load_And_Return_Next_Scene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        last_scene = Get_Active_Scene();
        return SceneManager.GetActiveScene();
    }

    public void Load_Scene_Additive(int _scene_index)
    {
        SceneManager.LoadScene(_scene_index, LoadSceneMode.Additive);
        last_scene = Get_Active_Scene();
    }

    public Scene Load_And_Return_Scene_Additive(int _scene_index)
    {
        SceneManager.LoadScene(_scene_index, LoadSceneMode.Additive);
        last_scene = Get_Active_Scene();
        return SceneManager.GetActiveScene();
    }

    public void Load_Next_Scene_Additive()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Additive);
        last_scene = Get_Active_Scene();
    }

    public Scene Load_And_Return_Next_Scene_Additive()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Additive);
        last_scene = Get_Active_Scene();
        return SceneManager.GetActiveScene();
    }

    public Scene Get_Active_Scene()
    {
        return SceneManager.GetActiveScene();
    }

    public void Unload_Last_Scene()
    {
        SceneManager.UnloadScene(last_scene);
        Application.UnloadLevel(last_scene.buildIndex);
    }
}
