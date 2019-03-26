using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour {

    [SerializeField] RectTransform fuelFill;

    private PlayerController playerController;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        SetFuelAmount(playerController.GetFuelAmount());
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
