using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// 回転コマンド
/// </summary>
public class Rotate : Command
{
    public const string Id = "Rotate";

    /// <summary>
    /// キャラ画像
    /// </summary>
    private Image chara = null;
    /// <summary>
    /// 角度
    /// </summary>
    private Vector3 rotation = Vector3.zero;
    /// <summary>
    /// 時間
    /// </summary>
    private float time = 0f;
    /// <summary>
    /// 初回ウェイト
    /// </summary>
    private float delay = 0f;
    /// <summary>
    /// ループタイプ
    /// </summary>
    private iTween.LoopType looptype = iTween.LoopType.none;
    /// <summary>
    /// イースタイプ
    /// </summary>
    private iTween.EaseType easetype = iTween.EaseType.linear;

    /// <summary>
    /// 初期化
    /// </summary>
    public override void Init(string id, string[] p)
    {
        base.Init(id, p);

        var vec  = param[0].Split('/').Select((s) => float.Parse(s)).ToArray();
        chara    = NovelApp.Instance.Chara;
        rotation = new Vector3(vec[0], vec[1], vec[2]);
        time     = float.Parse(param[1]);
        delay    = float.Parse(param[2]);
        looptype = (iTween.LoopType)Enum.Parse(typeof(iTween.LoopType), param[3]);
        easetype = (iTween.EaseType)Enum.Parse(typeof(iTween.EaseType), param[4]);
    }

    /// <summary>
    /// 実行
    /// </summary>
    public override void Exec()
    {
        iTween.RotateTo(chara.gameObject, iTween.Hash(
            "rotation",         rotation,
            "time",             time,
            "delay",            delay,
            "looptype",         looptype,
            "easetype",         easetype,
            "islocal",          true,
            "oncomplete",       "OnRotateComplete",
            "oncompletetarget", gameObject));
    }

    /// <summary>
    /// 回転終了
    /// </summary>
    void OnRotateComplete()
    {
        Finish();
    }
}