using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetAudioParameter : MonoBehaviour
{
    public AudioMixer mixer;
    public string parameterName = "MasterVolume";
    public Slider slider;

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

    private void Update()
    {
        Parameter = slider.value;
    }

    /*public void ChangeAudio(float f)
    {
        Parameter = f;
        Debug.Log(f);
    }*/
}