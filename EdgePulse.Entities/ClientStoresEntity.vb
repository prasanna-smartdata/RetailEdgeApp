Public Class ClientStoresEntity
    Inherits BaseEntity
    Implements ICloneable

    Private _id As Int16
    Private _clientNumber As Int16
    Private _storeNumber As Int16
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

    Public Property ClientNumber() As Int16
        Get
            Return _clientNumber
        End Get
        Set(ByVal Value As Int16)
            _clientNumber = Value
            OnPropertyChanged("ClientNumber")
        End Set
    End Property

    Public Property StoreNumber() As Int16
        Get
            Return _storeNumber
        End Get
        Set(ByVal Value As Int16)
            _storeNumber = Value
            OnPropertyChanged("StoreNumber")
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
