Public Class BuyingGroupEntity
    Inherits BaseEntity
    Implements ICloneable

    Private _id As Int16
    Private _buyingGroupName As String

    Public Property BuyingGroupID() As Int16
        Get
            Return _id
        End Get
        Set(ByVal value As Int16)
            _id = value
        End Set
    End Property

    Public Property BuyingGroupName() As String
        Get
            Return _buyingGroupName
        End Get
        Set(ByVal value As String)
            _buyingGroupName = value
        End Set
    End Property

    Public Function Clone() As Object Implements ICloneable.Clone
        Return CType(Me.MemberwiseClone(), ClientEntity)
    End Function
End Class
