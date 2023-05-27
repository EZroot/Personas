using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;

public class AuthenticateAI : MonoBehaviour
{
    public TMP_InputField _inputField;
    private bool isRequestInProgress = false;

   private string apiKey = "token";
    private string endpoint = "https://api.openai.com/v1/chat/completions";
    private string model = "gpt-3.5-turbo";

    // Start is called before the first frame update
    private void Start()
    {
        _inputField.onEndEdit.AddListener(OnEndEdit);
    }

    private void OnEndEdit(string text)
    {
        // Check if the entered text contains a line break character ("\n")
        // Enter key was pressed
        Debug.Log("Enter key pressed!");
        var prompt = _inputField.text;
        // Clear the input field or process the entered text
        _inputField.text = string.Empty;

        // HandleEnterKey();
        StartCoroutine(SendChatRequest(prompt));

    }



     private System.Collections.IEnumerator SendChatRequest(string prompt)
    {
        string url = $"{endpoint}?model={UnityWebRequest.EscapeURL(model)}";
        
        var requestData = new
        {
            messages = new[]
            {
                new
                {
                    role = "system",
                    content = prompt
                }
            }
        };

        string requestDataJson = JsonUtility.ToJson(requestData);
        
        var www = new UnityWebRequest(url, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(requestDataJson);
        www.uploadHandler = new UploadHandlerRaw(jsonToSend);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("Authorization", $"Bearer {apiKey}");

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string response = www.downloadHandler.text;
            Debug.Log(response);
        }
        else
        {
            Debug.LogError("Request failed: " + www.error);
        }
    }
}

