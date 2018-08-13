using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TrialLogger : MonoBehaviour {

    public int currentTrialNumber = 0;    
    List<string> header;
    [HideInInspector]
    public Dictionary<string, string> trial;
    [HideInInspector]
    public string outputFolder;

    bool trialStarted = false;
    string ppid;
    string dataOutputPath;
    List<string> output;
    public List<string> fileNameList;
    private string fileName = "Coleta de dados";
    string path;
    private float time;
    private int testenumber;
    private List<string[]> rowData = new List<string[]>();
    // Use this for initialization
    void Awake () {
        outputFolder = Application.dataPath + "/Dados" + "/output";
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }
    }

    private void Start()
    {
        //string path = Path.Combine(Application.dataPath, "/SouLegal" + "/output");
        //path = Path.Combine(path, "NomeLegal.csv");
        path = Application.dataPath + "/Dados" + "/output";
        fileNameList.Add(fileName);
        //for(int i =0; i < fileNameList.Count; i++)
        //{
        //    Debug.Log(path + "/" + fileNameList[i] + ".csv");
        //    Debug.Log(System.IO.File.Exists(path + "/" + fileNameList[i] + ".csv"));
        //}

        //Debug.Log(path);
        Initialize();
        
    }

    // Update is called once per frame
    void Update () {
        time += 1 * Time.deltaTime;
    }

    public void Initialize(/*string participantID, *//*List<string> customHeader*/)
    {
        //ppid = participantID;
        header = new List<string>();
        InitHeader();
        InitDict();
        output = new List<string>();
        output.Add(string.Join(";", header.ToArray()));
        
        for(int i = 0; i < fileNameList.Count; i++)
        {
            if(System.IO.File.Exists(path + "/" + fileNameList[i] + ".csv"))
            {
                testenumber = fileNameList.Count + 1;
                fileName = fileName + testenumber.ToString();
                dataOutputPath = outputFolder + "/" + fileName + ".csv";
                fileNameList.Add(fileName);
            } else
            {
                dataOutputPath = outputFolder + "/" + fileName + ".csv";
            } 
        }

        //if(fileNameList.Count == 0)
        //{
        //    dataOutputPath = outputFolder + "/" + fileName + ".csv";
        //}
        //if (System.IO.File.Exists(path))
        //{
        //    dataOutputPath = outputFolder + "/" + fileName += 1.ToString + ".csv";
        //}
        //else
        //{
            
        //}
        //dataOutputPath = outputFolder + "/" + participantID + ".csv";
    }


    private void InitHeader()
    {
        //header.Insert(0, "number");
        //header.Insert(1, "ppid");
        //header.Insert(2, "start_time");
        //header.Insert(3, "end_time");

        header.Insert(0, "Inicio");
        header.Insert(1, "Termino");
        header.Insert(2, "Local");
        header.Insert(3, "Plataforma de escolha");
        header.Insert(4, "Escolha");
    }

    private void InitDict()
    {
        trial = new Dictionary<string, string>();
        foreach (string value in header)
        {
            trial.Add(value, "");
        }
    }

    public void StartTrial()
    {
        trialStarted = true;
        //currentTrialNumber += 1;
        InitDict();
        //trial["number"] = currentTrialNumber.ToString();
        //trial["ppid"] = ppid;
        time = 0;
        trial["Inicio"] = time.ToString();
        //trial["start_time"] = Time.time.ToString();
    }

    public void EndTrial()
    {
        if (output != null && dataOutputPath != null)
        {
            if (trialStarted)
            {
                //trial["end_time"] = Time.time.ToString();
                trial["Termino"] = time.ToString();
                output.Add(FormatTrialData());
                trialStarted = false;

            }
            //else Debug.LogError("Error ending trial - Trial wasn't started properly");

        }
        //else Debug.LogError("Error ending trial - TrialLogger was not initialsed properly");
     

    }

    private string FormatTrialData()
    {
        List<string> rowData = new List<string>();
        foreach (string value in header)
        {
            rowData.Add(trial[value]);

        }
        return string.Join(";", rowData.ToArray());
    }

    private void OnApplicationQuit()
    {

        if (output != null && dataOutputPath != null)
        {
            File.WriteAllLines(dataOutputPath, output.ToArray());
            Debug.Log(string.Format("Saved data to {0}.", dataOutputPath));
        }
        //else Debug.LogError("Error saving data - TrialLogger was not initialsed properly");
        
    }
}
