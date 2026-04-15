using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Voitureprincipal : MonoBehaviour
{
    Vector3 right = Vector3.right;
    public float vitesse;
    public float acceleration = 0.5f;
    public float deceleration = 0.02f;
    public float maxSpeed = 0.6f;
    public float reverseSpeed = 0.5f;
    public float rotationSpeed = 0.2f;

    public Animation wheelsAnimDvGauche;
    public Animation wheelsAnimDvDroite;
    public Animation wheelsAnimDrGauche;
    public Animation wheelsAnimDrDroite;

    public TextMeshProUGUI countdownText; // Référence au composant Text UI

    bool isBlue;
    bool canMove = false;

    // Start is called before the first frame update
    void Start()
    {
        vitesse = 0f;
        StartCoroutine(Countdown()); //On démarre ici le décompte
    }

    // Coroutine de décompte
    IEnumerator Countdown()
    {
        float countdown = 5f;
        while (countdown > 0)
        {
            countdownText.text = "Départ dans : " + Mathf.Ceil(countdown).ToString();
            Debug.Log(Mathf.Ceil(countdown));
            yield return new WaitForSeconds(1f);
            countdown -= 1f;
        }

        countdownText.text = "";
        canMove = true; //On autorise maintenant la voiture à se déplacer
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove) //Tant que le decompte n'est pas terminée, on retourne rien
        {
            return;
        }

        // Son du moteur de la voiture
        GetComponent<AudioSource>().pitch = vitesse / maxSpeed + 1.5f;

        float rotationAmount = vitesse * rotationSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.Z))
        {
            Debug.Log(acceleration);
            vitesse = Mathf.Min(vitesse + acceleration * Time.deltaTime, maxSpeed);
            transform.Translate(Vector3.forward * vitesse);
            wheelsAnimDvDroite.Play("maroue", PlayMode.StopAll);
            wheelsAnimDvDroite.transform.Rotate(Vector3.right, rotationAmount);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (vitesse > 0f)
            {
                vitesse = Mathf.Max(vitesse - deceleration, 0f);
            }
            else
            {
                vitesse = Mathf.Max(vitesse - reverseSpeed * Time.deltaTime, -reverseSpeed);
            }
            transform.Translate(Vector3.forward * vitesse);
            wheelsAnimDvDroite.Play("maroue", PlayMode.StopAll);
        }
        else if (Input.GetKey(KeyCode.P))
        {
            Debug.Log("Touche P appuyée");
        }
        else
        {
            if (vitesse > 0f)
            {
                vitesse = Mathf.Max(vitesse - deceleration * Time.deltaTime, 0f);
            }
            else if (vitesse < 0f)
            {
                vitesse = Mathf.Min(vitesse + deceleration * Time.deltaTime, 0f);
            }
        }

        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.up, -rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up, rotationSpeed);
        }

        transform.Translate(Vector3.forward * vitesse);
    }
}
