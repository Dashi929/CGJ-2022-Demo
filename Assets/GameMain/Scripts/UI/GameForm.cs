
using GameFramework.Event;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace GameMain
{
    public class GameForm : UGuiForm
    {
            [SerializeField] private Text mCountDownNum = null;
            [SerializeField] private Text mComboNum = null;
            [SerializeField] private Text mScoreNum = null;

            private float m_TargetTime = 60;
            private float m_CurrentTime = 0;
            private int m_ComboNum = 0;
            private int m_ScoreNum = 0;

            private bool m_GameOver = false;
#if UNITY_2017_3_OR_NEWER
        protected override void OnInit(object userData)
#else
        protected internal override void OnInit(object userData)
#endif
        {
            base.OnInit(userData);
            
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnOpen(object userData)
#else
        protected internal override void OnOpen(object userData)
#endif
        {
            base.OnOpen(userData);
            
            GameEntry.Event.Subscribe(AddComboEventArgs.EventId,OnAddCombo);
            GameEntry.Event.Subscribe(AddScoreEventArgs.EventId,OnAddScore);
            
            m_CurrentTime = m_TargetTime;
            m_ComboNum = 0;
            m_ScoreNum = 0;
            
            mCountDownNum.text = m_TargetTime.ToString("#0.00");
            mComboNum.text = 0.ToString();
            mScoreNum.text = 0.ToString();
            m_GameOver = false;
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnClose(bool isShutdown, object userData)
#else
        protected internal override void OnClose(bool isShutdown, object userData)
#endif
        {
            base.OnClose(isShutdown, userData);
            GameEntry.Event.Unsubscribe(AddComboEventArgs.EventId,OnAddCombo);
            GameEntry.Event.Unsubscribe(AddScoreEventArgs.EventId,OnAddScore);
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
#else
        protected internal override void OnUpdate(float elapseSeconds, float realElapseSeconds)
#endif
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
            if (m_GameOver)
                    return;
            if (m_CurrentTime > 0)
            {
                    m_CurrentTime -= Time.deltaTime;
                    mCountDownNum.text = m_CurrentTime.ToString();
            }
            else
            {
                    m_CurrentTime = 0;
                    mCountDownNum.text = Change(m_CurrentTime).ToString();
                    GameEntry.Event.FireNow(this,TimeOutEventArgs.Create());
                    m_GameOver = true;
            }
        }

        private void OnAddCombo(object sender,GameEventArgs e)
        {
                AddComboEventArgs ne = (AddComboEventArgs)e;
                m_ComboNum = ne.ComboNum;
                mComboNum.text = m_ComboNum.ToString();
        }

        private void OnAddScore(object sender,GameEventArgs e)
        { 
                AddScoreEventArgs ne = (AddScoreEventArgs)e;
                m_ScoreNum = ne.ScoreNum;
                mScoreNum.text = m_ScoreNum.ToString();
        }

        private float Change(float input)
        {
                float i = input;
                int j = (int)(i * 100);
                i = (float)j / 100;
                Debug.Log(string.Format("{0}", i));
                return i;
        }
    }
}
