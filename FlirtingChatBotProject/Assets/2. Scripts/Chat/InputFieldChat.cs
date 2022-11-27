using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldChat : MonoBehaviour
{
    public InputField inputField;
    public VisualChatManager visualChatManager;
    public Button inputButton;
    private bool inputAble = true;

    // Update is called once per frame
    void Update()
    {
        if (inputAble)
        {
            inputField.ActivateInputField();
            if (inputField.text != "")
            {
                inputButton.GetComponent<Image>().color = new Color(1.0f, 0.89f, 0.0f, 1.0f);
            }
        }
        else
        {
            inputButton.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }

        // 2. if text arrived, then visualize the text
    }

    public void InputDone()
    {
        // 1. call chatbot api bot text from given inputField.text

        visualChatManager.VisualizeChat(2, inputField.text, "유붕이", null); 
        inputField.text = "";

        inputField.DeactivateInputField();
        inputAble = false;
        StartCoroutine(WaitForIt()); // input 가능 여부 flag 처리
    }

    IEnumerator WaitForIt()
    {
        yield return new WaitForSeconds(0.5f);
        inputAble = true;
    }
}
