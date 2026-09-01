using System.Collections.Generic;
using GameFramework.Event;
using UnityEngine.Playables;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace GameMain
{
    public class ProcedureStoryline : ProcedureBase
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
            ChangeState<ProcedureGame>(procedureOwner);
            //GameEntry.Utlis.storylineID = GameEntry.UI.OpenUIForm(UIFormId.StorylineForm);
        }
        
        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            if (m_Director == null)
                return;
            if (m_Director.time <= m_Director.duration - 0.1f)
                return;
            ChangeState<ProcedureGame>(procedureOwner);
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
            if (ne.UIForm.SerialId != GameEntry.Utlis.storylineID)
                return;
            m_Director = ne.UIForm.gameObject.GetComponent<PlayableDirector>();
            m_Director.Play();
        }
    }
}
