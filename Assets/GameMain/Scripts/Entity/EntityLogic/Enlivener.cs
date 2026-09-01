using System;
using System.Collections;
using System.Collections.Generic;
using GameFramework.Event;
using UnityEngine;
using UnityGameFramework.Runtime;
using GameEntry = GameMain.GameEntry;

namespace GameMain
{
    public class Enlivener : EntityLogic
    {
        private enum EnlivenerState
        {
            Undefined,
            Level0,
            Level1,
            Level2,
            Level3,
        }

        private EnlivenerState m_EnlivenerState = EnlivenerState.Undefined;
        private EnlivenerData m_Data = null;
        private Animator m_Animator = null;

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            m_Data = userData as EnlivenerData;
            if (m_Data == null)
            {
                Log.Error("Entity data is invalid.");
                return;
            }
            GameEntry.Event.Subscribe(AddComboEventArgs.EventId,OnAddCombo);
            m_Animator = GetComponent<Animator>();
        }

        protected override void OnHide(bool isShutdown, object userData)
        {
            base.OnHide(isShutdown, userData);
            GameEntry.Event.Unsubscribe(AddComboEventArgs.EventId,OnAddCombo);
        }

        private void OnStateEnter()
        {
            switch (m_EnlivenerState)
            {
                case EnlivenerState.Undefined:
                    break;
                case EnlivenerState.Level0:
                    break;
                case EnlivenerState.Level1:
                    
                    break;
                case EnlivenerState.Level2:
                    break;
                case EnlivenerState.Level3:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnAddCombo(object sender, GameEventArgs e)
        {
            AddComboEventArgs ne = (AddComboEventArgs)e;
            switch (m_EnlivenerState)
            {
                case EnlivenerState.Undefined:
                    break;
                case EnlivenerState.Level0:
                    if (ne.ComboNum > 3)
                    {
                        m_EnlivenerState = EnlivenerState.Level1;
                        OnStateEnter();
                    }
                    break;
                case EnlivenerState.Level1:
                    if (ne.ComboNum > 7)
                    {
                        m_EnlivenerState = EnlivenerState.Level2;
                        OnStateEnter();
                    }
                    break;
                case EnlivenerState.Level2:
                    if (ne.ComboNum > 10)
                    {
                        m_EnlivenerState = EnlivenerState.Level3;
                        OnStateEnter();
                    }
                    break;
                case EnlivenerState.Level3:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}