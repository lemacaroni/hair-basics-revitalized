using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Collections;

public class FamesScript : MonoBehaviour
{
	private void Start()
	{
		this.agent = base.GetComponent<NavMeshAgent>();
		this.TargetPlayer();
	}

	private void Update()
	{
		if (this.coolDown > 0f && agent.velocity.magnitude <= 0.01f)
		{
			this.coolDown -= 1f * Time.deltaTime;
		}
		howHungry += 2 * Time.deltaTime;
		print(howHungry);
		if (howHungry < 25)
		{
			agent.speed = 15;
		}
		if (howHungry >= 25 && howHungry < 50)
        {
			agent.speed = gc.player.walkSpeed / 1.2f;
		}
		if (howHungry >= 50 && howHungry < 75)
		{
			agent.speed = gc.player.runSpeed / 1.2f;
		}
	}

	private void FixedUpdate()
	{
		Vector3 direction = this.player.position - base.transform.position;
		if (Physics.Raycast(base.transform.position + Vector3.up * 2f, direction, out var hitInfo, float.PositiveInfinity, 769, QueryTriggerInteraction.Ignore) & (hitInfo.transform.tag == "Player"))
		{
			this.db = true;
		}
		else
		{
			this.db = false;
		}
		if (stop)
        {
			return;
        }
		if (((howHungry >= 25f && HasFoodItem()) || howHungry >= 50f) && coolDown <= 0f)
		{
			TargetPlayer();
		}
		else if (coolDown <= 0f)
		{
			Wander();
		}
	}

	private void Wander()
	{
		this.wanderer.GetNewTarget();
		this.agent.SetDestination(this.wanderTarget.position);
		this.coolDown = 1f;
	}

	private void TargetPlayer()
	{
		this.agent.SetDestination(this.player.position);
		this.coolDown = 1f;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.name == "Player" && howHungry >= 25f)
		{
			if (howHungry >= 25f && howHungry < 50 && HasFoodItem())
            {
				StartCoroutine(Chomp(true));
            }
            else if (howHungry >= 50f)
			{
				StartCoroutine(Chomp(false));
			}
		}
		if (other.transform.name == "Yellow Face")
		{
			gc.SomeoneTied(gameObject);
			gameObject.SetActive(false);
		}
	}

	bool HasFoodItem()
	{
		for (int i = 0; i < foods.Count; i++)
		{
			if (gc.item[0] == foods[i] || gc.item[1] == foods[i] || gc.item[2] == foods[i] || gc.item[3] == foods[i])
			{
				return true;
			}
		}
		return false;
	}
	int HasFoodItemWhich()
	{
		int[] items = new int[4];
		for (int i = 0; i < foods.Count; i++)
		{
			if (gc.item[0] == foods[i])
			{
				return 0;
			}
			if (gc.item[1] == foods[i])
			{
				return 1;
			}
			if (gc.item[2] == foods[i])
			{
				return 2;
			}
			if (gc.item[3] == foods[i])
			{
				return 3;
			}
		}
		return -1;
	}

	IEnumerator Chomp(bool item)
	{
		stop = true;
		anim.SetTrigger("Chomp");
		yield return new WaitForSeconds(0.4f);
		if (item)
		{
			gc.LoseItem(HasFoodItemWhich());
			howHungry -= 25;
		}
        else
        {
			gc.player.health -= 25;
        }
		if (howHungry < 75)
		{
			anim.SetInteger("Chew", 1);
			yield return new WaitForSeconds(5f);
		}
		stop = false;
	}

	public bool db;

	public Transform player;

	public Animator anim;

	public Transform wanderTarget;

	public AILocationSelectorScript wanderer;

	public float coolDown;

	public float howHungry;

	private NavMeshAgent agent;

	public GameControllerScript gc;

	public List<int> foods;

	bool stop;
}
