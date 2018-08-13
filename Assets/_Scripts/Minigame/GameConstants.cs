using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    public class GameConstants {

        // Tags
        public const string PLAYER_TAG                 = "Player";
        public const string PLAYER_BULLET_TAG          = "PlayerBullet";
        public const string BORDER_TAG                 = "Border";
        public const string ENEMY_TAG                  = "MiniG_Enemy";
        public const string ENEMY_BULLET_TAG           = "EnemyBullet";

		public const string ASTEROID_TAG               = "Asteroid";
		public const string BORDERZZ_TAG               = "BorderZZ";

        // Speed Values
        public const float ENEMY1_SPEED          = 4F;
		public const float ENEMY2_SPEED          = 6F;
		public const float ASTEROID_SPEED 		 = 2F;

        // Times 
        public const float PLAYER_SHOOT_INTERVAl             = 0.1F;
        public const float PLAYER_SPECIAL_INTERVAL           = 0.1F;
        public const float PLAYER_SPECIAL_COOL_DOWN_INTERVAL = 1F;
        public const float ENEMEY1_MIN_FIRE_INTERVAL         = 1F;
        public const float ENEMEY1_MAX_FIRE_INTERVAL         = 3F;

        // Number of Bullets
        public const int PLAYER_BULLET_NUMBER = 100;
        public const int ENEMY_1_2_BULLET_NUMBER = 50;
		public const int ENEMY3_BULLET_NUMBER = 50;

        // Attack Values
        public const float PLAYER_BULLET_FORCE                   = 1000F;
        public const float PLAYER_SPECIAL_FORCE                  = 500F;
        public const int   PLAYER_SPECIAL_NUMBER_OF_SINGLE_SHOTS = 8;
        public const int   PLAYER_SPECIAL_NUMBER_OF_WAVES        = 3;
        public const float PLAYER_SPECIAL_ANGLE                  = 360F / (float)PLAYER_SPECIAL_NUMBER_OF_SINGLE_SHOTS;
        public const float PLAYER_SPECIAL_START_ANGLE            = 90F;
        public const float PLAYER_SPECIAL_RADIUS                 = 0.5F;
        public const int   PLAYER_SPECIAL_MAX_NUMBER_OF_ATTACKS  = 5;
        public const float ENEMY1_BULLET_FORCE                   = 400;

    }

