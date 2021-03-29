using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetAudioParameter : MonoBehaviour
{
    public AudioMixer mixer;
    public string parameterName = "MasterVolume";
    public Slider slider;
    string directory = "/data.json";
    string overall_path;

    protected float Parameter
    {
        get
        {
            float parameter;
            mixer.GetFloat(parameterName, out parameter);
            return parameter;
        }
        set
        {
            mixer.SetFloat(parameterName, value);
        }
    }
    private void Start()
    {
        overall_path = Application.persistentDataPath + directory;
        slider.onValueChanged.AddListener(ChangeAudio);
        slider.value = Load();
        mixer.SetFloat(parameterName, slider.value);
        ChangeAudio(slider.value);
        Debug.Log(overall_path);
    }

    /*private void Update()
    {
        Parameter = slider.value;
    }*/

    public void ChangeAudio(float f)
    {
        Parameter = f;
        Save(f);

    }

    void Save(float f)
    {
        save_audio save_file = new save_audio
        {
            parameter = f,
        };
        File.WriteAllText(overall_path, JsonUtility.ToJson(save_file));
    }

    float Load()
    {
        if (File.Exists(overall_path))
        {
            string loaded_text = File.ReadAllText(overall_path);
            save_audio json = JsonUtility.FromJson<save_audio>(loaded_text);
            return json.parameter;
        }
        return 0f;
    }

    private class save_audio
    {
        public float parameter;
    }
}