using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public InputField field;
    public Backend backend;
    private void Start()
    {
        field.onEndEdit.AddListener(Update_name);
    }

    public void Update_name(string s)
    {
        Debug.Log("sending...");
        backend.Send_name(s);
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }
}
