using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// ウェイトコマンド
/// </summary>
public class Wait : Command
{
    public const string Id = "Wait";

    /// <summary>
    /// ウェイト実行中フラグ
    /// </summary>
    public static bool IsRunning
    {
        get
        {
            // コマンド内にウェイトコマンドが存在するか？
            foreach (var command in NovelApp.Instance.Commands)
            {
                if (command is Wait) { return true; }
            }
            return false;
        }
    }

    /// <summary>
    /// ウェイト時間
    /// </summary>
    private float time = 0f;

    /// <summary>
    /// 初期化
    /// </summary>
    public override void Init(string id, string[] p)
    {
        base.Init(id, p);

        time = float.Parse(param[0]);
    }

    /// <summary>
    /// 実行
    /// </summary>
    public override void Exec()
    {
        // 一定時間なにもしない(空回し)
        iTween.ValueTo(gameObject, iTween.Hash(
            "from",             0f,
            "to",               1f,
            "time",             time,
            "onupdate",         "OnWaitUpdate",
            "oncomplete",       "OnWaitComplete",
            "oncompletetarget", gameObject));
    }

    /// <summary>
    /// ウェイト更新
    /// </summary>
    /// <param name="value">変化値</param>
    void OnWaitUpdate(float value) { }

    /// <summary>
    /// ウェイト終了
    /// </summary>
    void OnWaitComplete()
    {
        Finish();
    }
}