using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum MinionState { Walk, Argressive, Fight }


public class AI : MonoBehaviour
{
    public Transform[] goals;
    private int goalnumber = 0;
    private NavMeshAgent agent;
    private Transform CurrentEnemy;
    public Transform[] Enemies;
    private float RangeofAgr = 15f;
    private float RangeofBattle = 4f;
    private float NormalSpeed = 5f;
    private float AgressionSpeed = 4f;
    public Animator anim;

    private MinionState state;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = NormalSpeed;
        agent.destination = goals[0].position;
        anim.SetBool("Run", true);
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case MinionState.Walk:
                {
                    if (agent.remainingDistance < 0.5f)
                        GotoNextPoint();
                    foreach (Transform PotentionalEnemy in Enemies)
                    {
                        if (PotentionalEnemy == null) continue;
                        if (Vector3.Distance(transform.position, PotentionalEnemy.position) < RangeofAgr)
                        {
                            state = MinionState.Argressive;
                            CurrentEnemy = PotentionalEnemy;
                            return;
                        }
                    }
                }
                break;
            case MinionState.Argressive:
                {
                    if (Vector3.Distance(transform.position, CurrentEnemy.position) > RangeofAgr)
                    {
                        state = MinionState.Walk;
                        CurrentEnemy = null;
                        agent.destination = goals[goalnumber].position;
                        return;
                    }
                    
                    if (Vector3.Distance(transform.position, CurrentEnemy.position) > RangeofBattle)
                    {
                        agent.destination = CurrentEnemy.position;
                        agent.speed = AgressionSpeed;
                    }
                    if (Vector3.Distance(transform.position, CurrentEnemy.position) <= RangeofBattle)
                    {
                        state = MinionState.Fight;
                        return;
                    }
                    
                }
                break;
            case MinionState.Fight:
                {  
                    if (Vector3.Distance(transform.position, CurrentEnemy.position) <= RangeofBattle)
                    {
                        anim.SetBool("Run", false);
                    }
                    if (Vector3.Distance(transform.position, CurrentEnemy.position) > RangeofBattle)
                    {
                        anim.SetBool("Run", true);
                        state = MinionState.Argressive;
                    }

                }
                break;
        }
    }
    private void GotoNextPoint()
    {
        goalnumber = (goalnumber + 1) % goals.Length;
        agent.destination = goals[goalnumber].position;
    }
}
