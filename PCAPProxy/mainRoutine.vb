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
                Console.Write("System is up to date preass any key to continue or O for options.")
                Dim key As ConsoleKeyInfo = Console.ReadKey
                If key.Key = ConsoleKey.O Then
chooseOption:
                    Console.Clear()
                    Console.WriteLine("--------------------------------")
                    Console.WriteLine("ROTMG Proxy.Net Options Screen")
                    Console.WriteLine("--------------------------------")
                    Console.WriteLine("[A] Rerun update routine")
                    Console.WriteLine("[B] Change Network Interface")
                    Console.WriteLine("[C] Print Current Packets Definitions and Keys")
                    Console.WriteLine("[D] Cancel")
                    Dim oKey As ConsoleKeyInfo = Console.ReadKey()
                    Select Case oKey.Key
                        Case ConsoleKey.A
                            Properties.AGCVersion = version
                            My.Settings.AGCVersion = version
                            My.Settings.Save()
                            ' Update Routine
                            autoUpdate.Main()
                        Case ConsoleKey.B
                            Console.Clear()
                            Properties.interfaceNumber = 10000
                        Case ConsoleKey.C
                            Console.Clear()

                            ' RC4 Keys
                            Console.WriteLine("clientKey=" & Properties.clientKey)
                            Console.WriteLine("serverKey=" & Properties.serverKey)
                            Console.WriteLine("Press any key to continue...")
                            Console.ReadKey(True)
                            Console.Clear()

                            ' Packet Definitions
                            For i = 0 To Properties.packetArray.Count - 1
                                Console.WriteLine(Properties.packetArray(i).ToString.Split("=")(0) & " -> " & Properties.packetArray(i).ToString.Split("=")(1))
                            Next
                            Console.WriteLine("Press any key to continue...")
                            Console.ReadKey(True)
                            Console.Clear()

                            Console.WriteLine("Please note that .txt versions of this information is stored in your apps root")
                            Console.WriteLine("Press any key to continue...")
                            Console.ReadKey(True)
                            Console.Clear()
                        Case ConsoleKey.D
                            ' Proceed Normally
                        Case Else
                            Console.Clear()
                            Console.WriteLine("Not a valid key selection.")
                            Threading.Thread.Sleep(2000)
                            GoTo chooseOption
                    End Select

                End If
            End If
        End Using

        ' Obtain Advanced Network information about Network Devices
        deviceAdvancedInformation.Main()

        ' Begin Listening
        openListener.Main()

        Console.ReadKey()
    End Sub

End Module
