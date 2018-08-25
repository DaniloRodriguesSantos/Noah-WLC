using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    public class GameConstants {

        // Tags
        public const string PLAYER_TAG                 = "Player";
        public const string PLAYER_ATTACK              = "MiniG_PlayerAttack";
        public const string BORDER_TAG                 = "Border";
        public const string BOSS_BORDER_TAG            = "MiniG_Boss_Border";
        public const string ENEMY_TAG                  = "MiniG_Enemy";
        public const string BOSS_TAG                   = "MiniG_Boss";

        // Speed Values

        // Times 

        // Number of Bullets
        public const int ENEMY_NUMBER = 15;

        // Attack Values
        public const float PLAYER_ATTACK_DURATION        = 0.5f;
        public const float PLAYER_ATTACK_COOLDOWN        = 5f;

        // PlayerPrefs Values
        public static void PlayerPrefsValues()
        {
        PlayerPrefs.SetString("canRetry", "false");
        }
}

