﻿Class ProcessForm

    Public Sub New(ByRef ViewModel As SuperstoreProcessViewModel)

        ' This call is required by the designer.
        InitializeComponent()
        Me.DataContext = ViewModel

        ' Add any initialization after the InitializeComponent() call.

    End Sub


End Class
