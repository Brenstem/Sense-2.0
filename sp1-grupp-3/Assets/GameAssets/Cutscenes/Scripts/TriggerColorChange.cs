using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerColorChange : MonoBehaviour
{
    [SerializeField] string playerTag;
    [SerializeField] float cutsceneLength;

    private Timer timer;

    private void Start()
    {
        timer = new Timer();
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag(playerTag))
        {
            GetComponent<BoxCollider2D>().enabled = false;
            timer.StartTimer(cutsceneLength);

        }
    }

    private void Update()
    {
        timer.UpdateTimer();

        if (timer.TimerFinished)
        {
            Camera.main.GetComponent<ColorChangerEffect>().ActivateEffect();
            timer.ResetTimer();
        }
    }
}
