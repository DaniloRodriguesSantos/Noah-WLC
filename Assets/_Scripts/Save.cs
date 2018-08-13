using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;


public class Save : MonoBehaviour
{

    void Start()
    {
        Savecsv();
    }

    void Savecsv()
    {
        //string filePath = @"/Saved_data.csv";
        string filePath = Application.dataPath + "/TesteLegal" + "/output" + "/Saved_data.csv";
        if (!Directory.Exists(filePath))
        {
            Directory.CreateDirectory(filePath);
        }
        string delimiter = ",";

        string[][] output = new string[][]{
             new string[]{"Col 1 Row 1", "Col 2 Row 1", "Col 3 Row 1"},
             new string[]{"Col1 Row 2", "Col2 Row 2", "Col3 Row 2"}
         };
        int length = output.GetLength(0);
        StringBuilder sb = new StringBuilder();
        for (int index = 0; index < length; index++)
            sb.AppendLine(string.Join(delimiter, output[index]));

        File.WriteAllText(filePath, sb.ToString());
    }
}