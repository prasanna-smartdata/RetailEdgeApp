Public Class ClientStoreDetailsEntity
    Inherits BaseEntity
    Implements ICloneable

    Private _clientNumber As Int16
    Private _storeNumber As Int16
    Private _storeName As String
    Private _storeShortName As String

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

    Public Property StoreName() As Int16
        Get
            Return _storeName
        End Get
        Set(ByVal Value As Int16)
            _storeName = Value
            OnPropertyChanged("StoreName")
        End Set
    End Property
    Public Property StoreShortName() As Int16
        Get
            Return _storeShortName
        End Get
        Set(ByVal Value As Int16)
            _storeShortName = Value
            OnPropertyChanged("StoreShortName")
        End Set
    End Property

    Public Function Clone() As Object Implements ICloneable.Clone
        Return CType(Me.MemberwiseClone(), ClientStoreDetailsEntity)
    End Function
End Class
