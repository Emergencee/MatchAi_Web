using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class OpenAIApiTest : MonoBehaviour
{
    // OpenAI API Ű
    string apiKey = "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";

    // �׽�Ʈ�� ����� �Է�
    string userInput = "Hello, GPT!";

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GenerateConversation(userInput));
    }

    IEnumerator GenerateConversation(string prompt)
    {
        // ��ȭ ���� ��û ������
        string url = "https://api.openai.com/v1/engines/text-davinci-003/completions";
        string jsonRequestBody = "{\"prompt\": \"" + prompt + "\", \"max_tokens\": 50, \"temperature\": 0.7}";

        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            // API ��� ����
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + apiKey);

            // JSON ��û ���� ����
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonRequestBody);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();

            // ��û ������
            yield return request.SendWebRequest();

            // ��û �Ϸ� �� ó��
            if (request.result == UnityWebRequest.Result.Success)
            {
                string responseText = request.downloadHandler.text;
                Debug.Log("AI Response: " + responseText);
            }
            else
            {
                Debug.LogError("Error generating conversation: " + request.error);
            }
        }
    }
}
