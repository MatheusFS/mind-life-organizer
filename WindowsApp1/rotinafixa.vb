Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms

Public Class rotinafixa
    Private Sub rotinafixa_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'DataGridView1.Columns.Item(0).Name = "Field1"
        'DataGridView1.Columns.Item(0).DataPropertyName = "acao"
        'DataGridView1.Columns.Item(1).Name = "Field2"
        'DataGridView1.Columns.Item(1).DataPropertyName = "Field2"

        'bindingSource1.DataSource = MinddbDataSet
        'DataGridView1.DataSource = bindingSource1

        Dim connectionString As String = "Server=(LocalDB)\MSSQLLocalDB;Integrated Security=true;AttachDbFilename=C:\Users\MatheusFS\source\repos\WindowsApp1\WindowsApp1\minddb.mdf;"
        Dim sql As String = "SELECT acao AS SEGUNDA FROM rotinafixa WHERE dia='segunda'"
        Dim sql2 As String = "SELECT acao AS TERÇA FROM rotinafixa WHERE dia='terca'"
        Dim sql3 As String = "SELECT acao AS QUARTA FROM rotinafixa WHERE dia='quarta'"
        Dim sql4 As String = "SELECT acao AS QUINTA FROM rotinafixa WHERE dia='quinta'"
        Dim sql5 As String = "SELECT acao AS SEXTA FROM rotinafixa WHERE dia='sexta'"
        Dim sql6 As String = "SELECT acao AS SABADO FROM rotinafixa WHERE dia='sabado'"
        Dim sql7 As String = "SELECT acao AS DOMINGO FROM rotinafixa WHERE dia='domingo'"
        Dim connection As New SqlConnection(connectionString)
        Dim dataadapter As New SqlDataAdapter(sql, connection)
        Dim dataadapter2 As New SqlDataAdapter(sql2, connection)
        Dim dataadapter3 As New SqlDataAdapter(sql3, connection)
        Dim dataadapter4 As New SqlDataAdapter(sql4, connection)
        Dim dataadapter5 As New SqlDataAdapter(sql5, connection)
        Dim dataadapter6 As New SqlDataAdapter(sql6, connection)
        Dim dataadapter7 As New SqlDataAdapter(sql7, connection)
        Dim ds As New DataSet()
        Dim ds2 As New DataSet()
        Dim ds3 As New DataSet()
        Dim ds4 As New DataSet()
        Dim ds5 As New DataSet()
        Dim ds6 As New DataSet()
        Dim ds7 As New DataSet()
        connection.Open()
        dataadapter.Fill(ds, "rotinafixa")
        dataadapter2.Fill(ds2, "rotinafixa2")
        dataadapter3.Fill(ds3, "rotinafixa3")
        dataadapter4.Fill(ds4, "rotinafixa4")
        dataadapter5.Fill(ds5, "rotinafixa5")
        dataadapter6.Fill(ds6, "rotinafixa6")
        dataadapter7.Fill(ds7, "rotinafixa7")
        connection.Close()
        DataGridView1.DataSource = ds
        DataGridView2.DataSource = ds2
        DataGridView3.DataSource = ds3
        DataGridView4.DataSource = ds4
        DataGridView5.DataSource = ds5
        DataGridView6.DataSource = ds6
        DataGridView7.DataSource = ds7
        DataGridView1.DataMember = "rotinafixa"
        DataGridView2.DataMember = "rotinafixa2"
        DataGridView3.DataMember = "rotinafixa3"
        DataGridView4.DataMember = "rotinafixa4"
        DataGridView5.DataMember = "rotinafixa5"
        DataGridView6.DataMember = "rotinafixa6"
        DataGridView7.DataMember = "rotinafixa7"
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim dow As String

        Select Case Date.Today.DayOfWeek.ToString
            Case "Monday"
                dow = "segunda"
            Case "Tuesday"
                dow = "terca"
            Case "Wednesday"
                dow = "quarta"
            Case "Thursday"
                dow = "quinta"
            Case "Friday"
                dow = "sexta"
            Case "Saturday"
                dow = "sabado"
            Case "Sunday"
                dow = "domingo"
        End Select

        MsgBox("Hoje é " + Date.Today.DayOfWeek.ToString + " (dia " + Date.Today.DayOfYear.ToString + ")")
        Dim connectionString As String = "Server=(LocalDB)\MSSQLLocalDB;Integrated Security=true;AttachDbFilename=C:\Users\MatheusFS\source\repos\WindowsApp1\WindowsApp1\minddb.mdf;"
        Dim connection As New SqlConnection(connectionString)
        Dim connection2 As New SqlConnection(connectionString)
        Dim connection3 As New SqlConnection(connectionString)
        Dim connection4 As New SqlConnection(connectionString)
        Dim query As New SqlCommand("SELECT acao, idhorario FROM rotinafixa WHERE dia='" + dow + "' AND acao != 'Livre' ORDER BY idhorario ASC", connection)
        Dim query2 As New SqlCommand
        Dim query3 As SqlCommand
        Dim query4 As SqlCommand
        Dim SQLData1 As SqlDataReader
        Dim SQLData2 As SqlDataReader
        Dim proxdia = Date.Today.Day.ToString + 1

        connection.Open() ' {
        connection2.Open() ' {
        SQLData1 = query.ExecuteReader()
        Do While SQLData1.Read() ' {

            '--------------
            query3 = New SqlCommand("SELECT * FROM timeline WHERE acao='" + SQLData1.GetValue(0) + " (" + Today.Day.ToString + "/" + Today.Month.ToString + ")'", connection3)
            query4 = New SqlCommand("INSERT INTO timeline(acao,inputdate,deadline,source,type,done) VALUES('" + SQLData1.GetValue(0) + " (" + Today.Day.ToString + "/" + Today.Month.ToString + ")',GETDATE(),'11/" + proxdia.ToString + "/2017 00:00 AM', 'Interno', 3, 0)", connection4)

            connection3.Open()
            SQLData2 = query3.ExecuteReader()
            If SQLData2.HasRows Then
                ' Já tem
            Else
                connection4.Open()
                query4.ExecuteNonQuery()
                connection4.Close()
            End If
            connection3.Close()
            SQLData2.Close()

        Loop
        SQLData1.Close() ' }
        connection2.Close() ' }
        connection.Close() ' }
        Form1.atttimeline.Start()
        Me.Close()
    End Sub
End Class