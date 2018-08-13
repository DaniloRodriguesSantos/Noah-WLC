using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class PoolingPlayerBullet : MonoBehaviour {
        public  GameObject                   bulletPrefab;
        public  Transform                    bulletsContainer;
        private int                          currentBulletIndex;
        private List<PlayerBulletController> bullets;


        public void Start() {
            bullets          = new List<PlayerBulletController>();
            bullets.Capacity = GameConstants.PLAYER_BULLET_NUMBER;
            AddBulletsToThePool();
        }

        #region Regular Bullets
        private void AddBulletsToThePool() {
            for (int i = 0; i < GameConstants.PLAYER_BULLET_NUMBER; i++) {
                var go = Instantiate(bulletPrefab);
                go.SetActive(false);
                go.transform.parent = bulletsContainer;
                bullets.Add(go.GetComponent<PlayerBulletController>());
            }
        }

        public PlayerBulletController GetBullet() {
            PlayerBulletController b = bullets[currentBulletIndex];
            if (b.IsActive()) {
                print("Number of PlayerBullets not enough");
                return null;
            }

            currentBulletIndex = (currentBulletIndex + 1) % bullets.Count;
            return b;
        }
        #endregion

    }

