﻿Class ClientMaintenancePage


    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        Me.DataContext = New ClientViewModel()
        ' Add any initialization after the InitializeComponent() call.

    End Sub



    Private Sub btnExit_Click(sender As Object, e As RoutedEventArgs)
        '   Me.Close()
    End Sub


End Class
