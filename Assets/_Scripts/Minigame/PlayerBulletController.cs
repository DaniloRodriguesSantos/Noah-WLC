using System.Collections;
using System.Collections.Generic;
using UnityEngine;


	public class PlayerBulletController : MonoBehaviour
	{
        
		private static Quaternion zero = Quaternion.identity;
		private Transform trans;
		private Rigidbody2D rb;

		private GameObject UIGame;
        //public UIController scoreText;

        public void Awake ()
		{
			trans = GetComponent<Transform> ();
			rb = GetComponent<Rigidbody2D> ();
			//UIGame = GameObject.Find ("UI_Canvas");
			//scoreText = UIGame.GetComponent<UIController> ();
		}


		public void SetInMotion (Vector3 pos)
		{
			ToggleActive (true);
			trans.position = pos;
			rb.AddForce (Vector2.right * GameConstants.PLAYER_BULLET_FORCE);
		}

		public bool IsActive ()
		{
			return gameObject.activeInHierarchy;
		}


		public void ToggleActive (bool b)
		{
			gameObject.SetActive (b);
		}


		public void OnTriggerEnter2D (Collider2D hit)
		{
			if (hit.gameObject.CompareTag (GameConstants.BORDER_TAG)) {
				ToggleActive (false);
				return;
			} 
			//   if (hit.gameObject.CompareTag(GameConstants.ENEMY_BULLET_TAG)) {
//                hit.gameObject.GetComponent<EnemyBulletController>().ToggleActive(false);
			//  ToggleActive(false);
			//  return;
			//}

			//if (hit.gameObject.CompareTag (GameConstants.ENEMY_TAG)) {
			//	scoreText.score += 10;

			//	hit.gameObject.GetComponent<Enemy1Controller> ().Destroy ();
			//	ToggleActive (false);

			//	hit.gameObject.GetComponent<Enemy2Controller> ().Destroy ();
			//	ToggleActive (false);
			//	return;
			//}
		}
	}

