using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonController : MonoBehaviour, ISelectHandler
{
    [SerializeField] EventSystem canvasEventSystem;
    [SerializeField] GameObject selectOnStart;
    [SerializeField] GameObject menuActionController;

    private bool buttonSelected = false;
    private MenuActions menuActions;
    private GameObject currentlySelectedGameObject;

    // Reference fetching
    private void Start()
    {
        menuActions = menuActionController.GetComponent<MenuActions>();
    }

    // Checks for keyboard/gamepad input and switches to keyboard/gamepad control
    private void Update()
    {
        currentlySelectedGameObject = canvasEventSystem.currentSelectedGameObject;

        if (Input.GetAxis("Vertical") != 0 && !buttonSelected)
        {
            canvasEventSystem.SetSelectedGameObject(selectOnStart);
            buttonSelected = true;
        }

        // Switches back to mouse control if the player clicks
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1))
        {
            buttonSelected = false;
        }
    }

    // Checks for when new objects are selected with keyboard/gamepad input to play sound
    public void OnSelect(BaseEventData eventData)
    {
        menuActions.PlaySelectSound();
    }
}
