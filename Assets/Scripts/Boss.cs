using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{

	public Transform player;
	private Animator anim;
	public bool isFlipped = false;
	[SerializeField] private Transform firePoint;
	[SerializeField] private GameObject[] fireballs;

	int bossHealth = 350;
	public int currentHealth;

	private void Start()
	{
		anim = GetComponent<Animator>();
		currentHealth = bossHealth;
	}

    public void TakeDamage(int damage)
	{
		currentHealth -= damage;


		if (currentHealth <= 0)
		{
			Destroy(GetComponent<Collider2D>());
			anim.SetTrigger("Die");
		}
	}
	public void LookAtPlayer()
	{
		Vector3 flipped = transform.localScale;
		flipped.z *= -1f;

		if (transform.position.x > player.position.x && isFlipped)
		{
			transform.localScale = flipped;
			transform.Rotate(0f, 180f, 0f);
			isFlipped = false;
		}
		else if (transform.position.x < player.position.x && !isFlipped)
		{
			transform.localScale = flipped;
			transform.Rotate(0f, 180f, 0f);
			isFlipped = true;
		}
	}

	public void Attack()
    {
		if (fireballs[0].activeInHierarchy)
			return;
		fireballs[FindFireball()].transform.position = firePoint.position;
		if (isFlipped == true)
		{
			fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
		}
		else {
			fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x * -1f));
		}

	}

	private int FindFireball()
    {
		for (int i = 0; i < fireballs.Length; i++)
        {
			if (!fireballs[i].activeInHierarchy)
				return i;
        }
		return 0;
    }


	public void EndGame()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}