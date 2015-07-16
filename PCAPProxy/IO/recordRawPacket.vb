Imports System.Text
Imports System.IO

Public Class recordRawPacket
    Public Shared Sub toFile(packetType As String, buffer As IList(Of Byte))
        ' Create a time stamp and file path for the packet
        Dim today As String = Date.Today
        today = today.Replace("/", ".")

        If My.Computer.FileSystem.DirectoryExists(System.AppDomain.CurrentDomain.BaseDirectory & "/savedPackets/" & packetType) = False Then
            My.Computer.FileSystem.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory & "/savedPackets/" & packetType)
        End If

        Dim path As String = System.AppDomain.CurrentDomain.BaseDirectory & "/savedPackets/" & packetType & "/" & today & "_" & DateAndTime.Now.Hour & "_" & DateAndTime.Now.Minute & "_" & DateAndTime.Now.Second & ".txt"

        '' Build the text
        Dim s As New StringBuilder

        ' Packet Name
        s.AppendLine(packetType)

        ' Write bytes 8 to a line teb delimeted
        For i = 0 To buffer.Count - 1
            s.Append(vbTab & buffer(i))
            If (i + 1) Mod 8 = 0 Then
                s.AppendLine()
            End If
        Next

        ' Write to file
        Using outfile As New StreamWriter(path)
            outfile.Write(s.ToString())
        End Using
    End Sub
End Class
