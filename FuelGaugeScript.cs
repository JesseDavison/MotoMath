using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelGaugeScript : MonoBehaviour
{

    // full: -38
    // empty: 38
    // if F(x) = a*x + b, and F(100% fuel) = a * 100 + b = -38 degrees, and F(0) = b = 38
    //      then we get:    F(x) = (-19/25)*x + 38, where x is the fuel level and F(x) is the angle of the needle




    public float fuelConsumptionRate;

    public float fuelAmount = 100;
    public bool fuelBeingBurned;

    public float angle;

    int counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (fuelBeingBurned) {
            counter += 1;

            if (counter >= 100) {
                counter = 0;
                fuelAmount -= Time.deltaTime * fuelConsumptionRate;
                if (fuelAmount <= 0) {
                    fuelAmount = 0;
                    fuelBeingBurned = false;
                }


                Vector3 temp = transform.rotation.eulerAngles;

                PlayerPrefs.SetFloat("fuelInInventory", fuelAmount);
                GameManager.instance.ShowLevelUI_ammo_and_inventory_Display();


                // BETTER:
                //      F(x) = ((-2 * angle) / 100) * x + angle

                temp.z = ((-2f * angle) / 100f) * fuelAmount + angle;

                transform.rotation = Quaternion.Euler(temp);

            }



        }







        //transform.Rotate(0, 0, zAxisRotationSpeed);

    }

    public void AddFuelToGauge(float amount) {
        fuelAmount += amount;
        if (fuelAmount >= 100) {
            fuelAmount = 100;
        }

        Vector3 temp = transform.rotation.eulerAngles;

        temp.z = ((-2f * angle) / 100f) * fuelAmount + angle;

        transform.rotation = Quaternion.Euler(temp);

    }

    public void StopFuelConsumption() {
        fuelBeingBurned = false;
    }
    public void StartFuelConsumption() {
        fuelBeingBurned = true;
    }


}
