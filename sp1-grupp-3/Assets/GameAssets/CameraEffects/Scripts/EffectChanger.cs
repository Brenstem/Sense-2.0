using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class EffectChanger : MonoBehaviour
{
    PostProcessVolume postVolume;

    public PostProcessProfile profile;
    public PostProcessProfile profile2;
    public float speed;
    public float x = 0;
    public float destination;
    FloatParameter fuckOff;
    
    void Start()
    {
        postVolume = GetComponent<PostProcessVolume>();
    }
    
    void Update()
    {
        if(x != destination)
        {
            x = Mathf.MoveTowards(x, destination, speed);
        }
        profile.GetSetting<ColorGrading>().saturation.value = x;

        if(Input.GetKeyDown(KeyCode.F)) 
        {
            postVolume.profile = profile2;
        }
        else if(Input.GetKeyDown(KeyCode.G)) {
            postVolume.profile = profile;
        }
    }
}
