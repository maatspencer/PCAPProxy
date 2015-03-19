Imports System.IO

Public Class StatData
    Public Property obf0() As Integer
    Public Property obf1() As Int32
    Public Property obf2() As String

    Public Shared Function parse(b As IList(Of Byte), ByRef o As Integer) As StatData
        Dim data As New StatData
        data.obf0 = byteArray.readByte(b, o)
        Console.WriteLine("obf0: " & data.obf0)

        If isUTFData(data.obf0) = True Then
            Console.WriteLine("wtf do I do here with a string of unknown length")
            Console.ReadKey()
        Else
            data.obf1 = byteArray.readInt32(b, o)
        End If

        Return data
    End Function

    Private Shared Function isUTFData(obf0 As Integer) As Boolean
        Select Case obf0
            Case 31 Or 62 Or 82 Or 38 Or 54
                Return True
            Case Else
                Return False
        End Select
    End Function
End Class
