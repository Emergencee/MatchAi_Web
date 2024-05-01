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
    private RestaurantManager restaurantManager;

    string con = "Server=localhost;Database=testdb;Uid=root;Pwd=1234;Charset=utf8mb4";

    public void Awake()
    {
        restaurantManager = GetComponent<RestaurantManager>(); // ���� ���� ������Ʈ�� �پ� �ִ� RestaurantFoodList ��ũ��Ʈ�� ������

    }

    public List<FoodListVO> GetFoodListFromDB()
    {
        List<FoodListVO> FoodList = new List<FoodListVO> ();
        var sql = "SELECT gr.SEQ, gr.FOODNM , gr.FOODPRICE " +
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
                        FoodListVO fv = new FoodListVO();
                        fv.FoodNo = (int)reader["SEQ"];
                        fv.FoodNm = (string)reader["FOODNM"];
                        fv.FoodPr = (int)reader["FOODPRICE"];

                        FoodList.Add(fv);
                    }
                }
            }
        }
       
        return FoodList;
    }



    public int  GetUserInfoFromDB()
    {
        int Usercash = 0;
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
                       Usercash = reader.GetInt32(0);
                        Debug.Log(Usercash);
                    }
                }
            }
        }
        return Usercash;
    }


    public void UpdateUserCash(int payment)
    {

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