Imports System
Imports System.Net
Imports System.IO

Class NEW_TICK
    Public Shared tickId As Int32 'Int
    Public Shared tickTime As Int32
    Public Shared statuses As IList(Of Status)

    Public Shared Sub Main(incoming As Boolean, b As IList(Of Byte))
        Dim o As Integer = 0 ' byte offset

        ' Read the tickID
        tickId = byteHandler.readInt32(b, o)

        ' Read the tickTime
        tickTime = byteHandler.readInt32(b, o)

        ' Get the number of stuatuses
        Dim statusCount As Int16 = byteHandler.readInt16(b, o)

        Console.WriteLine("tickId: " & tickId)
        Console.WriteLine("tickTime: " & tickTime)
        Console.WriteLine("statusCount: " & statusCount)

        ' Build an arry of Statuses
        statuses = New List(Of Status)
        For i = 0 To statusCount - 1
            Dim stat As Status = New Status()
            stat = Status.parse(b, o)
            statuses.Add(stat)
        Next
    End Sub

    Public Shared Sub Client(buffer As IList(Of Byte))

    End Sub
    Public Shared Sub Server(buffer As IList(Of Byte))

    End Sub
End Class
