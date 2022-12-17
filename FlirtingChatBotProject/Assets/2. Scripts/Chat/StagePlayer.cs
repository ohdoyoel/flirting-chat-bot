using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class StagePlayer : MonoBehaviour
{
    public string stage_path;
    public VisualChatManager visualChatManager;

    
    List<List<string>> flag_cont = new List<List<string>>();
    // Start is called before the first frame update
    void Start()
    {
        StreamReader sr = new StreamReader(Application.dataPath + stage_path);

        bool endOfFile = false;
        while (!endOfFile)
        {
            string data_String = sr.ReadLine();
            if(data_String == null)
            {
                endOfFile = true;
                break;
            }
            var data_values = data_String.Split(',');
            var tmp = new List<string>();
            tmp.Add(data_values[0]);
            tmp.Add(data_values[1]);
            tmp.Add(data_values[2]);
            flag_cont.Add(tmp);
        }
        // for (int i = 0; i < flag_cont.Count; i++)
        // {
        //     Debug.Log(flag_cont[i][0] + " " + flag_cont[i][1] + " " + flag_cont[i][2]);
        // }
        
        StartCoroutine(ProcessCSV());
    }

    int i = 0;
    void Update()
    {
        if (i >= flag_cont.Count)
        {
            StopCoroutine(ProcessCSV());
            i = 0;
        }
    }

    IEnumerator ProcessCSV()
    {
        
        while (i < flag_cont.Count)
        {
            Debug.Log(flag_cont[i][0] + " " + flag_cont[i][1] + " " + flag_cont[i][2]);
            if (flag_cont[i][0] == "0")
            {
                visualChatManager.VisualizeChat(0, flag_cont[i][1], "유붕이", null);
            }
            else if (flag_cont[i][0] == "1")
            {
                visualChatManager.VisualizeChat(1, flag_cont[i][1], "유순이", Resources.Load<Texture2D>("Assets/1. Resources/Images/profile_img.png"));
            }
            else if (flag_cont[i][0] == "2")
            {
                visualChatManager.VisualizeChat(2, flag_cont[i][1], "유붕이", null);
            }
            else if (flag_cont[i][0] == "3")
            {
                visualChatManager.VisualizeChat(2, flag_cont[i][1], "유붕이", null);
            }
            i++;
            yield return new WaitForSeconds(3.0f);
        }
    }
}
