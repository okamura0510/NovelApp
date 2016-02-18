using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// パンチコマンド
/// </summary>
public class Punch : Command
{
    public const string Id = "Punch";

    /// <summary>
    /// キャラ画像
    /// </summary>
    private Image chara = null;
    /// <summary>
    /// ダメージ量
    /// </summary>
    private Vector3 amount = Vector3.zero;
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
        amount   = new Vector3(vec[0], vec[1], vec[2]);
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
        iTween.PunchScale(chara.gameObject, iTween.Hash(
            "amount",           amount,
            "time",             time,
            "delay",            delay,
            "looptype",         looptype,
            "easetype",         easetype,
            "islocal",          true,
            "oncomplete",       "OnPunchComplete",
            "oncompletetarget", gameObject));
    }

    /// <summary>
    /// パンチ終了
    /// </summary>
    void OnPunchComplete()
    {
        Finish();
    }
}