using UnityEngine;
using System;
using System.IO;
using System.Xml.Serialization;

/// <summary>
/// Representation of game result entry
/// </summary>
[Serializable]
public class Record {

    public string PlayerName { get; set; }
    public int CoinCount { get; set; }
    public float TimeElapsed { get; set; }
    public DateTime Date{get;set;}
    public string EndReason { get; set; }

    public Record(string playerName, int coinCount, float timeElapsed, DateTime date, string endReason)
    {
        this.PlayerName = playerName;
        this.CoinCount = coinCount;
        this.TimeElapsed = timeElapsed;
        this.Date = date;
        this.EndReason = endReason;
    }

    public Record()
    {

    }
	
}
