using Script.UI.Outing;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    private static InventoryManager instance;
    public GameObject InventoryMenu;
    public GameObject inventoryPrefab; // QuestList �̹��� ������ ����
    public GameObject inventory; // QuestList �̹��� ����
    public GameObject detailPopup; // TODOList�� ���� ���� �˾�â

    public Transform inventorytLayout; // QuestList���� �� ���̾ƿ� ����
    private List<GameObject> inventoryInstances = new List<GameObject>();

    private InventoryDao inven;
    private List<Dictionary<string, object>> InvenList = new List<Dictionary<string, object>>();

    private void Awake()
    {
        // �ν��Ͻ��� ���� ��� ���� GameObject�� RestaurantManager�� �߰��մϴ�.
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject );
        }
    }
    public static InventoryManager Instance => instance;

    public int ItemNm { get; set; } // TODO�� ��ȣ


    private void Start()
    {
        inven = GetComponent<InventoryDao>();
        InvenList = inven.GetInvenList();
        StartInven(InvenList);
    }
    private void Update()
    {
        var popupPosition = Input.mousePosition;
        popupPosition.x += 100;
        popupPosition.y += 100;
        detailPopup.transform.position = popupPosition;
    }
    public void StartInven(List<Dictionary<string, object>> InvenList)
    {
        inventory.SetActive(true);

        // ������ ������ QuestList ������Ʈ���� �����մϴ�.
        foreach (GameObject invenInstance in inventoryInstances)
        {
            Destroy(invenInstance);
        }
        inventoryInstances.Clear();
        // ���ο� QuestList ������Ʈ�� �����ϰ� �����մϴ�.
        foreach (var inven in InvenList)
        {
            {
                GameObject invenInstance = Instantiate(inventoryPrefab, inventorytLayout);
                invenInstance.name =  (string)inven["ItemName"];
                inventoryInstances.Add(invenInstance);

                Text textComponent = invenInstance.GetComponentInChildren<Text>();
                if (textComponent != null)
                {
                    textComponent.text = inven["ItemName"] + 
                                         "\r\n" +
                                         "X"+
                                         inven["Quantity"];
                }
            }
        }
        inventory.SetActive(false);
    }
    public void OnPointerEnter()
    {
        // ���� ���콺�� ����Ű�� �ִ� ���� ������Ʈ ������
        var nowGameObject = EventSystem.current.currentSelectedGameObject;
        // �˾� Ȱ��ȭ
        detailPopup.SetActive(true);
        // �ش� ���� ������Ʈ�� TodoNo ��������
        var ItemNm = nowGameObject.name; // = Convert.ToInt32(nowGameObject.name);
                        // �˾��� ���� ���� ����
        var popupText = detailPopup.GetComponentInChildren<Text>();
        // TodoList���� �ش� Todo �׸��� ������ ������
        foreach (var dic in InvenList)
        {
            if (dic["ItemName"].Equals(ItemNm))
            {

                // �˾��� Todo �׸��� ���� ������ ���
                popupText.text = (string)dic["ItemDescription"];
                break;
            }
        }
    }
    public void OnClickInventory()
    {
        ActivateMenu(InventoryMenu);
    }
    public void OnClickInverntoryOut()
    {
        DeactivateMenu(InventoryMenu);
    }
    private void ActivateMenu(GameObject InventoryMenu)
    {
        InventoryMenu.SetActive(true);
    }
    private void DeactivateMenu(GameObject InventoryMenu)
    {
        InventoryMenu.SetActive(false);
    }
    public void OnPointerExit()
    {
        // �˾� ��Ȱ��ȭ
        detailPopup.SetActive(false);
    }
}

