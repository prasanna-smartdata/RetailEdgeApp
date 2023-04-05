﻿Public Class ClientEntity
    Inherits BaseEntity
    Implements ICloneable

    Private _id As Int16
    Private _clientNum As String
    Private _fakeClient As Boolean
    Private _clientName As String
    Private _path As String
    Private _comment As String
    Private _emailAddress As String
    Private _noOfSites As Int16
    Private _recEmail As String
    Private _buyingGroupId As Int16
    Private _accountManagerId As Int16
    Private _proactiveCallDate As Date
    Private _supportEmail As String
    Private _isActiveClient As Boolean
    Private _useEdgeSW As Boolean
    Private _layawaysOnPickUp As Boolean
    Private _isDoorCounter As Boolean
    Private _isMacroOked As Boolean
    Private _isSupplierWebSystem As Boolean
    Private _state As String
    Private _HoStoreNumber As String
    Private _isEdgePulseEnabled As Boolean
    Private _isMentorningClient As Boolean
    Private _isSuperStoreActive As Boolean
    Private _isKPILite As Boolean
    Private _jewelsure As Boolean
    Private _results As Boolean
    Private _sqlServer As Boolean

    Public Property ID() As Int16
        Get
            Return _id
        End Get
        Set(ByVal Value As Int16)
            _id = Value
            OnPropertyChanged("ID")
        End Set
    End Property


    Public Property ClientNumber() As String
        Get
            Return _clientNum
        End Get
        Set(ByVal Value As String)
            _clientNum = Value
            OnPropertyChanged("ClientNumber")
        End Set
    End Property

    Public Property FakeClient() As Boolean
        Get
            Return _fakeClient
        End Get
        Set(ByVal Value As Boolean)
            _fakeClient = Value
            OnPropertyChanged("FakeClient")
        End Set
    End Property

    Public Property ClientName() As String
        Get
            Return _clientName
        End Get
        Set(ByVal Value As String)
            _clientName = Value
            OnPropertyChanged("ClientName")
        End Set
    End Property

    Public Property Path() As String
        Get
            Return _path
        End Get
        Set(ByVal Value As String)
            _path = Value
            OnPropertyChanged("Path")
        End Set
    End Property

    Public Property Comment() As String
        Get
            Return _comment
        End Get
        Set(ByVal Value As String)
            _comment = Value
            OnPropertyChanged("Comment")
        End Set
    End Property

    Public Property EmailAddress() As String
        Get
            Return _emailAddress
        End Get
        Set(ByVal Value As String)
            _emailAddress = Value
            OnPropertyChanged("EmailAddress")
        End Set
    End Property

    Public Property NoOfSites() As Int16
        Get
            Return _noOfSites
        End Get
        Set(ByVal Value As Int16)
            _noOfSites = Value
            OnPropertyChanged("NoOfSites")
        End Set
    End Property

    Public Property RecEmail() As String
        Get
            Return _recEmail
        End Get
        Set(ByVal Value As String)
            _recEmail = Value
            OnPropertyChanged("RecEmail")
        End Set
    End Property

    Public Property BuyingGroupId() As Int16
        Get
            Return _buyingGroupId
        End Get
        Set(ByVal Value As Int16)
            _buyingGroupId = Value
            OnPropertyChanged("BuyingGroupId")
        End Set
    End Property

    Public Property AccountManagerId() As Int16
        Get
            Return _accountManagerId
        End Get
        Set(ByVal Value As Int16)
            _accountManagerId = Value
            OnPropertyChanged("AccountManagerId")
        End Set
    End Property
    Public Property ProactiveCallDate() As Date
        Get
            Return _proactiveCallDate
        End Get
        Set(ByVal Value As Date)
            _proactiveCallDate = Value
            OnPropertyChanged("ProactiveCallDate")
        End Set
    End Property
    Public Property SupportEmail() As String
        Get
            Return _supportEmail
        End Get
        Set(ByVal Value As String)
            _supportEmail = Value
            OnPropertyChanged("SupportEmail")
        End Set
    End Property
    Public Property IsActiveClient() As Boolean
        Get
            Return _isActiveClient
        End Get
        Set(ByVal Value As Boolean)
            _isActiveClient = Value
            OnPropertyChanged("IsActiveClient")
        End Set
    End Property
    Public Property UseEdgeSW() As Boolean
        Get
            Return _useEdgeSW
        End Get
        Set(ByVal Value As Boolean)
            _useEdgeSW = Value
            OnPropertyChanged("UseEdgeSW")
        End Set
    End Property
    Public Property LayawaysOnPickUp() As Boolean
        Get
            Return _layawaysOnPickUp
        End Get
        Set(ByVal Value As Boolean)
            _layawaysOnPickUp = Value
            OnPropertyChanged("LayawaysOnPickUp")
        End Set
    End Property
    Public Property IsDoorCounter() As Boolean
        Get
            Return _isDoorCounter
        End Get
        Set(ByVal Value As Boolean)
            _isDoorCounter = Value
            OnPropertyChanged("IsDoorCounter")
        End Set
    End Property
    Public Property IsMacroOked() As Boolean
        Get
            Return _isMacroOked
        End Get
        Set(ByVal Value As Boolean)
            _isMacroOked = Value
            OnPropertyChanged("IsMacroOked")
        End Set
    End Property
    Public Property IsSupplierWebSystem() As Boolean
        Get
            Return _isSupplierWebSystem
        End Get
        Set(ByVal Value As Boolean)
            _isSupplierWebSystem = Value
            OnPropertyChanged("IsSupplierWebSystem")
        End Set
    End Property
    Public Property State() As String
        Get
            Return _state
        End Get
        Set(ByVal Value As String)
            _state = Value
            OnPropertyChanged("State")
        End Set
    End Property
    Public Property HoStoreNumber() As String
        Get
            Return _HoStoreNumber
        End Get
        Set(ByVal Value As String)
            _HoStoreNumber = Value
            OnPropertyChanged("HoStoreNumber")
        End Set
    End Property
    Public Property IsEdgePulseEnabled() As String
        Get
            Return _isEdgePulseEnabled
        End Get
        Set(ByVal Value As String)
            _isEdgePulseEnabled = Value
            OnPropertyChanged("IsEdgePulseEnabled")
        End Set
    End Property
    Public Property IsMentorningClient() As String
        Get
            Return _isMentorningClient
        End Get
        Set(ByVal Value As String)
            _isMentorningClient = Value
            OnPropertyChanged("IsMentorningClient")
        End Set
    End Property
    Public Property IsSuperStoreActive() As Boolean
        Get
            Return _isSuperStoreActive
        End Get
        Set(ByVal Value As Boolean)
            _isSuperStoreActive = Value
            OnPropertyChanged("IsSuperStoreActive")
        End Set
    End Property
    Public Property IsKPILite() As Boolean
        Get
            Return _isKPILite
        End Get
        Set(ByVal Value As Boolean)
            _isKPILite = Value
            OnPropertyChanged("IsKPILite")
        End Set
    End Property
    Public Property Jewelsure() As String
        Get
            Return _jewelsure
        End Get
        Set(ByVal Value As String)
            _jewelsure = Value
            OnPropertyChanged("Jewelsure")
        End Set
    End Property
    Public Property Results() As String
        Get
            Return _results
        End Get
        Set(ByVal Value As String)
            _results = Value
            OnPropertyChanged("Results")
        End Set
    End Property
    Public Property SqlServer() As Boolean
        Get
            Return _sqlServer
        End Get
        Set(ByVal Value As Boolean)
            _sqlServer = Value
            OnPropertyChanged("SqlServer")
        End Set
    End Property
    Public Function Clone() As Object Implements ICloneable.Clone
        Return CType(Me.MemberwiseClone(), ClientEntity)
    End Function
End Class
