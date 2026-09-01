using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;

public class BuildTools : MonoBehaviour {

	private static string GameName = PlayerSettings.productName + "_" + DateTime.Now.ToString("yyyyMMddHHmm");
 
    private static string OutPutPath = System.IO.Directory.GetCurrentDirectory() + "/BuildTools/OutPut/";
    //private static string ApkPath = string.Format("{0}{1}.apk", OutPutPath, GameName);
    //private static string ApkAndroidPath = string.Format("{0}{1}", OutPutPath, GameName+ "Android");
    //private static string EXEPath = string.Format("{0}{1}.exe", OutPutPath + GameName+ "_PC" + "/",GameName);
    //private static string AndroidProjectPath = string.Format("{0}{1}_AndroidProject", OutPutPath, GameName);


    [MenuItem("Tools/打包/打包APK")]
    public static void BuildAPK()
    {
        string versionName = "1.0.0";
		int versionCode = 1;
        string productName = "";
        string WorkSpace = "";
		bool isDevelopment = false;
 
        Debug.Log("------------- 接收命令行参数 -------------");
        List<string> commondList = new List<string>();
        foreach (string arg in System.Environment.GetCommandLineArgs())
        {
            Debug.Log("命令行传递过来参数：" + arg);
            commondList.Add(arg);
        }
        try
        {
            Debug.Log("命令行传递过来参数数量：" + commondList.Count);
            versionName = commondList[commondList.Count - 3];
            productName = commondList[commondList.Count - 2];
            versionCode = int.Parse(commondList[commondList.Count - 1]);
            //versionCode = (int)commondList[commondList.Count - 1];
        }
        catch (Exception e)
        {
            print(e.Message);
        }

        string ApkPath = string.Format("{0}{1}.apk", OutPutPath, productName);

        Debug.Log("------------- 更新资源 -------------");
        //OneKeyRefreshSource.Create();
 
        Debug.Log("------------- 开始 BuildAPK -------------");
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);
        PlayerSettings.bundleVersion = versionName;
        PlayerSettings.Android.bundleVersionCode = versionCode;

        //打包安卓工程为true
        EditorUserBuildSettings.exportAsGoogleAndroidProject = false;

        BuildOptions buildOption = BuildOptions.None;
        if (isDevelopment)
            buildOption |= BuildOptions.Development;
        else
            buildOption &= BuildOptions.Development;
        //PlayerSettings.Android.keystorePass = "00000000";       // 密钥密码
        //PlayerSettings.Android.keyaliasName = "test.keystore";    // 密钥别名
        //PlayerSettings.Android.keyaliasPass = "00000000";
        BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, ApkPath, BuildTarget.Android, buildOption);
        Debug.Log("------------- 结束 BuildAPK -------------");
        Debug.Log("Build目录：" + ApkPath);
        //Application.OpenURL(OutPutPath);
    }
    [MenuItem("Tools/打包/打包Android")]
    public static void BuildAndroid()
    {
        string versionName = "1.0.0";
        int versionCode = 1;
        string productName = "";
        string WorkSpace = "";
        bool isDevelopment = false;

        Debug.Log("------------- 接收命令行参数 -------------");
        List<string> commondList = new List<string>();
        foreach (string arg in System.Environment.GetCommandLineArgs())
        {
            Debug.Log("命令行传递过来参数：" + arg);
            commondList.Add(arg);
        }
        try
        {
            Debug.Log("命令行传递过来参数数量：" + commondList.Count);
            versionName = commondList[commondList.Count - 3];
            productName = commondList[commondList.Count - 2];
            //versionCode = (int)commondList[commondList.Count - 1];
            versionCode = int.Parse(commondList[commondList.Count - 1]);
        }
        catch (Exception e)
        {
            print(e.Message);
        }


        string AndroidPath = string.Format("{0}{1}", OutPutPath, productName);
        Debug.Log("------------- 更新资源 -------------");
        //OneKeyRefreshSource.Create();

        Debug.Log("------------- 开始 BuildApkAndroid -------------");
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);
        PlayerSettings.bundleVersion = versionName;
        PlayerSettings.Android.bundleVersionCode = versionCode;


        //打包安卓工程为true
        EditorUserBuildSettings.exportAsGoogleAndroidProject = true;

      
        BuildOptions buildOption = BuildOptions.None;
        if (isDevelopment)
            buildOption |= BuildOptions.Development;
        else
            buildOption &= BuildOptions.Development;
        //PlayerSettings.Android.keystorePass = "00000000";       // 密钥密码
        //PlayerSettings.Android.keyaliasName = "test.keystore";    // 密钥别名
        //PlayerSettings.Android.keyaliasPass = "00000000";
        BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, AndroidPath, BuildTarget.Android, buildOption);
        Debug.Log("------------- 结束 BuildAndroid -------------");
        Debug.Log("Build目录：" + AndroidPath);
        //Application.OpenURL(OutPutPath);
    }

    [MenuItem("Tools/打包/打包EXE")]
    public static void BuildEXE()
    {
        string versionName = "1.0.0";
        int versionCode = 1;
        string productName = "";
        string WorkSpace = "";
        bool isDevelopment = false;

        Debug.Log("------------- 接收命令行参数 -------------");
        List<string> commondList = new List<string>();
        foreach (string arg in System.Environment.GetCommandLineArgs())
        {
            Debug.Log("命令行传递过来参数：" + arg);
            commondList.Add(arg);
        }
        try
        {
            Debug.Log("命令行传递过来参数数量：" + commondList.Count);
            versionName = commondList[commondList.Count - 3];
            productName = commondList[commondList.Count - 2];
            versionCode = int.Parse(commondList[commondList.Count - 1]);
            //versionCode = (int)commondList[commondList.Count - 1];
        }
        catch (Exception e)
        {
            print(e.Message);
        }

        string EXEPath = string.Format("{0}{1}.exe", OutPutPath + productName + "/", productName);
        Debug.Log("------------- 更新资源 -------------");
        //OneKeyRefreshSource.Create();

        Debug.Log("------------- 开始 BuildEXE -------------");
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows);

        PlayerSettings.bundleVersion = versionName;
        PlayerSettings.Android.bundleVersionCode = versionCode;


        BuildOptions buildOption = BuildOptions.None;
        if (isDevelopment)
            buildOption |= BuildOptions.Development;
        else
            buildOption &= BuildOptions.Development;
        //PlayerSettings.Android.keystorePass = "00000000";       // 密钥密码
        //PlayerSettings.Android.keyaliasName = "test.keystore";    // 密钥别名
        //PlayerSettings.Android.keyaliasPass = "00000000";
        BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, EXEPath, BuildTarget.StandaloneWindows64, buildOption);
        Debug.Log("------------- 结束 BuildEXE -------------");
        Debug.Log("Build目录：" + EXEPath);
        //Application.OpenURL(OutPutPath);
    }
}
