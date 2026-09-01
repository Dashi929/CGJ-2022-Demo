using System;
using System.Collections.Generic;
using GameFramework.DataTable;
using GameFramework.Event;
using UnityEngine;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace GameMain
{
    public class ProcedureGame : ProcedureBase
    {
        public override bool UseNativeDialog
        {
            get
            {
                return false;
            }
        }
        
        private enum GameState
        {
            Undefined,
            Game,
            Pause,
            GameOver,
            GameClear
        }

        private GameState m_GameState = GameState.Undefined;

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);
            GameEntry.Event.Subscribe(GameOverEventArgs.EventId,GameOver);
            GameEntry.Event.Subscribe(GameContinueEventArgs.EventId,GameContinue);
            GameEntry.Event.Subscribe(GameClearEventArgs.EventId,GameClear);

            GameEntry.UI.OpenUIForm(UIFormId.GameForm);
            GameEntry.Sound.StopAllLoadedSounds();
            GameEntry.Sound.PlayMusic(2);
            
            m_GameState = GameState.Game;
            OnGameStateEnter();
        }

        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            switch (m_GameState)
            {
                case GameState.Undefined:
                    break;
                case GameState.Game:
                    if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        m_GameState = GameState.Pause;
                        OnGameStateEnter();
                    }
                    break;
                case GameState.Pause:
                    if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        m_GameState = GameState.Game;
                        OnGameStateEnter();
                    }
                    break;
                case GameState.GameOver:
                    procedureOwner.SetData<VarInt32>("NextSceneId", GameEntry.Config.GetInt("Scene.Menu"));
                    ChangeState<ProcedureChangeScene>(procedureOwner);
                    m_GameState = GameState.Undefined;
                    break;
                case GameState.GameClear:
                    ChangeState<ProcedureEnd>(procedureOwner);
                    m_GameState = GameState.Undefined;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);
            GameEntry.Event.Unsubscribe(GameOverEventArgs.EventId,GameOver);
            GameEntry.Event.Unsubscribe(GameContinueEventArgs.EventId,GameContinue);
            GameEntry.Event.Unsubscribe(GameClearEventArgs.EventId,GameClear);
            GameEntry.UI.CloseAllLoadedUIForms();
        }

        private void OnGameStateEnter()
        {
            switch (m_GameState)
            {
                case GameState.Undefined:
                    break;
                case GameState.Game:
                    GameEntry.Base.GameSpeed = 1.0f;
                    if (GameEntry.Utlis.pauseID != null) 
                        GameEntry.UI.CloseUIForm((int)GameEntry.Utlis.pauseID);
                    break;
                case GameState.Pause:
                    GameEntry.Base.GameSpeed = 0f;
                    GameEntry.Utlis.pauseID = GameEntry.UI.OpenUIForm(UIFormId.PauseForm);
                    break;
                case GameState.GameOver:
                    break;
                case GameState.GameClear:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private void GameClear(object sender, GameEventArgs e)
        {
            GameClearEventArgs ne = (GameClearEventArgs)e;
            
            IDataTable<DREnd> dtEnds = GameEntry.DataTable.GetDataTable<DREnd>();

            DREnd drEnd1 = dtEnds.GetDataRow(0);
            DREnd drEnd2 = dtEnds.GetDataRow(1);
            DREnd drEnd3 = dtEnds.GetDataRow(2);

            if (ne.CurrentFloorNum >= drEnd3.TargetFloorNum)
            {
                GameEntry.Utlis.gameOverState = Utlis.GameOverState.End3;
            }
            else if(ne.CurrentFloorNum >= drEnd2.TargetFloorNum)
            {
                GameEntry.Utlis.gameOverState = Utlis.GameOverState.End2;
            }
            else if(ne.CurrentFloorNum >= drEnd1.TargetFloorNum)
            {
                GameEntry.Utlis.gameOverState = Utlis.GameOverState.End1;
            }
            else
            {
                
            }
            m_GameState = GameState.GameClear;
            OnGameStateEnter();
        }
        
        private void GameContinue(object sender, GameEventArgs e)
        {
            m_GameState = GameState.Game;
            OnGameStateEnter();
        }

        private void GameOver(object sender, GameEventArgs e)
        {
            m_GameState = GameState.GameOver;
            OnGameStateEnter();
        }
    }
}
