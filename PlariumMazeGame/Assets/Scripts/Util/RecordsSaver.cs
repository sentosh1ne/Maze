
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;

/// <summary>
/// /Manages savings and loadings of game results
/// </summary>
public class RecordsSaver
{
    public List<Record> Records { get; set; }

    public void Save(string path)
    {
        var serializer = new XmlSerializer(typeof(RecordsSaver));
        using (var stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, this);
        }
    }

    public static RecordsSaver Load(string path)
    {
        var serializer = new XmlSerializer(typeof(RecordsSaver));
        using (var stream = new FileStream(path, FileMode.Open))
        {
            return serializer.Deserialize(stream) as RecordsSaver;
        }
    }

    /// <summary>
    ///  Creates xml file if it does not exist and puts the record
    /// </summary>
    /// <param name="r"></param>
    public void CreateNewRecordSource(Record r)
    {
        RecordsSaver recordSaver = new RecordsSaver();
        recordSaver.Records = new List<Record>();
        recordSaver.Records.Add(r);
        recordSaver.Save(PlayerPreferences.recordsSaveLocation);
    }

    /// <summary>
    /// Append record to the end of a xml file if it exists
    /// </summary>
    /// <param name="r"></param>
    public void AppendRecord(Record r)
    {
        var collection = RecordsSaver.Load(Path.Combine(Application.dataPath, "records.xml"));
        collection.Records.Add(r);
        collection.Save(Path.Combine(Application.dataPath, "records.xml"));
    }

}
