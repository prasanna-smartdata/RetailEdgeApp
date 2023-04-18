Public Class Report_SSClientStoresDetailsEntity
    Inherits BaseEntity
    Implements ICloneable

    Private _superstoreGroupID As Int16
    Private _clientID As Int16
    Private _clientNum As String
    Private _storeID As Int16
    Private _storeName As String
    Private _dateCreated As Date

    Public Property SuperstoreGroupID() As Int16
        Get
            Return _superstoreGroupID
        End Get
        Set(ByVal Value As Int16)
            _superstoreGroupID = Value

        End Set
    End Property

    Public Property ClientID() As Int16
        Get
            Return _clientID
        End Get
        Set(ByVal Value As Int16)
            _clientID = Value
        End Set
    End Property
    Public Property ClientNum() As String
        Get
            Return _clientNum
        End Get
        Set(ByVal Value As String)
            _clientNum = Value
        End Set
    End Property

    Public Property StoreID() As Int16
        Get
            Return _storeID
        End Get
        Set(ByVal Value As Int16)
            _storeID = Value
        End Set
    End Property
    Public Property StoreName() As String
        Get
            Return _storeName
        End Get
        Set(ByVal Value As String)
            _storeName = Value
        End Set
    End Property


    Public Property DateCreated() As Date
        Get
            Return _dateCreated
        End Get
        Set(ByVal Value As Date)
            _dateCreated = Value
            OnPropertyChanged("DateCreated")
        End Set
    End Property

    Public Function Clone() As Object Implements ICloneable.Clone
        Return CType(Me.MemberwiseClone(), ClientStoreSuperstoreEntity)
    End Function

End Class
