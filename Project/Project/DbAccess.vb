﻿Imports System.Data
Imports System.Data.Odbc
Public Class DataTier
    Private ConnectionString As String = "Driver={MySQL ODBC 5.3 ANSI Driver}; server=141.209.241.47; database=bis425c1g2; user=bis425c1g2; password=initpass"
    Private DBConnection As New OdbcConnection(ConnectionString)
    Private DBCommand As OdbcCommand
    Public DBDataAdapter As OdbcDataAdapter
    Public DBDataTable As DataTable
    Public Params As New List(Of OdbcParameter)
    Public RecordCount As Integer
    Public Exception As String
    Public Sub ExecuteQuery(QueryString As String)

        RecordCount = 0
        Exception = String.Empty
        Try
            DBConnection.Open()
            DBCommand = New OdbcCommand(QueryString, DBConnection)
            For Each p As OdbcParameter In Params
                DBCommand.Parameters.Add(p)
            Next
            Params.Clear()
            DBDataTable = New DataTable
            DBDataAdapter = New OdbcDataAdapter(DBCommand)
            RecordCount = DBDataAdapter.Fill(DBDataTable)
        Catch ex As Exception
            Exception = ex.Message
        End Try
        If DBConnection.State = ConnectionState.Open Then
            DBConnection.Close()
        End If
    End Sub
    Public Sub AddParam(Name As String, Value As Object)
        Dim NewParam As New OdbcParameter(Name, Value)
        Params.Add(NewParam)
    End Sub
End Class
