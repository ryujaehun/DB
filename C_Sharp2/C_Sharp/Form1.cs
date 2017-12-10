using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C_Sharp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        //로그인 버튼을 클릭 구현한 리스너
        private void login_button_Click(object sender, EventArgs e)
        {
            

            SqlConnection con =new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\lujae\Documents\Database.mdf;Integrated Security=True;Connect Timeout=30");
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) USERNAME from USERINFO where USERNAME='"+id_text.Text+ "'and PASSWORD='"+pw_text.Text+"'", con);

            //결과처리
            DataTable dataTable = new DataTable();
            sda.Fill(dataTable);
            con.Close();
            if (dataTable.Rows[0][0].ToString()=="1")
            {
                //로그인이 성공인 경우
                this.Hide();
                diagramForm diagramform = new diagramForm();
                diagramform.Show();
                
            }
            else
            {
                // 로그인이 실패
                // 메세지 박스를 띄워줌
                MessageBox.Show("아이디와 비밀번호를 확인해주세요.");
            }

            
        }

        private void join_button_Click(object sender, EventArgs e)
        {
            //null값 예외처리
            if(id_text.Text.ToString()==""|| pw_text.Text.ToString() == "")
                MessageBox.Show("아이디와 비밀번호를 입력해주세요!!");
            else {
                  using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\lujae\Documents\Database.mdf;Integrated Security=True;Connect Timeout=30"))
                    {
                        using (SqlCommand command = new SqlCommand())
                        {
                            command.Connection = connection;            // <== lacking
                            command.CommandType = CommandType.Text;
                            command.CommandText = "INSERT into USERINFO (USERNAME, PASSWORD) VALUES (@userID, @userPW)";
                            command.Parameters.AddWithValue("@userID", id_text.Text.ToString());
                            command.Parameters.AddWithValue("@userPW", pw_text.Text.ToString());
                    
                            try
                            {
                                connection.Open();
                                int recordsAffected = command.ExecuteNonQuery();
                            }
                            catch (SqlException)
                            {
                                // error here
                            }
                            finally
                            {
                                connection.Close();
                            MessageBox.Show("회원가입이 완료되었습니다!");
                        }
                        }
                    }
                
              
            

            }
        }

        private void diagram_button_Click(object sender, EventArgs e)
        {
            this.Hide();
            diagramForm diagramform = new diagramForm();
            diagramform.Show();
        }
    }
}
