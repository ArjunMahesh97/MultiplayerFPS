using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour {

    [SerializeField] RectTransform fuelFill;
    [SerializeField] GameObject pauseMenu;

    private PlayerController playerController;

	// Use this for initialization
	void Start () {
        PauseMenu.IsOn = false;
	}
	
	// Update is called once per frame
	void Update () {
        SetFuelAmount(playerController.GetFuelAmount());

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    private void TogglePauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        PauseMenu.IsOn = pauseMenu.activeSelf;
    }

    public void SetController(PlayerController controller)
    {
        playerController = controller;
    }

    void SetFuelAmount(float amount)
    {
        fuelFill.localScale = new Vector3(1f, amount, 1f);

    }


}
