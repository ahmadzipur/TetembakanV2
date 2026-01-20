using UnityEngine;
using System.Collections;

namespace InfimaGames.LowPolyShooterPack.Legacy
{
	public class TargetScript : MonoBehaviour
	{
		float randomTime;
		bool routineStarted = false;
		bool canBeHit = true;

		public bool isHit = false;

		[Header("Customizable Options")]
		public float minTime;
		public float maxTime;

		[Header("Audio")]
		public AudioClip upSound;
		public AudioClip downSound;

		[Header("Animations")]
		public AnimationClip targetUp;
		public AnimationClip targetDown;

		public AudioSource audioSource;
		private Collider targetCollider;

		[Header("Score")]
		public int point = 10;
		private GameManager gameManager;

		private void Start()
		{
			targetCollider = GetComponent<Collider>();
			gameManager = FindObjectOfType<GameManager>();
		}

		private void Update()
		{
			
            if (isHit && !routineStarted && canBeHit)
			{
				canBeHit = false;
				routineStarted = true;

				// Turunkan target
				GetComponent<Animation>().clip = targetDown;
				GetComponent<Animation>().Play();

				audioSource.clip = downSound;
				audioSource.Play();

				// targetCollider.enabled = false;

				if (gameManager != null)
					gameManager.AddScore(point);

				StartCoroutine(DelayTimer());
			}
		}

		private IEnumerator DelayTimer()
		{
			randomTime = Random.Range(minTime, maxTime);
			yield return new WaitForSeconds(randomTime);

			// Naikkan target
			GetComponent<Animation>().clip = targetUp;
			GetComponent<Animation>().Play();

			audioSource.clip = upSound;
			audioSource.Play();

			// targetCollider.enabled = true;

			isHit = false;
			routineStarted = false;
			canBeHit = true;
		}
	}
}
