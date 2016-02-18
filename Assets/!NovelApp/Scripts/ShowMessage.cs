using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// メッセージ表示コマンド
/// </summary>
public class ShowMessage : Command
{
    public const string Id = "ShowMessage";

    /// <summary>
    /// メッセージテキスト
    /// </summary>
    private Text messageText = null;
    /// <summary>
    /// メッセージ
    /// </summary>
    private string message = "";

    /// <summary>
    /// 初期化
    /// </summary>
    public override void Init(string id, string[] p)
    {
        base.Init(id, p);

        messageText = NovelApp.Instance.MessageText;
        message     = param[0];
    }

    /// <summary>
    /// 実行
    /// </summary>
    public override void Exec()
    {
        messageText.text = message.Replace("<br>", "\n");
        Finish();
    }
}