Imports System
Imports System.Net
Imports System.IO

Class NEW_TICK
    Public Shared tickId As Int32 'Int
    Public Shared tickTime As Int32
    Public Shared statuses() As Status

    Public Shared Sub Main(incoming As Boolean, buffer As Byte())
        ' Open a Binary Reader
        Dim memStream As MemoryStream = New MemoryStream(buffer)
        Using b As New BinaryReader(memStream)

            ' Read the tickID
            tickId = b.ReadInt32
            ' Read the tickTime
            tickTime = b.ReadInt32

            ' Get the number of stuatuses
            Dim statusCount As Int16 = b.ReadInt16

            ' Build an arry of Statuses
            For i = 0 To statusCount - 1
                Dim stat As Status = New Status()
                stat = Status.parse(b)
                statuses(i) = stat
            Next
        End Using

        Console.WriteLine("Success")
    End Sub
    Public Shared Sub Client(buffer As Byte())

    End Sub
    Public Shared Sub Server(buffer As Byte())

    End Sub
End Class
