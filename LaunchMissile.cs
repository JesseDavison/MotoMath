using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchMissile : MonoBehaviour
{
    public GameObject playerVehicle;
    VehiclePlayer playerVehicle_Script;

    private void Awake()
    {
        playerVehicle_Script = playerVehicle.GetComponent<VehiclePlayer>();
    }

    public void LaunchMissileNow() {
        GameManager.instance.FireMissile();
    }

    public void TurnOffGameObject() {
        gameObject.SetActive(false);
        playerVehicle_Script.EndFiringMissile();
    }

}
