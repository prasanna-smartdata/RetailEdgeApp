﻿Public Class RMHClientBaseDL

    Public ReadOnly Property ConnectionString() As String
        Get
            Return Infrastructure.ClientConfiguration.GetRmhConnectionSource
        End Get
    End Property


End Class
