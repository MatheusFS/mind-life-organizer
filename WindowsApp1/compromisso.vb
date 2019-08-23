Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms

Public Class compromisso

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim rButton1 As RadioButton = GroupBox2.Controls.OfType(Of RadioButton).FirstOrDefault(Function(r) r.Checked = True)
        Dim rButton2 As RadioButton = GroupBox1.Controls.OfType(Of RadioButton).FirstOrDefault(Function(r) r.Checked = True)
        Dim rButton3 As RadioButton = GroupBox3.Controls.OfType(Of RadioButton).FirstOrDefault(Function(r) r.Checked = True)
        Dim connectionString As String = "Server=(LocalDB)\MSSQLLocalDB;Integrated Security=true;AttachDbFilename=C:\Users\MatheusFS\source\repos\WindowsApp1\WindowsApp1\minddb.mdf;"
        Dim connection As New SqlConnection(connectionString)
        Dim sql As String = "INSERT INTO timeline(acao,inputdate,deadline,source,type,durac,done) VALUES('" + TextBox1.Text + "',GETDATE(),'" + TextBox3.Text + "/" + TextBox2.Text + "/" + TextBox4.Text + " " + TextBox5.Text + ":" + TextBox6.Text + ":00" + " " + rButton1.Text + "','" + rButton2.Text + "'," + rButton3.Text.Substring(0, 1) + "," + TextBox7.Text + ",0)"
        Dim query As New SqlCommand(sql, connection)
        connection.Open()
        Try
            query.ExecuteNonQuery()
        Catch ex As System.Data.SqlClient.SqlException
            MsgBox(ex.Message)
            MsgBox(sql)
        End Try
        connection.Close()
        MsgBox("Compromisso cadastrado com sucesso!")
        Close()
    End Sub

    Private Sub compromisso_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'smartpreencher
        TextBox2.Text = Date.Now.Day
        TextBox3.Text = Date.Now.Month
        TextBox4.Text = Date.Now.Year
    End Sub
End Class