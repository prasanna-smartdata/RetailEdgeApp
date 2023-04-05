Public Class SuperstoreEntity
    Inherits BaseEntity
    Implements ICloneable

    Private _id As Int16
    Private _groupNum As String
    Private _groupName As String
    Private _deptsToUse As String

    Public Property ID() As Int16
        Get
            Return _id
        End Get
        Set(ByVal Value As Int16)
            _id = Value
            OnPropertyChanged("ID")
        End Set
    End Property

    Public Property GroupNum() As String
        Get
            Return _groupNum
        End Get
        Set(ByVal Value As String)
            _groupNum = Value
            OnPropertyChanged("GroupNum")
        End Set
    End Property

    Public Property GroupName() As String
        Get
            Return _groupName
        End Get
        Set(ByVal Value As String)
            _groupName = Value
            OnPropertyChanged("GroupName")
        End Set
    End Property
    Public Property DeptsToUse() As Int16
        Get
            Return _deptsToUse
        End Get
        Set(ByVal Value As Int16)
            _deptsToUse = Value
            OnPropertyChanged("DeptsToUse")
        End Set
    End Property
    Public Function Clone() As Object Implements ICloneable.Clone
        Return CType(Me.MemberwiseClone(), SuperstoreEntity)
    End Function
End Class
