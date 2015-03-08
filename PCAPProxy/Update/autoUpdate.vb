Imports System
Imports System.IO
Imports System.Net
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Threading.Tasks

Public Class autoUpdate
    ' Initialize Variables
    Private Shared Folder As String = AppDomain.CurrentDomain.BaseDirectory & "RABCDasm"
    Private Shared FILE_NAME As String
    Private Shared arytext As String()
    Private Shared Quote As String = """"

    ' Update Routine
    Public Shared Sub Main()
        Console.WriteLine("Downloading the latest Assembly Game Client...")
        getLatest()

        Console.WriteLine("Decompiling Client...")
        Decompile()

        Console.WriteLine("Mapping Packets...")
        Console.WriteLine()
        mapPackets()

        Console.WriteLine("Finding RC4 Keys...")
        Console.WriteLine()
        getRC4()

        Console.WriteLine("Cleaning up files...")
        cleanFiles()
    End Sub
    ' Find RC4 Keys
    Private Shared Sub getRC4()
        FILE_NAME = Folder & "\getRC4.bat"
        Array.Resize(arytext, 4)
        arytext(0) = "cd " & Folder
        arytext(1) = "findstr /s /m " & Quote & "rc4" & Quote & " *class.asasm >results4.txt"
        arytext(2) = "findstr /m /f:results1.txt " & Quote & "getCipher" & Quote & " *class.asasm >results5.txt"
        arytext(3) = "findstr /m /f:results5.txt " & Quote & "PLAYSOUND" & Quote & " *class.asasm >results6.txt"
        Create_Batch(arytext, FILE_NAME)

        'Find the smaller of the two packet classes
        Dim MyFile As String = System.IO.File.ReadAllText(Folder & "\results6.txt")
        MyFile = MyFile.Replace(Environment.NewLine, "")
        Dim NewFile As String = Folder & "\RC4keys.txt"
        Dim Count As Integer = 1
        Dim CurrentLine As String = ""
        Dim toggle As Boolean = False
        Dim writer As StreamWriter = New StreamWriter(NewFile, False)
        Dim keyTick As Integer = 0


        'Read for the last instance of returnvalue before Capabilities
        Using sr As StreamReader = New StreamReader(Folder & "\" & MyFile)
            CurrentLine = sr.ReadLine
            Do While (Not CurrentLine Is Nothing)

                If toggle = True Then
                    If CurrentLine.Contains("pushstring") Then
                        If keyTick = 0 Then
                            Properties.clientKey = CurrentLine.Split(Quote)(1)
                            writer.WriteLine("clientKey=" & Properties.clientKey)
                            keyTick += 1
                            toggle = False
                        Else
                            Properties.serverKey = CurrentLine.Split(Quote)(1)
                            writer.WriteLine("serverKey=" & Properties.serverKey)
                            keyTick += 1
                            toggle = False
                        End If
                    End If

                    If keyTick = 2 Then
                        Exit Do
                    End If
                End If

                If CurrentLine.Contains("rc4") Then
                    toggle = True
                End If

                CurrentLine = sr.ReadLine
                Count = Count + 1
            Loop
        End Using

        writer.Flush()
        writer.Close()

        My.Settings.serverKey = Properties.serverKey
        My.Settings.clientKey = Properties.clientKey
        My.Settings.Save()
        Console.WriteLine("clientKey=" & Properties.clientKey)
        Console.WriteLine("serverKey=" & Properties.serverKey)
        Console.WriteLine("Press any key to continue...")
        Console.ReadKey(True)
        Console.Clear()

    End Sub
    ' Get Packet ID's
    Private Shared Sub mapPackets()
        ' findstr CMD for packet class
        FILE_NAME = Folder & "\mapPackets.bat"
        Array.Resize(arytext, 4)
        arytext(0) = "cd " & Folder
        arytext(1) = "findstr /s /m " & Quote & "slotid 75" & Quote & " *class.asasm >results1.txt"
        arytext(2) = "findstr /m /f:results1.txt " & Quote & "chooseName" & Quote & " *class.asasm >results2.txt"
        arytext(3) = "findstr /m /f:results2.txt " & Quote & "keyTime_" & Quote & " *class.asasm >results3.txt"
        Create_Batch(arytext, FILE_NAME)

        'Find the smaller of the two packet classes
        Dim MyFile As String
        If System.IO.File.ReadAllLines(Folder & "\results3.txt")(0).Length > System.IO.File.ReadAllLines(Folder & "\results3.txt")(1).Length Then
            MyFile = System.IO.File.ReadAllLines(Folder & "\results3.txt")(0)
        Else
            MyFile = System.IO.File.ReadAllLines(Folder & "\results3.txt")(1)
        End If

        ' Variables
        MyFile = MyFile.Replace(Environment.NewLine, "")
        Dim NewFile As String = Folder & "\packetID.txt"
        Dim Count As Integer = 1
        Dim CurrentLine As String = ""
        Dim writer As StreamWriter = New StreamWriter(NewFile, False)

        ' Clear old packets
        Properties.packetArray.Clear()

        ' unnamed packets
        Dim tick As Integer = 0
        Dim tickArray(6) As String
        tickArray(0) = "SHOOT2"
        tickArray(1) = "UPDATEACK"
        tickArray(2) = "NEW_TICK"
        tickArray(3) = "SHOW_EFFECT"
        tickArray(4) = "CREATEGUILDRESULT"
        tickArray(5) = "SHOOT"
        tickArray(6) = "FILE"

        ' Write the Packets to a Text File and array list
        Using sr As StreamReader = New StreamReader(Folder & "\" & MyFile)
            CurrentLine = sr.ReadLine
            Do While (Not CurrentLine Is Nothing)

                If CurrentLine.Contains("slotid") And CurrentLine.Contains("Integer") Then
                    Dim name As String = CurrentLine.Split(Quote)(5)
                    Dim id As String = CurrentLine.Split("(")(5).Replace(") end", "")
                    If name.Contains("-") And tick <> 7 Then
                        name = tickArray(tick)
                        tick += 1
                    End If

                    writer.WriteLine(name & "=" & id)
                    Properties.packetArray.Add(name & "=" & id)
                End If

                CurrentLine = sr.ReadLine
                Count = Count + 1
            Loop
        End Using

        My.Settings.packets = Properties.packetArray
        My.Settings.Save()

        writer.Flush()
        writer.Close()

        For i = 0 To Properties.packetArray.Count - 1
            Console.WriteLine(Properties.packetArray(i).ToString.Split("=")(0) & " -> " & Properties.packetArray(i).ToString.Split("=")(1))
        Next
        Console.WriteLine("Press any key to continue...")
        Console.ReadKey(True)
        Console.Clear()
    End Sub
    ' Cleanup
    Private Shared Sub cleanFiles()
        My.Computer.FileSystem.DeleteFile(Folder & "/client.swf")
        My.Computer.FileSystem.DeleteFile(Folder & "/results1.txt")
        My.Computer.FileSystem.DeleteFile(Folder & "/results2.txt")
        My.Computer.FileSystem.DeleteFile(Folder & "/results3.txt")
        My.Computer.FileSystem.DeleteFile(Folder & "/results4.txt")
        My.Computer.FileSystem.DeleteFile(Folder & "/results5.txt")
        My.Computer.FileSystem.DeleteFile(Folder & "/results6.txt")
        My.Computer.FileSystem.DeleteFile(Folder & "/client-0.abc")
        My.Computer.FileSystem.DeleteFile(Folder & "/client-1.abc")
        My.Computer.FileSystem.DeleteDirectory(Folder & "/client-1", FileIO.DeleteDirectoryOption.DeleteAllContents)
    End Sub
    ' Get Newest Client
    Private Shared Sub getLatest()
        Using wc As New WebClient()
            Dim version = wc.DownloadString("http://www.realmofthemadgod.com/version.txt")
            Dim swf = "http://www.realmofthemadgod.com/AssembleeGameClient" + version + ".swf"
            wc.DownloadFile(swf, Folder & "/client.swf")
        End Using
    End Sub
    ' Decompile
    Private Shared Sub Decompile()
        Dim FILE_NAME As String = Folder & "\decompile.bat"
        Dim aryText(5) As String
        aryText(0) = "cd " & Folder
        aryText(1) = "swfdecompress client.swf"
        aryText(2) = "abcexport client.swf"
        aryText(3) = "rabcdasm client-1.abc"
        Create_Batch(aryText, FILE_NAME)
        'Process.Start(Folder & "/decAndExport.bat")
        'Process.Start(Folder & "/disassemble.bat").WaitForExit()
    End Sub
    ' Create And Run Batch Files
    Private Shared Sub Create_Batch(aryText() As String, FileName As String)
        Dim objWriter As New System.IO.StreamWriter(FileName, False)
        For i = 0 To aryText.Length - 1
            objWriter.WriteLine(aryText(i))
        Next
        objWriter.Close()

        Dim Process As New Process
        Dim ps As New ProcessStartInfo(FileName)
        ps.RedirectStandardError = True
        ps.RedirectStandardOutput = True
        ps.CreateNoWindow = True
        ps.WindowStyle = ProcessWindowStyle.Hidden
        ps.UseShellExecute = False
        Process.StartInfo = ps
        Process.Start()
        Process.WaitForExit()
    End Sub
End Class
