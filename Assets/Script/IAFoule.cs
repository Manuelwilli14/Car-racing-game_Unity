using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAFoule : MonoBehaviour
{
    //public List<GameObject> waypoints;
    public List<GameObject> waypoints;
    public float speed;
    int currentWaypoint = 0;
    bool turnAtNextPoint = false;

    // Start is called before the first frame update
    void Start()
    {
        speed = GenerateRandomSpeed(6, 14);
    }

    // Update is called once per frame
    void Update()
    {
        speed = speed + 0.01f;
        Vector3 destination = waypoints[currentWaypoint].transform.position;
        Vector3 newPosi = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        transform.position = newPosi;

        float distance = Vector3.Distance(transform.position, destination);

     
        if (distance < 1.0f)
        {
            //speed = speed + 3f;

            if (currentWaypoint == 6)
            {
                //transform.Rotate(0, 45, 0, Space.Self);
                //transform.Rotate(Vector3.up * Time.deltaTime * speed);
                turnAtNextPoint = true;
            }
            currentWaypoint++;
        }

        if(turnAtNextPoint)
        {
            transform.Rotate(0, -45, 0, Space.Self);
            //transform.RotateAround(waypoints[6].transform.position, Vector3.up, -45.0f * Time.deltaTime);
            turnAtNextPoint = false;

        }
    }


    void OnTriggerEnterStay(Collider otherCollider)
    {
        // peut on eviter la collision
        if (IsRelevantCollision(otherCollider))
        {
            // Calcul positions relatives
            Vector3 relativePosition = otherCollider.transform.position - transform.position;

            // eviter
            if (relativePosition.x > 0) // obstacle a droite
            {
                SteerLeft();
            }
            else if (relativePosition.x < 0) // Obstacle a gauche
            {
                SteerRight();
            }
            else if (relativePosition.z > 0) // Obstacle devant
            {
                Brake();
            }
            else // Obstacle derriere
            {
               
            }
        }
    }

    // declqncher quand il y q collision
    void OnCollisionEnter(Collision collision)
    {
        // peut on eviter la coll
        if (IsRelevantCollision(collision.collider))
        {
            //perspective
        }
    }

    // est ce qu on peut eviter la coll
    bool IsRelevantCollision(Collider otherCollider)
    {
        // est ce que le collider ne fait pas partie de la voiture elle meme
        if (otherCollider.gameObject == gameObject)
        {
            return false;
        }

        // collider dqns rayon
        float distance = Vector3.Distance(transform.position, otherCollider.transform.position);
        if (distance > 5.0f)
        {
            return false;
        }

        return true;
    }

    // tourne a gauche
    void SteerLeft()
    {
        transform.Rotate(0, -45.0f, 0, Space.Self);
    }

    // tourne a droite
    void SteerRight()
    {
        transform.Rotate(0, 45.0f, 0, Space.Self);
    }

    // 
    void Brake()
    {
        speed *= 0.8f;
    }

    float GenerateRandomSpeed(float min, float max)
    {
        return Random.Range(min, max);
    }
}
