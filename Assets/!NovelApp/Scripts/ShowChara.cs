using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// キャラ表示コマンド
/// </summary>
public class ShowChara : Command
{
    public const string Id = "ShowChara";

    /// <summary>
    /// キャラ画像
    /// </summary>
    private Image chara = null;
    /// <summary>
    /// ウェイト時間
    /// </summary>
    private string fileName = "";
    /// <summary>
    /// 表示タイプ。0：瞬時、1：フェイドイン
    /// </summary>
    private int showType = 0;

    /// <summary>
    /// 初期化
    /// </summary>
    public override void Init(string id, string[] p)
    {
        base.Init(id, p);

        chara    = NovelApp.Instance.Chara;
        fileName = param[0];
        showType = int.Parse(param[1]);
    }

    /// <summary>
    /// 実行
    /// </summary>
    public override void Exec()
    {
        // キャラ画像読み込み
        NovelApp.Instance.Chara.sprite = Resources.Load<Sprite>(fileName);

        // 表示
        if (showType == 0)
        {
            // 瞬時(終了)
            OnShowCharaComplete();
        }
        else
        {
            // フェイドイン
            chara.color = new Color(chara.color.r, chara.color.g, chara.color.b, 0f);
            iTween.ValueTo(gameObject, iTween.Hash(
                "from",             0f,
                "to",               1f,
                "time",             1f,
                "onupdate",         "OnShowCharaUpdate",
                "oncomplete",       "OnShowCharaComplete",
                "oncompletetarget", gameObject));
        }
    }

    /// <summary>
    /// 表示更新
    /// </summary>
    /// <param name="value">変化値</param>
    void OnShowCharaUpdate(float value)
    {
        // アルファ変化
        chara.color = new Color(chara.color.r, chara.color.g, chara.color.b, value);
    }

    /// <summary>
    /// 表示終了
    /// </summary>
    void OnShowCharaComplete()
    {
        Finish();
    }
}