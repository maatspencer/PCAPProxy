Imports System.IO

Public Class Status
    Public Property objectId() As Int32
    Public Property pos() As Location
    Public Property data As IList(Of StatData)

    Public Shared Function parse(b As IList(Of Byte), ByRef o As Integer) As Status
        Dim status As New Status
        ' Get object ID
        status.objectId = byteHandler.readInt32(b, o)

        ' Get Object Position
        status.pos = Location.parse(b, o)

        ' Status Count
        Dim count As Int16 = byteHandler.readInt16(b, o)

        ' Status Array
        status.data = New List(Of StatData)
        For i = 0 To count - 1
            status.data.Add(StatData.parse(b, o))
        Next

        Return status
    End Function
End Class
