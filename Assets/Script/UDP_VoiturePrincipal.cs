using System.Collections;
using UnityEngine;
using TMPro;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class UDP_Voitureprincipal : MonoBehaviour
{
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
    public TextMeshProUGUI countdownText;

    bool canMove = false;

    // --- UDP ---
    UdpClient udpClient;
    Thread receiveThread;
    string currentGesture = "NONE";

    void Start()
    {
        vitesse = 0f;
        StartCoroutine(Countdown());

        // Démarrage de l'écoute UDP
        udpClient = new UdpClient(5052);
        receiveThread = new Thread(ReceiveData);
        receiveThread.IsBackground = true;
        receiveThread.Start();
    }

    // Réception des gestes depuis Python
    void ReceiveData()
    {
        while (true)
        {
            try
            {
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = udpClient.Receive(ref anyIP);
                currentGesture = Encoding.UTF8.GetString(data);
            }
            catch { }
        }
    }

    IEnumerator Countdown()
    {
        float countdown = 5f;
        while (countdown > 0)
        {
            countdownText.text = "Départ dans : " + Mathf.Ceil(countdown).ToString();
            yield return new WaitForSeconds(1f);
            countdown -= 1f;
        }
        countdownText.text = "";
        canMove = true;
    }

    void Update()
    {
        if (!canMove) return;

        // Son du moteur
        GetComponent<AudioSource>().pitch = vitesse / maxSpeed + 1.5f;
        float rotationAmount = vitesse * rotationSpeed * Time.deltaTime;

        // --- AVANT (clavier Z ou geste FORWARD) ---
        if (Input.GetKey(KeyCode.Z) || currentGesture == "FORWARD")
        {
            vitesse = Mathf.Min(vitesse + acceleration * Time.deltaTime, maxSpeed);
            wheelsAnimDvDroite.Play("maroue", PlayMode.StopAll);
            wheelsAnimDvDroite.transform.Rotate(Vector3.right, rotationAmount);
        }
        // --- ARRIERE (clavier S uniquement) ---
        else if (Input.GetKey(KeyCode.S))
        {
            if (vitesse > 0f)
                vitesse = Mathf.Max(vitesse - deceleration, 0f);
            else
                vitesse = Mathf.Max(vitesse - reverseSpeed * Time.deltaTime, -reverseSpeed);

            wheelsAnimDvDroite.Play("maroue", PlayMode.StopAll);
        }
        // --- DECELERATION NATURELLE ---
        else
        {
            if (vitesse > 0f)
                vitesse = Mathf.Max(vitesse - deceleration * Time.deltaTime, 0f);
            else if (vitesse < 0f)
                vitesse = Mathf.Min(vitesse + deceleration * Time.deltaTime, 0f);
        }

        // --- GAUCHE (clavier Q ou geste LEFT) ---
        if (Input.GetKey(KeyCode.Q) || currentGesture == "LEFT")
        {
            transform.Rotate(Vector3.up, -rotationSpeed);
        }
        // --- DROITE (clavier D ou geste RIGHT) ---
        else if (Input.GetKey(KeyCode.D) || currentGesture == "RIGHT")
        {
            transform.Rotate(Vector3.up, rotationSpeed);
        }

        transform.Translate(Vector3.forward * vitesse);
    }

    void OnDestroy()
    {
        receiveThread?.Abort();
        udpClient?.Close();
    }
}