//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using UnityEngine;
using UnityGameFramework.Runtime;

namespace GameMain
{
    public class MenuForm : UGuiForm
    {
        [SerializeField] private GameObject m_StartButton = null;
        [SerializeField] private GameObject m_DevelopButton = null;
        [SerializeField] private GameObject m_QuitButton = null;
        
        [Header("Develop")]
        [SerializeField] private GameObject m_DevelopPage = null;
        
        private ProcedureMenu m_ProcedureMenu = null;

        public void OnStartButtonClick()
        {
            m_ProcedureMenu.StartGame();
            gameObject.SetActive(false);
        }

        public void OnDevelopButtonClick()
        {
            m_StartButton.SetActive(false);
            m_DevelopButton.SetActive(false);
            m_QuitButton.SetActive(false);
            
            m_DevelopPage.SetActive(true);
        }
        
        public void OnQuitButtonClick()
        {
            UnityGameFramework.Runtime.GameEntry.Shutdown(ShutdownType.Quit);
            // GameEntry.UI.OpenDialog(new DialogParams()
            // {
            //     Mode = 2,
            //     Title = GameEntry.Localization.GetString("AskQuitGame.Title"),
            //     Message = GameEntry.Localization.GetString("AskQuitGame.Message"),
            //     OnClickConfirm = delegate (object userData) { UnityGameFramework.Runtime.GameEntry.Shutdown(ShutdownType.Quit); },
            // });
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnOpen(object userData)
#else
        protected internal override void OnOpen(object userData)
#endif
        {
            base.OnOpen(userData);

            m_ProcedureMenu = (ProcedureMenu)userData;
            if (m_ProcedureMenu == null)
            {
                Log.Warning("ProcedureMenu is invalid when open MenuForm.");
                return;
            }
            m_StartButton.SetActive(true);
            m_DevelopButton.SetActive(true);
            m_QuitButton.SetActive(true);
            
            m_DevelopPage.SetActive(false);
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnClose(bool isShutdown, object userData)
#else
        protected internal override void OnClose(bool isShutdown, object userData)
#endif
        {
            m_ProcedureMenu = null;

            base.OnClose(isShutdown, userData);
        }
    }
}
