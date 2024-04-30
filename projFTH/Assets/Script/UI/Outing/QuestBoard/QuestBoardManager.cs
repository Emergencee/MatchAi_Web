namespace Script.UI.Outing
{
    using global::System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

    public class QuestBoardManager : MonoBehaviour 
    { 
        public GameObject questListPrefab; // QuestList �̹��� ������ ����
        public GameObject questList; // QuestList �̹��� ����
        public Transform questListLayout; // QuestList���� �� ���̾ƿ� ����

        private QusetBoardDao questBoardDao;

        private bool playbleQuest = true;
        private bool submitQuest;
        private bool completeQuest;

        private List<GameObject> questListInstances = new List<GameObject>();

        private void Start()
        {

            questBoardDao = GetComponent<QusetBoardDao>();
            UpdateQuestList();

        }

        public void UpdateQuestList()
        {
            questList.SetActive(true);

            List<Dictionary<string, object>> QuestList = new List<Dictionary<string, object>>();

            if (playbleQuest)
            {
                QuestList = questBoardDao.GetQuestBoardList();
            }
            else if (submitQuest)
            {
                QuestList = questBoardDao.GetSubmitQuestBoardList();
            }
            else if (completeQuest)
            {
                QuestList = questBoardDao.GetCompleteQuestBoardList();
            }

            // ������ ������ QuestList ������Ʈ���� �����մϴ�.
            foreach (GameObject questInstance in questListInstances)
            {
                Destroy(questInstance);
            }
            questListInstances.Clear();

            // ���ο� QuestList ������Ʈ�� �����ϰ� �����մϴ�.
            int i = 0;
            foreach (var dic in QuestList)
            {
                i++;

                GameObject questListInstance = Instantiate(questListPrefab, questListLayout);
                questListInstance.name = "QuestList" + i;
                questListInstances.Add(questListInstance);

                Text textComponent = questListInstance.GetComponentInChildren<Text>();

                if (textComponent != null)
                {
                    textComponent.text = dic["QUESTNO"] + "." +
                            " : " + dic["QUESTNM"] + "\r\n" +
                            " ���� : " + dic["QUESTMEMO"];
                }
            }
            questList.SetActive(false);

        }

        public void OnClickSubmit()
        {
            GameObject clickedButton = EventSystem.current.currentSelectedGameObject;
            GameObject parentObject = clickedButton.transform.parent.gameObject;
            string parentObjectName = parentObject.name;
            Debug.Log("Ŭ���� ��ư�� �θ� ������Ʈ �̸�: " + parentObjectName);

            var QuestList = questBoardDao.GetQuestBoardList();
            string indexString = parentObjectName.Replace("QuestList", "");
            int index = int.Parse(indexString);

            Dictionary<string, object> QuestInfo = QuestList[index - 1];
            int questNo = (int)QuestInfo["QUESTNO"];

            questBoardDao.SubmitQuset(questNo);

            // ������Ʈ�� ������ �ٽ� ǥ���մϴ�.
            UpdateQuestList();
        }

        public void OnClickPlayQuest()
        {
            playbleQuest = true;
            submitQuest = false;
            completeQuest = false;
            UpdateQuestList();
        }

        public void OnClickSubmitQuest()
        {
            playbleQuest = false;
            submitQuest = true;
            completeQuest = false;
            UpdateQuestList();
        }

        public void OnClickCompleteQuest()
        {
            playbleQuest = false;
            submitQuest = false;
            completeQuest = true;
            UpdateQuestList();
        }

        public void OnClickReturn()
        {
            {
                SceneManager.LoadScene("OutingScene");
            }
        }
    }
}