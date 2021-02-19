Public Class LootFilter
    Public Property Name As String
    Public Property Url As String
    Public Sub New(Name As String, Url As String)
        Me.Name = Name
        Me.Url = Url
    End Sub
End Class