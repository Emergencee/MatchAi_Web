using Script.UI.StartLevel.Dao;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RestaurantFoodList : MonoBehaviour
{
    public GameObject foodListPrefab; // foodList �̹��� ������ ����
    public GameObject foodList; // foodList �̹��� ����
    public Transform foodListLayout; // foodList �� ���̾ƿ� ����


    private RestaurantDao _rsetaurantDao;



    string foodName = "";
    int foodPrice = 0;
    int Uesrcash = 0;

    public void Awake()
    {
        _rsetaurantDao = GetComponent<RestaurantDao>(); // ���� ���� ������Ʈ�� �پ� �ִ� RestaurantDao ��ũ��Ʈ�� ������

    }

    public void GetFoodList(List<Dictionary<string, object>> foodList)
    {

        int i = 0;
        _rsetaurantDao.OnClickEatMeueBtn();
        foreach (var dic in foodList)
        {
            i++;
            string a = i.ToString();
            // �̹��� ������ �ν��Ͻ�ȭ
            GameObject foodListInstance = Instantiate(foodListPrefab, foodListLayout);
            foodListInstance.name = "foodlist" + a;
            // �̹��� ������Ʈ�� ��ųʸ� �� ����
            Text textComponent = foodListInstance.GetComponentInChildren<Text>();
            if (textComponent != null)
            {
                textComponent.text = dic["FOODNM"] + "\r\n" + " " + dic["FOODPRICE"];

            }
        }
        foodList.Clear();
    }

        public void GetclickFoodList(List<Dictionary<string, object>> foodList)
        {
            // �̺�Ʈ �ý��ۿ��� ���� ���õ� ���� ������Ʈ�� �����ɴϴ�.
            GameObject clickList = EventSystem.current.currentSelectedGameObject;

            // Ŭ���� ���� ������Ʈ�� �̸��� �����ɴϴ�.
            string objectName = clickList.name;

            // "foodlist"�� �����ϰ� �ε����� ����ϴ�.
            string indexString = objectName.Replace("foodlist", "");

            // ������ �ε��� ���ڿ��� ������ ��ȯ�մϴ�.
            int index = int.Parse(indexString);

            // �ش� �ε����� �ش��ϴ� ���� ������ �����ɴϴ�.
            Dictionary<string, object> foodInfo = foodList[index - 1]; // �ε����� 1���� �����ϹǷ� -1 ���ݴϴ�.

            // ������ ������ ����մϴ�.
            foodName = (string)foodInfo["FOODNM"];
            foodPrice = (int)foodInfo["FOODPRICE"];

        }
    }
