using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CleartilScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (wait > 0)
        {
            wait -= Time.deltaTime;
            return;
        }
        agent.SetDestination(player.position);
        StartCoroutine(Step());
    }

    IEnumerator Step()
    {
        wait = waitTime;
        anim.SetTrigger("step");
        agent.speed = 84;
        aud.Play();
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(Time.deltaTime);
        }
        agent.speed = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "UbrSpray(Clone)")
        {
            wait += 5;
            waitTime += 0.1f;
        }
    }

    public float waitTime = 2.4f;
    float wait = 3;

    public Animator anim;
    AudioSource aud;
    NavMeshAgent agent;

    public Transform player;
}
