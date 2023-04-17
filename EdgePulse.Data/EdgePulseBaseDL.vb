Public Class EdgePulseBaseDL
    Public ReadOnly Property ConnectionString() As String
        Get
            Return Infrastructure.ClientConfiguration.GetEdgePulseConnectionSource
        End Get
    End Property

End Class
