using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public static EnemyController instance;

    public NavMeshAgent agent;
    public GameObject FinishLine;
    public GameObject[] objectPool;

    private int targetNo;

    // Start is called before the first frame update
    void Start()
    {
        agent.updateRotation = false;
        Invoke("Setdestination", 0.5f);

        //agent.SetDestination(FinishLine.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.z == objectPool[targetNo].transform.position.z && targetNo < 37)
        {
            targetNo++;
            Invoke("Setdestination", 0.5f);
           
        }

    }

    void Setdestination()
    {
        agent.SetDestination(objectPool[targetNo].transform.position);
        //targetNo++;
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            
        }
    }*/

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Finish"))
        {
            transform.GetComponent<Rigidbody>().isKinematic = true;
            transform.GetComponent<NavMeshAgent>().enabled = false;
            GameManager.instance.Confetti.SetActive(true);

            if(GameManager.instance.Crown_Enemy.transform.position.z > GameManager.instance.Crown_Player.transform.position.z)
            {
                GameManager.instance.PlayerLost();
                transform.GetComponent<Animator>().SetBool("hasWon",true);
                Ballcontroller.instance.transform.GetComponent<Animator>().SetBool("hasLost", true);

            }
            else
            {
                GameManager.instance.PlayerWon();

            }
        }
    }
}
