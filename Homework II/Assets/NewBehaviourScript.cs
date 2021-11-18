using UnityEngine;
using UnityEngine.AI;

public class NewBehaviourScript : MonoBehaviour
{
    NavMeshAgent agent;
    public Animator anim;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float Speed = agent.velocity.magnitude;
        anim.SetFloat("Speed", Speed);
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                agent.destination = hit.point;
            }
        }
    }
}