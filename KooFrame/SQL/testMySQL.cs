using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using MySql.Data.MySqlClient;
using System.Linq;
using UnityEngine.SceneManagement;

public class testMySQL : MonoBehaviour
{
	public string name1;
	public string pwd1;
	bool flag = false;

	public void ConectSql()
	{
		MySqlConnection sqlCon = new MySqlConnection("server=127.0.0.1;port=3306;user=root;password=0000; database=user;charset=utf8");
		MySqlCommand cmd = new MySqlCommand();
		cmd.Connection = sqlCon;
		cmd.CommandType = System.Data.CommandType.Text;
		cmd.CommandText = "select * from userdata where User='" + this.name1.ToString() + "'and Password='" + this.pwd1.ToString() + "'";
		MySqlDataAdapter da = new MySqlDataAdapter(cmd);
		da.SelectCommand.Connection.Open();
		MySqlDataReader rd = cmd.ExecuteReader();
		if (rd.Read())
		{
			Debug.Log("登入成功");
		}
		else
		{
			flag = true;
		}
		da.SelectCommand.Connection.Close();
		sqlCon.Close();

	}
	private void OnGUI()
	{
		GUI.Label(new Rect(10, 50, 100, 20), "用户名");
		GUI.Label(new Rect(10, 80, 100, 20), "密码");
		name1 = GUI.TextField(new Rect(60, 50, 120, 20), name1);
		pwd1 = GUI.PasswordField(new Rect(60, 80, 120, 20), pwd1, "*"[0], 16);
		if (GUI.Button(new Rect(40, 120, 50, 20), "登录"))
		{
			ConectSql();
		}
		if (GUI.Button(new Rect(120, 120, 50, 20), "重置"))
		{
			name1 = "";
			pwd1 = "";
		}
		if (flag)
		{
			GUI.Label(new Rect(10, 150, 200, 50), "用户名或密码错误，请重新输入");
			name1 = "";
			pwd1 = "";
		}

	}
}
