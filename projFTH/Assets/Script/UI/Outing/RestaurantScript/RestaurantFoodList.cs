using JetBrains.Annotations;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities;
using System;
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
   
    public List<Dictionary<string, object>> FoodList = new();
    public List<Dictionary<string, object>> UserInfo = new();
    string foodName="";
    int foodPrice=0;
    int Uesrcash = 0;
    string con = "Server=localhost;Database=testdb;Uid=root;Pwd=1234;Charset=utf8mb4";

    public void Start()
    {

        OnClickEatMeueBtn();
        int i = 0;

        foreach (var dic in FoodList)
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
       

        foodList.SetActive(false);
    }

    public void OnClickEatMeueBtn()
    {

        var sql = "SELECT gr.FOODNM , gr.FOODPRICE " +
                    "FROM game_restaurant gr ";
        using (MySqlConnection connection = new MySqlConnection(con))
        {
            connection.Open();
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = sql;
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Dictionary<string, object> dic = new();
                        foodName = (string)reader["FOODNM"];
                        foodPrice = (int)reader["FOODPRICE"];
                        dic.Add("FOODNM", foodName);
                        dic.Add("FOODPRICE", foodPrice);

                        FoodList.Add(dic);
                    }
                }
            }
        }
    }

    public void GetclickFoodList()
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
        Dictionary<string, object> foodInfo = FoodList[index - 1]; // �ε����� 1���� �����ϹǷ� -1 ���ݴϴ�.

        // ������ ������ ����մϴ�.
        string foodName = (string)foodInfo["FOODNM"];
        foodPrice = (int)foodInfo["FOODPRICE"];

    }

    public void GetUserInfo()
    {
        var sql = "  SELECT gu.USERCASH " +
                 "   FROM game_userinfo gu ";
        using (MySqlConnection connection = new MySqlConnection(con))
        {
            connection.Open();
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = sql;
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Uesrcash = reader.GetInt32(0);
                        Debug.Log("DB���� "+Uesrcash);
                    }
                }
            }
        }
    }

    
    public void PaymentFood()
    {
        GetUserInfo();
 

        int payment = Uesrcash - foodPrice; // ����� ĳ�ÿ��� ���� ���� ����
        Debug.Log("������ " +payment);

        if (payment > 0)
        {

            using (MySqlConnection connection = new MySqlConnection(con))
            {
                connection.Open();
                using (MySqlCommand cmd = connection.CreateCommand())
                {
                    var sql = " update game_userinfo " +
                                 " set USERCASH = (@payment)" +
                               " where USERNO = 1 ";
                    // DB�� ���� ���� ����
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@payment", payment);
                    cmd.ExecuteNonQuery();
                }
            }

        }
        if(payment < 0)
        {
            Debug.Log("�������� �����մϴ�");
        }

    }

   
}