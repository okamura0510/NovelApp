using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

/// <summary>
/// ノベルアプリ
/// </summary>
public class NovelApp : MonoBehaviour
{
    /// <summary>
    /// インスタンス
    /// </summary>
    public static NovelApp Instance { get; private set; }

    /// <summary>
    /// キャラ画像
    /// </summary>
    public Image Chara;
    /// <summary>
    /// メッセージテキスト
    /// </summary>
    public Text MessageText;

    /// <summary>
    /// コマンドデータ
    /// </summary>
    private Queue<CommandData> commandData = new Queue<CommandData>();
    /// <summary>
    /// コマンドリスト
    /// </summary>
    private List<Command> commands = new List<Command>();

    /// <summary>
    /// コマンドリスト
    /// </summary>
    public List<Command> Commands { get { return commands; } set { commands = value; } }

    /// <summary>
    /// 開始処理
    /// </summary>
    void Start()
    {
        Instance = this;

        // Excelからコマンド読み込み
        string path = Application.dataPath + "/!NovelApp/Editor/NovelScript.xls";
        using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        {
            IWorkbook book = new HSSFWorkbook(fs);
            ISheet sheet = book.GetSheetAt(0);
            for (int i = 1; i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                if (row == null) { continue; }

                // コマンド取得
                string id = row.GetCell(0).ToString();
                string param = row.GetCell(1).ToString();
                if (id == Finish.Id) { break; }

                // コマンドデータ保存
                var data = new CommandData() { Id = id, Param = param.Split(',') };
                commandData.Enqueue(data);
            }
        }
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // ウェイト中はコマンド実行停止
        if (Wait.IsRunning) { return; }

        // コマンド実行
        if (commandData.Count > 0)
        {
            var data    = commandData.Dequeue();
            var command = CreateCommand(data.Id, data.Param);
            command.Exec();
            commands.Add(command);
        }
    }

    /// <summary>
    /// コマンド作成
    /// </summary>
    /// <param name="id">ID</param>
    /// <param name="param">パラメータ</param>
    /// <returns>コマンド</returns>
    private Command CreateCommand(string id, string[] param)
    {
        // コマンド作成
        Command command = null;
        switch(id)
        {
            case Wait.Id:
                command = gameObject.AddComponent<Wait>();  
                break;
            case ShowChara.Id:
                command = gameObject.AddComponent<ShowChara>();
                break;
            case ShowMessage.Id:
                command = gameObject.AddComponent<ShowMessage>();
                break;
            case Move.Id:
                command = gameObject.AddComponent<Move>();
                break;
            case Rotate.Id:
                command = gameObject.AddComponent<Rotate>();
                break;
            case Punch.Id:
                command = gameObject.AddComponent<Punch>();
                break;
        }

        // コマンド初期化
        command.Init(id, param);
        return command;
    }
}