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

    public GameObject GaugeCircle;      // to fluctuate in size when fuel level gets low
    RectTransform GaugeCircle_RectTransform;
    float takeTheSineOfThis = 0;
    public float fluctuateSpeed_lessThan10 = 0.1f;
    public float fluctuateSpeed_lessThan5 = 0.35f;
    public float fluctuateSpeed_lessThan1 = 1f;


    private void Awake()
    {
        GaugeCircle_RectTransform = GaugeCircle.GetComponent<RectTransform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (fuelAmount <= 0)
        {


            GaugeCircle_RectTransform.localScale = new Vector3(1, 1, 1);
            transform.localScale = new Vector3(1, 1, 1);

            fuelAmount = 0;
            if (fuelBeingBurned) {
                StopFuelConsumption();
                GameManager.instance.StartOutOfFuelSlowdown();
            }


        }
        else if (fuelAmount <= 1)
        {
            float tempThing = Mathf.Sin(takeTheSineOfThis) / 10f;      // change range from -1/1, to -0.1 / 0.1
            tempThing += 1f; // change range to 0.9 / 1.1

            GaugeCircle_RectTransform.localScale = new Vector3(tempThing, tempThing, tempThing);
            transform.localScale = new Vector3(tempThing, tempThing, tempThing);

            takeTheSineOfThis += fluctuateSpeed_lessThan1;
        }
        else if (fuelAmount <= 5)
        {
            float tempThing = Mathf.Sin(takeTheSineOfThis) / 10f;      // change range from -1/1, to -0.1 / 0.1
            tempThing += 1f; // change range to 0.9 / 1.1

            GaugeCircle_RectTransform.localScale = new Vector3(tempThing, tempThing, tempThing);
            transform.localScale = new Vector3(tempThing, tempThing, tempThing);

            takeTheSineOfThis += fluctuateSpeed_lessThan5;
        }
        else if (fuelAmount <= 10.9f)
        {
            // make the fuel gauge fluctuate in size between 0.9 & 1.1 scale
            float tempThing = Mathf.Sin(takeTheSineOfThis) / 10f;      // change range from -1/1, to -0.1 / 0.1
            tempThing += 1f; // change range to 0.9 / 1.1

            GaugeCircle_RectTransform.localScale = new Vector3(tempThing, tempThing, tempThing);
            transform.localScale = new Vector3(tempThing, tempThing, tempThing);

            takeTheSineOfThis += fluctuateSpeed_lessThan10;


        } 
        else
        {
            GaugeCircle_RectTransform.localScale = new Vector3(1, 1, 1);
            transform.localScale = new Vector3(1, 1, 1);
        }


        if (fuelBeingBurned) {

            fuelAmount -= Time.deltaTime * (fuelConsumptionRate / 100f);
            Vector3 temp = transform.rotation.eulerAngles;
            PlayerPrefs.SetFloat("fuelInInventory", fuelAmount);
            GameManager.instance.ShowLevelUI_ammo_and_inventory_Display();
            temp.z = ((-2f * angle) / 100f) * fuelAmount + angle;
            transform.rotation = Quaternion.Euler(temp);




            //else if (fuelAmount > 10) {

            //    counter += 1;
            //    if (counter >= 100)
            //    {
            //        counter = 0;
            //        fuelAmount -= Time.deltaTime * fuelConsumptionRate;

            //        //if (fuelAmount <= 0)
            //        //{

            //        //    fuelAmount = 0;
            //        //    StopFuelConsumption();
            //        //    GameManager.instance.StartOutOfFuelSlowdown();

            //        //}


            //        Vector3 temp = transform.rotation.eulerAngles;
            //        PlayerPrefs.SetFloat("fuelInInventory", fuelAmount);
            //        GameManager.instance.ShowLevelUI_ammo_and_inventory_Display();
            //        // BETTER:
            //        //      F(x) = ((-2 * angle) / 100) * x + angle

            //        temp.z = ((-2f * angle) / 100f) * fuelAmount + angle;

            //        transform.rotation = Quaternion.Euler(temp);

            //    }
            //}





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
    public void ResetFuelToFullForNewGame() {
        GaugeCircle_RectTransform.localScale = new Vector3(1, 1, 1);
        fuelAmount = 100;
        Vector3 temp = transform.rotation.eulerAngles;
        PlayerPrefs.SetFloat("fuelInInventory", fuelAmount);
        GameManager.instance.ShowLevelUI_ammo_and_inventory_Display();
        temp.z = ((-2f * angle) / 100f) * fuelAmount + angle;
        transform.rotation = Quaternion.Euler(temp);

        fuelBeingBurned = true;

    }

    public void DisableFuelIssuesForQuitButton() {
        fuelAmount = 100;
    }


}
