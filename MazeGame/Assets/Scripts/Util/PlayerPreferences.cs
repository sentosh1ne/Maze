using UnityEngine;
using System.Collections;
using System.IO;
public class PlayerPreferences : MonoBehaviour {

    public static string playerName = "Default";
    public static string recordsSaveLocation;
    // Use this for initialization
    void Awake () {
        DontDestroyOnLoad(transform.gameObject);
        recordsSaveLocation = Path.Combine(Application.dataPath, "records.xml");
    }

}
