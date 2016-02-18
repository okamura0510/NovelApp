using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// コマンド基底クラス
/// </summary>
public class Command : MonoBehaviour
{
    /// <summary>
    /// ID
    /// </summary>
    public string id = "";
    /// <summary>
    /// パラメータ
    /// </summary>
    public string[] param = null;

    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="id">ID</param>
    /// <param name="param">パラメータ</param>
    public virtual void Init(string id, string[] param)
    {
        this.id    = id;
        this.param = param;
    }
    
    /// <summary>
    /// 実行
    /// </summary>
    public virtual void Exec() { }

    /// <summary>
    /// 終了
    /// </summary>
    public virtual void Finish()
    {
        NovelApp.Instance.Commands.Remove(this);
        Destroy(this);
    }
}