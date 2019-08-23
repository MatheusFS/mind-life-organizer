Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms

Public Class Form1

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        rotinafixa.Show()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles seg.Tick
        Label1.Text = Date.Now()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        compromisso.Show()
    End Sub

    Private Sub Timeline_LOG(sender As Object, e As EventArgs) Handles MyBase.Load, Button5.Click, atttimeline.Tick
        Dim connectionString As String = "Server=(LocalDB)\MSSQLLocalDB;Integrated Security=true;AttachDbFilename=C:\Users\MatheusFS\source\repos\WindowsApp1\WindowsApp1\minddb.mdf;"
        Dim sql As String = "SELECT id,acao,inputdate,DATEDIFF(SECOND, GETDATE(), FORMAT(deadline, 'yyyy-MM-dd HH:mm:ss'))/3600 AS exp,source,type,done FROM timeline WHERE done=0 AND DATEDIFF(SECOND, GETDATE(), FORMAT(deadline, 'yyyy-MM-dd HH:mm:ss')) > -1000000 AND DATEDIFF(SECOND, GETDATE(), FORMAT(deadline, 'yyyy-MM-dd HH:mm:ss')) < 86400 ORDER BY exp ASC;"
        Dim sql2 As String = "SELECT id,acao,inputdate,DATEDIFF(SECOND, GETDATE(), FORMAT(deadline, 'yyyy-MM-dd HH:mm:ss'))/3600 AS exp,source,type,done FROM timeline WHERE done=0 AND DATEDIFF(SECOND, GETDATE(), FORMAT(deadline, 'yyyy-MM-dd HH:mm:ss')) >= 86400 AND DATEDIFF(SECOND, GETDATE(), FORMAT(deadline, 'yyyy-MM-dd HH:mm:ss')) < 604800 ORDER BY exp ASC;"
        Dim sql3 As String = "SELECT id,acao,inputdate,DATEDIFF(SECOND, GETDATE(), FORMAT(deadline, 'yyyy-MM-dd HH:mm:ss'))/3600 AS exp,source,type,done FROM timeline WHERE done=0 AND DATEDIFF(SECOND, GETDATE(), FORMAT(deadline, 'yyyy-MM-dd HH:mm:ss')) >= 604800 ORDER BY exp ASC;"
        Dim sql4 As String = "SELECT id,acao,source,type FROM timeline WHERE done=1"
        Dim sql5 As String = "SELECT acao,DATEDIFF(SECOND, GETDATE(), FORMAT(deadline, 'yyyy-MM-dd HH:mm:ss'))/3600 AS exp FROM timeline WHERE done=0 AND DATEDIFF(SECOND, GETDATE(), FORMAT(deadline, 'yyyy-MM-dd HH:mm:ss')) < 32400 ORDER BY exp ASC;"
        Dim connection As New SqlConnection(connectionString)
        Dim connection2 As New SqlConnection(connectionString)
        Dim dataadapter As New SqlDataAdapter(sql, connection)
        Dim dataadapter2 As New SqlDataAdapter(sql2, connection)
        Dim dataadapter3 As New SqlDataAdapter(sql3, connection)
        Dim ds As New DataSet()
        Dim ds2 As New DataSet()
        Dim ds3 As New DataSet()
        Dim z As Integer
        Dim query As New SqlCommand(sql5, connection)
        Dim qry1 As SqlCommand ' SQLData1 - count(0) - connection
        Dim query2 As SqlCommand '- 
        Dim query3 As New SqlCommand("UPDATE users SET cqh = cqh + 10 WHERE id=1", connection2)
        Dim qry4 As SqlCommand ' SQLData2 - count(1) - connection
        Dim query5 As New SqlCommand("SELECT sum(durac) FROM timeline WHERE done=0 AND DATEDIFF(SECOND, GETDATE(), FORMAT(deadline, 'yyyy-MM-dd HH:mm:ss')) < 32400;", connection2)
        Dim SQLData1 As SqlDataReader
        Dim SQLData2 As SqlDataReader
        Dim SQLData3 As SqlDataReader
        Dim resultado As MsgBoxResult

        connection.Open() ' {
        SQLData1 = query.ExecuteReader()
        connection2.Open()
        SQLData3 = query5.ExecuteReader()
        SQLData3.Read()
        Label8.Text = SQLData3.GetValue(0).ToString
        SQLData3.Close() ' }
        connection2.Close() ' }
        Do While SQLData1.Read() ' {
            query2 = New SqlCommand("UPDATE timeline SET done = 1 WHERE acao='" + SQLData1.GetValue(0) + "'", connection2)
            resultado = MsgBox("O compromisso `" + SQLData1.GetValue(0) + "` foi finalizado?", vbYesNo, "Compromisso expirado")
            If resultado = vbYes Then
                connection2.Open() ' {
                query2.ExecuteNonQuery()
                query3.ExecuteNonQuery()
                connection2.Close() ' }
                MsgBox("Parabéns! (+10)")
            Else
                'MsgBox("AI SAO TRES DAMANHA E EU TO BOLADAO NUM PASSAT")
            End If
        Loop
        SQLData1.Close() ' }
        connection.Close() ' }

        qry4 = New SqlCommand("SELECT acao FROM timeline WHERE done=1", connection2)

        connection2.Open() ' {
        SQLData2 = qry4.ExecuteReader()
        Do While SQLData2.Read() ' {
            If ListBox2.Items.Contains(SQLData2.GetValue(0)) Then
                ' Já adicionado
            Else
                ListBox2.Items.Add(SQLData2.GetValue(0))
            End If
        Loop
        SQLData2.Close() ' }
        connection2.Close() ' }

        connection.Open()
        dataadapter.Fill(ds, "timeline")
        dataadapter2.Fill(ds2, "timeline2")
        dataadapter3.Fill(ds3, "timeline3")
        qry1 = New SqlCommand("SELECT count(acao) FROM timeline WHERE done=0 AND DATEDIFF(SECOND, GETDATE(), FORMAT(deadline, 'yyyy-MM-dd HH:mm:ss')) < 0", connection)
        SQLData1 = qry1.ExecuteReader()

        SQLData1.Read() ' qry1 - count - connection
        z = SQLData1.GetValue(0)
        SQLData1.Close()

        connection.Close()

        DataGridView1.DataSource = ds
        DataGridView2.DataSource = ds2
        DataGridView3.DataSource = ds3
        DataGridView1.DataMember = "timeline"
        DataGridView2.DataMember = "timeline2"
        DataGridView3.DataMember = "timeline3"

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        Me.Close()

    End Sub
End Class
