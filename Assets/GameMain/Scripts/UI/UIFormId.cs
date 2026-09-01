//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameMain
{
    /// <summary>
    /// 界面编号。
    /// </summary>
    public enum UIFormId : byte
    {
        Undefined = 0,

        /// <summary>
        /// 弹出框。
        /// </summary>
        DialogForm = 1,

        /// <summary>
        /// 主菜单。
        /// </summary>
        MenuForm = 100,

        /// <summary>
        /// Timeline1。
        /// </summary>
        StorylineForm = 101,

        /// <summary>
        /// Timeline2。
        /// </summary>
        EndForm1 = 102,
        
        /// <summary>
        /// Timeline3
        /// </summary>
        EndForm2 = 103,
        
        /// <summary>
        /// Timeline4
        /// </summary>
        EndForm3 = 104,
        
        /// <summary>
        /// 暂停菜单
        /// </summary>
        PauseForm = 105,
        
        /// <summary>
        /// 游戏菜单
        /// </summary>
        GameForm = 106,
    }
}
