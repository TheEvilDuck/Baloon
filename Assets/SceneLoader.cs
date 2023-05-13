using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]UI _ui;
    private void OnEnable() {
        _ui.reloadScene.onClick.AddListener(ReloadScene);
    }
    private void OnDisable() {
        _ui.reloadScene.onClick.RemoveListener(ReloadScene);
    }
    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
