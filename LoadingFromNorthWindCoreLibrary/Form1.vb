﻿Imports NorthWindCoreLibrary.Classes

Public Class Form1
    Private Async Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim results = Await CustomersOperations.GetCustomersAsync()
        Debug.WriteLine(results.Count)
    End Sub
End Class