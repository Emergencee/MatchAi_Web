namespace Script.UI.Outing
{
    using global::System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;
    using UnityEngine.UIElements;

    public class QuestBoardManager : MonoBehaviour
    {
        public GameObject questListPrefab; // QuestList �̹��� ������ ����
        public GameObject questList; // QuestList �̹��� ����
        public Transform questListLayout; // QuestList���� �� ���̾ƿ� ����
        private List<GameObject> questListInstances = new List<GameObject>();

        public GameObject SubmitquestListPrefab; // SubmitQuestList �̹��� ������ ����
        public GameObject SubmitquestList; // SubmitQuestList �̹��� ����
        public Transform SubmitquestListLayout; // SubmitQuestList���� �� ���̾ƿ� ����
        private List<GameObject> SubmitquestListInstances = new List<GameObject>();

        private QusetBoardDao questBoardDao;
        private List<QuestBoardVO> QuestList = new List<QuestBoardVO>();
        private void Start()
        {
            questBoardDao = GetComponent<QusetBoardDao>();
            QuestList = questBoardDao.GetQuestBoardList();

            StartQuestList(QuestList);

        }
        public void StartQuestList(List<QuestBoardVO> QuestList)
        {
            questList.SetActive(true);
            SubmitquestList.SetActive(true);

            // ������ ������ QuestList ������Ʈ���� �����մϴ�.
            foreach (GameObject questInstance in questListInstances)
            {
                Destroy(questInstance);
            }
            foreach (GameObject submitquestInstance in SubmitquestListInstances)
            {
                Destroy(submitquestInstance);
            }
            questListInstances.Clear();
            SubmitquestListInstances.Clear();
            // ���ο� QuestList ������Ʈ�� �����ϰ� �����մϴ�.


            foreach (var quest in QuestList)
            {
                if (quest.SubmitFlag.Equals("N"))
                {
                    {
                        GameObject questListInstance = Instantiate(questListPrefab, questListLayout);
                        questListInstance.name = "QuestList" + quest.QuestNo;
                        questListInstances.Add(questListInstance);

                        Text textComponent = questListInstance.GetComponentInChildren<Text>();

                        if (textComponent != null)
                        {
                            textComponent.text = quest.QuestNo + "." +
                                         " : " + quest.QuestNm + "\r\n" +
                                    " ���� : " + quest.QuestMemo;
                        }
                    }
                }
            }
            questList.SetActive(false);
            SubmitquestList.SetActive(false);
        }
        public void UpdateQuestList(List<QuestBoardVO> QuestList)
        {
            questList.SetActive(true);
            SubmitquestList.SetActive(true);

            // ������ ������ QuestList ������Ʈ���� �����մϴ�.
            foreach (GameObject questInstance in questListInstances)
            {
                Destroy(questInstance);
            }
            foreach (GameObject submitquestInstance in SubmitquestListInstances)
            {
                Destroy(submitquestInstance);
            }
            questListInstances.Clear();
            SubmitquestListInstances.Clear();
            // ���ο� QuestList ������Ʈ�� �����ϰ� �����մϴ�.


            foreach (var quest in QuestList)
            {
                if (!quest.SubmitFlag.Equals("N"))
                {
                    {
                        GameObject SubmitquestListInstance = Instantiate(SubmitquestListPrefab, SubmitquestListLayout);
                        SubmitquestListInstance.name = "QuestList" + quest.QuestNo;
                        SubmitquestListInstances.Add(SubmitquestListInstance);

                        Text textComponent = SubmitquestListInstance.GetComponentInChildren<Text>();

                        if (textComponent != null)
                        {
                            textComponent.text = quest.QuestNo + "." +
                                         " : " + quest.QuestNm + "\r\n" +
                                    " ���� : " + quest.QuestMemo;
                        }
                    }
                }
            }
            questList.SetActive(false);
            SubmitquestList.SetActive(false);
        }

        public void OnClickSubmit()
        {
            GameObject clickedButton = EventSystem.current.currentSelectedGameObject;
            GameObject parentObject = clickedButton.transform.parent.gameObject;
            string parentObjectName = parentObject.name;
            string indexString = parentObjectName.Replace("QuestList", "");
            int index = int.Parse(indexString);

            questBoardDao.SubmitQuset(index);
            // ������Ʈ�� ������ �ٽ� ǥ���մϴ�.
            StartQuestList(QuestList);
            SceneManager.LoadScene("QuestBoardScene");

        }
        public void OnClickRefuseSubmit()
        {
            GameObject clickedButton = EventSystem.current.currentSelectedGameObject;
            GameObject parentObject = clickedButton.transform.parent.gameObject;
            string parentObjectName = parentObject.name;
            string indexString = parentObjectName.Replace("QuestList", "");
            int index = int.Parse(indexString);
            questBoardDao.RefuseSubmitQuset(index);
            // ������Ʈ�� ������ �ٽ� ǥ���մϴ�.
            StartQuestList(QuestList);
            SceneManager.LoadScene("QuestBoardScene");

        }
        public void QuestButton()
        {
            string ButtonNm = OnClickQuestListButton();
            if (ButtonNm.Equals("PalybleQuest"))
            {
                StartQuestList(QuestList);
            }
            else if (ButtonNm.Equals("SubmitQuest"))
            {
                UpdateQuestList(QuestList);
            }
        }
        public string OnClickQuestListButton()
        {
            GameObject clickedButton = EventSystem.current.currentSelectedGameObject;
            string ChiceBotton = clickedButton.name;
            return ChiceBotton;
        }
        public void OnClickReturn()
        {
            {
                SceneManager.LoadScene("OutingScene");
            }
        }
    }
}