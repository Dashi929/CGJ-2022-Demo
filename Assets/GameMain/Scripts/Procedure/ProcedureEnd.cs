using System;
using System.Collections.Generic;
using GameFramework.Event;
using UnityEngine.Playables;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace GameMain
{
    public class ProcedureEnd : ProcedureBase
    {
        private PlayableDirector m_Director = null;
        public override bool UseNativeDialog
        {
            get
            {
                return false;
            }
        }

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);
            GameEntry.Event.Subscribe(OpenUIFormSuccessEventArgs.EventId,OnOpenUISuccess);
            procedureOwner.SetData<VarInt32>("NextSceneId", GameEntry.Config.GetInt("Scene.Menu"));
            ChangeState<ProcedureChangeScene>(procedureOwner);
            switch (GameEntry.Utlis.gameOverState)
            {
                case Utlis.GameOverState.Undefined:
                    break;
                case Utlis.GameOverState.End1:
                    GameEntry.Utlis.endID = GameEntry.UI.OpenUIForm(UIFormId.EndForm1);
                    break;
                case Utlis.GameOverState.End2:
                    GameEntry.Utlis.endID = GameEntry.UI.OpenUIForm(UIFormId.EndForm2);
                    break;
                case Utlis.GameOverState.End3:
                    GameEntry.Utlis.endID = GameEntry.UI.OpenUIForm(UIFormId.EndForm3);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            if (m_Director == null)
                return;
            if (m_Director.time <= m_Director.duration - 0.1f)
                return;
            procedureOwner.SetData<VarInt32>("NextSceneId", GameEntry.Config.GetInt("Scene.Menu"));
            ChangeState<ProcedureChangeScene>(procedureOwner);
        }
        
        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {

            base.OnLeave(procedureOwner, isShutdown);
            GameEntry.Event.Unsubscribe(OpenUIFormSuccessEventArgs.EventId,OnOpenUISuccess);
            
            if (m_Director == null)
                return;
            m_Director.Stop();
            m_Director.time = 0;
        }

        private void OnOpenUISuccess(object sender, GameEventArgs e)
        {
            OpenUIFormSuccessEventArgs ne = (OpenUIFormSuccessEventArgs)e;
            if (ne.UIForm.SerialId != GameEntry.Utlis.endID)
                return;
            m_Director = ne.UIForm.gameObject.GetComponent<PlayableDirector>();
            m_Director.Play();
        }
    }
}
