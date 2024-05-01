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

       

        private List<GameObject> questListInstances = new List<GameObject>();

        private void Start()
        {

            questBoardDao = GetComponent<QusetBoardDao>();
            UpdateQuestList();

        }

        public void UpdateQuestList()
        {
            questList.SetActive(true);

            List<QuestBoardVO> QuestList = questBoardDao.GetQuestBoardList();




            // ������ ������ QuestList ������Ʈ���� �����մϴ�.
            foreach (GameObject questInstance in questListInstances)
            {
                Destroy(questInstance);
            }
            questListInstances.Clear();

            // ���ο� QuestList ������Ʈ�� �����ϰ� �����մϴ�.
            int i = 0;
            foreach (var quest in QuestList)
            {
                i++;

                GameObject questListInstance = Instantiate(questListPrefab, questListLayout);
                questListInstance.name = "QuestList" + i;
                questListInstances.Add(questListInstance);

                Text textComponent = questListInstance.GetComponentInChildren<Text>();

                if (textComponent != null)
                {
                    textComponent.text = quest.QuestNo + "." +
                            " : " + quest.QuestNm + "\r\n" +
                            " ���� : " + quest.QuestMemo;
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

            QuestBoardVO quest = QuestList[index - 1];
            int questNo = quest.QuestNo;

            questBoardDao.SubmitQuset(questNo);

            // ������Ʈ�� ������ �ٽ� ǥ���մϴ�.
            UpdateQuestList();
        }

        public void OnClickPlayQuest()
        {
           
            UpdateQuestList();
        }

        public void OnClickSubmitQuest()
        {
        
            UpdateQuestList();
        }

        public void OnClickCompleteQuest()
        {
            
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