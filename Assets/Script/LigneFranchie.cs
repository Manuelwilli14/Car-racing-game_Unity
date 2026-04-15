using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LigneFranchie : MonoBehaviour
{
    private int rank = 1; // ordre d'arrivée
    private int totalCars; // Total number of cars
    private int carsFinished = 0; // Number of cars that have finished
    public TextMeshProUGUI countdownText;

    private List<string> classementFinal = new List<string>(); // Liste pour stocker le classement final


    void Start()
    {
        totalCars = GameObject.FindGameObjectsWithTag("Car").Length;
        Debug.Log("Total Cars: " + totalCars);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            Debug.Log("Ligne franchie");
            Debug.Log("Car " + other.name + " a fini en position " + rank);

            classementFinal.Add("Position " + rank + ": " + other.name);

            rank++;
            carsFinished++;


            if (carsFinished >= totalCars)
            {
                Debug.Log("Tous les véhicules ont franchi la ligne d'arrivée");
                classement();
                //Application.Quit(); 
            }
        }
    }
    void classement()
    {
        countdownText.text = "Classement Final:\n";
        foreach (string ligne in classementFinal)
        {
            countdownText.text += ligne + "\n";
        }
    }
}
