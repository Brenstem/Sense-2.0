using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ColorChangerEffect : MonoBehaviour
{
    PostProcessVolume postVolume;

    public PostProcessProfile profile;

    public float speed;
    public float startingPoint = 0;
    public float destination;
    private float meme;
    bool activateEffect = false;


    void Start()
    {
        postVolume = GetComponent<PostProcessVolume>();
        profile.GetSetting<ColorGrading>().saturation.value = -80;

    }

    void Update()
    {
        if(activateEffect == true)
        {
            if (startingPoint != destination)
            {
                startingPoint = Mathf.MoveTowards(startingPoint, destination, speed);
            }
            profile.GetSetting<ColorGrading>().saturation.value = startingPoint;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            ActivateEffect();
        }
    }

    public void ActivateEffect()
    {
        if(startingPoint != -80)
        {
            startingPoint = -80;
        }
        profile.GetSetting<ColorGrading>().saturation.value = startingPoint;

        activateEffect = true;
    }
}