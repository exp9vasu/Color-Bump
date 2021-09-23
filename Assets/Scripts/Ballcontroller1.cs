using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballcontroller1 : MonoBehaviour
{
    public static Ballcontroller1 instance;

    public EnemyController enemyController;

    [SerializeField] private float thrust = 150f;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float wallDistance = 5f;
    [SerializeField] private float minCamDistance = 3f;
    private Vector3 force;

    private Vector2 lastMousePos;
    public GameObject RivalBall;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 deltaPos = Vector2.zero;

        if (Input.GetMouseButton(0))
        {
            //RivalBall.GetComponent<Animator>().enabled = true;

            Vector2 currentmousePos = Input.mousePosition;

            if (lastMousePos == Vector2.zero)
                lastMousePos = currentmousePos;

            deltaPos = currentmousePos - lastMousePos;
            lastMousePos = currentmousePos;

            force = new Vector3(deltaPos.x, 0, deltaPos.y) * thrust;
            
            rb.AddForce(force);

            //rb.MovePosition(force);

            //rb.transform.Translate()

            /*if (Mathf.Abs(deltaPos.x) > Mathf.Abs(deltaPos.y))
            {
                force = new Vector3(deltaPos.x, 0, 0) * thrust;
            }
            else if (Mathf.Abs(deltaPos.y) > Mathf.Abs(deltaPos.x))
            {
                force = new Vector3(0, 0, deltaPos.y) * thrust;
            }*/
        }
        else
        {
            lastMousePos = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        Vector3 pos = transform.position;

        if (transform.position.x < -wallDistance)
        {
            pos.x = -wallDistance;
        }
        else if (transform.position.x > wallDistance)
        {
            pos.x = wallDistance;
        }

        if (transform.position.z < Camera.main.transform.position.z + minCamDistance)
        {
            pos.z = Camera.main.transform.position.z + minCamDistance;
        }

        transform.position = pos;

        if (GameManager.instance.GameEnded)
        { 
            return; 
        }

        if (GameManager.instance.GameStarted)
        {
            rb.MovePosition(transform.position + Vector3.forward * 5 * Time.fixedDeltaTime);
        }
    }

    private void LateUpdate()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (GameManager.instance.GameEnded)
        { return; }

        if (collision.gameObject.tag == "Death")
        {
            GameManager.instance.EndGame(false);
        }

        if (collision.collider.CompareTag("Finish"))
        {
            transform.GetComponent<Rigidbody>().isKinematic = true;
            GameManager.instance.Confetti.SetActive(true);


            if (GameManager.instance.Crown_Player.transform.position.z < GameManager.instance.Crown_Enemy.transform.position.z)
            {
                GameManager.instance.PlayerLost();
               

            }
            else
            {
                GameManager.instance.PlayerWon();
                transform.GetComponent<Animator>().SetBool("hasWon", true);
                enemyController.transform.GetComponent<Animator>().SetBool("hasLost", true);
            }
        }
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            transform.GetComponent<Rigidbody>().isKinematic = true;
        }
    }*/
}