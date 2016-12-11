using UnityEngine;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using System;

/// <summary>
/// Manages the table with records
/// </summary>
public class RecordsTableManager : MonoBehaviour
{
    GameObject playerScoreList;
    public GameObject playerRow;

    //Values of each column for the entry in the table
    Text[] textValues;

    void Start()
    {
        playerScoreList = GameObject.Find("PlayerScoreList");
        List<Record> records = new List<Record>();
        FillScoreTable(records);
    }

    // Populates record row with info
    private void setTextForChildren(Record record, Text[] textValues)
    {
        textValues[0].text = record.PlayerName;
        textValues[1].text = record.CoinCount.ToString();
        textValues[2].text = record.TimeElapsed.ToString();
        textValues[3].text = record.Date.ToString();
        textValues[4].text = record.EndReason;
    }

    //Adds new record to the table list
    private void AppendToList(Record record)
    {
        GameObject recordRow = Instantiate(playerRow);
        recordRow.transform.parent = playerScoreList.transform;
        textValues = recordRow.GetComponentsInChildren<Text>();
        setTextForChildren(record, textValues);
    }

    private void FillScoreTable(List<Record> records)
    {
        try
        {
            records = RecordsSaver.Load(PlayerPreferences.recordsSaveLocation).Records;
        }
        catch (FileNotFoundException e)
        {
            Debug.LogException(e);
            RecordsSaver recordsSaver = new RecordsSaver();
            recordsSaver.Save(PlayerPreferences.recordsSaveLocation);
            return;
        }

        records.Sort((x, y) => -DateTime.Compare(x.Date, y.Date));

        foreach (var r in records)
        {
            AppendToList(r);
        }
    }

}
