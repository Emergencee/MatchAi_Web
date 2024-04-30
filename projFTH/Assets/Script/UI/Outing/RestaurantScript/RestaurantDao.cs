using JetBrains.Annotations;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities;
using Script.UI.StartLevel.Dao;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RestaurantDao : MonoBehaviour
{
    private RestaurantFoodList _RestaurantFoodList;

    string con = "Server=localhost;Database=testdb;Uid=root;Pwd=1234;Charset=utf8mb4";

    public void Awake()
    {
        _RestaurantFoodList = GetComponent<RestaurantFoodList>(); // ���� ���� ������Ʈ�� �پ� �ִ� RestaurantFoodList ��ũ��Ʈ�� ������

    }

    public void OnClickEatMeueBtn()
    {
        List<Dictionary<string,object>> FoodList = new List<Dictionary<string,object>>();
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
                        string foodName = (string)reader["FOODNM"];
                        int foodPrice = (int)reader["FOODPRICE"];
                        dic.Add("FOODNM", foodName);
                        dic.Add("FOODPRICE", foodPrice);

                        FoodList.Add(dic);
                    }
                }
            }
        }
        _RestaurantFoodList.GetFoodList(FoodList);
        _RestaurantFoodList.GetclickFoodList(FoodList);
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
                      int  Usercash = reader.GetInt32(0);
                        Debug.Log(Usercash);
                    }
                }
            }
        }
    }


    public void PaymentFood()
    {

        int payment = 0;
        using (MySqlConnection connection = new MySqlConnection(con))
        {
            connection.Open();
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                var sql = " update game_userinfo " +
                             " set USERCASH = (@payment)" +
                           " where SEQ = 1 ";
                // DB�� ���� ���� ����
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@payment", payment);
                cmd.ExecuteNonQuery();
            }
        }

    }
      
    

   
}