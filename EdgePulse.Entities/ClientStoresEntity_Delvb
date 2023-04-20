Public Class ClientStoresEntity
    Inherits BaseEntity
    Implements ICloneable

    Private _id As Int16
    Private _clientID As Int16
    Private _clientNumber As Int16
    Private _storeID As Int16
    Private _storeName As String

    Public Property ID() As Int16
        Get
            Return _id
        End Get
        Set(ByVal Value As Int16)
            _id = Value
            OnPropertyChanged("ID")
        End Set
    End Property
    Public Property ClientID() As Int16
        Get
            Return _clientID
        End Get
        Set(ByVal Value As Int16)
            _clientID = Value
            OnPropertyChanged("ClientID")
        End Set
    End Property
    Public Property ClientNumber() As Int16
        Get
            Return _clientNumber
        End Get
        Set(ByVal Value As Int16)
            _clientNumber = Value
            OnPropertyChanged("ClientNumber")
        End Set
    End Property

    Public Property StoreID() As Int16
        Get
            Return _storeID
        End Get
        Set(ByVal Value As Int16)
            _storeID = Value
            OnPropertyChanged("StoreID")
        End Set
    End Property
    Public Property StoreName() As String
        Get
            Return _storeName
        End Get
        Set(ByVal Value As String)
            _storeName = Value
            OnPropertyChanged("StoreName")
        End Set
    End Property
    Public Function Clone() As Object Implements ICloneable.Clone
        Return CType(Me.MemberwiseClone(), ClientStoresEntity)
    End Function
End Class
