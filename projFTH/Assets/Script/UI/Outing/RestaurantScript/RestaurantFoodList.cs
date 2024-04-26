using JetBrains.Annotations;
using MySql.Data.MySqlClient;
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
    public string foodName;
    public int foodPrice;
    string con = "Server=localhost;Database=testdb;Uid=root;Pwd=1234;Charset=utf8mb4";

    public void Start()
    {

        OnClickEatMeueBtn();

        int i = 0;
        foreach (var dic in FoodList)
        {
            // �̹��� ������ �ν��Ͻ�ȭ
            GameObject foodListInstance = Instantiate(foodListPrefab, foodListLayout);

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
                        Dictionary<string, object> dic = new();
                        dic.Add("USERCASH", (int)reader["USERCASH"]);

                        UserInfo.Add(dic);
                    }
                }
            }
        }
    }

    
    public void PaymentFood()
    {
        GetUserInfo();

        Dictionary<string, object> dicUCash = UserInfo[0];
        int userCash = (int)dicUCash["USERCASH"];


            userCash -= foodPrice; // ����� ĳ�ÿ��� ���� ���� ����
        
        Debug.Log(userCash);
    }

   
}