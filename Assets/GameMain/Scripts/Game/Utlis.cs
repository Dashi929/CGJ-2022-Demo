using System.Collections;
using System.Collections.Generic;
using GameMain;
using UnityEditor;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace GameMain {
    public class Utlis : GameFrameworkComponent
    {
        private int m_combo;
        public int Combo
        {
            get { return m_combo; }
            set
            {
                if (value != m_combo)
                {
                    GameEntry.Event.FireNow(this, AddComboEventArgs.Create(value));
                    m_combo = value;
                    Debug.Log("comboNum" + value);
                }
            }
        }

        private int m_score;
        public int Score
        {
            get { return m_score; }
            set
            {
                if (value != m_score)
                {
                    GameEntry.Event.FireNow(this, AddScoreEventArgs.Create(value));
                    m_score = value;
                    Debug.Log("ScoreNum" + value);
                }
            }
        }

        public int upGradeCount = 0;
        public int curLevel = 1;


        public int? storylineID = null;
        public int? pauseID = null;
        public int? endID = null;

        public enum GameOverState
        {
            Undefined,
            End1,
            End2,
            End3
        }
        public GameOverState gameOverState = GameOverState.Undefined;

        // public Sprite GetSprite(string spriteName)
        // {
        //     return (Sprite)AssetDatabase.LoadAssetAtPath(AssetUtility.GetSprite(spriteName), typeof(Sprite));
        // }
    }
}
