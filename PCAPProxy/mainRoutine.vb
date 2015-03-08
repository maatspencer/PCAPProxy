Imports System
Imports System.Collections.Generic
Imports PcapDotNet.Core
Imports System.Net
Imports System.Text

Module mainRoutine
    '' Public Variables
    Public allDevices As IList(Of LivePacketDevice) = LivePacketDevice.AllLocalMachine

    Sub Main()
        ' Console Window Setup
        Console.Title = "ROTMG Proxy.NET"
        Console.ForegroundColor = ConsoleColor.White
        Console.WindowLeft = 0
        Console.WindowTop = 0

        Console.WriteLine("ROTMG Proxy Created by maat7043")
        Console.WriteLine("Version 1.2 Updated 2015.3.8")
        Console.WriteLine("")

        ' Check to See if Winpcap is setup correctly
        If allDevices.Count = 0 Then
            Console.WriteLine("Double check that winpcap is installed!")
            Console.ReadKey()
            Return
        End If

        ' Load Properties
        Properties.Load()

        ' Check to see if you are up to date
        Using wc As New WebClient()
            Dim version = wc.DownloadString("http://www.realmofthemadgod.com/version.txt")
            If Not version = Properties.AGCVersion Then
                Properties.AGCVersion = version
                My.Settings.AGCVersion = version
                My.Settings.Save()
                ' Update Routine
                autoUpdate.Main()
            Else
                Console.WriteLine("Assembly Game Client: " & version)
                Console.WriteLine("Release Version: " & Properties.versionNumber)
                Console.Write("System is up to date preass any key to continue")
                Console.ReadKey()
            End If
        End Using

        ' Obtain Advanced Network information about Network Devices
        deviceAdvancedInformation.Main()

        ' Begin Listening
        openListener.Main()

        Console.ReadKey()
    End Sub

End Module
