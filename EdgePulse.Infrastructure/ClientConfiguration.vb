﻿Imports System.Configuration

Public Class ClientConfiguration
    Shared Function GetRmhConnectionSource() As String

        Return ConfigurationManager.ConnectionStrings("rmhClientConnection").ConnectionString
    End Function

    Shared Function GetEdgePulseConnectionSource() As String

        Return ConfigurationManager.ConnectionStrings("edgePulseConnection").ConnectionString
    End Function
End Class
