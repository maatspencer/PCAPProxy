Imports System
Imports System.IO
Imports System.Net
Imports System.Collections.Generic
Imports PcapDotNet.Core
Imports PcapDotNet.Packets
Imports System.Threading
Imports System.Threading.Tasks

Public Class openListener
    Public Shared Sub Main()
        ' Select the desired adapter Adapter
        Dim deviceIndex As Integer = 0
        If Properties.interfaceNumber = 10000 Then
            Do
                Console.WriteLine("Enter the interface number (1-" & allDevices.Count & "):")
                Dim deviceIndexString As String = Console.ReadLine()
                If Not Integer.TryParse(deviceIndexString, deviceIndex) OrElse deviceIndex < 1 OrElse deviceIndex > allDevices.Count Then
                    deviceIndex = 0
                End If
            Loop While deviceIndex = 0

            ' Cache the Correct Network interface
            My.Settings.InterfaceNum = deviceIndex
            My.Settings.Save()
        Else
            deviceIndex = 6 'Properties.interfaceNumber
        End If

        ' Take the selected adapter
        Dim selectedDevice As PacketDevice = allDevices(deviceIndex - 1)

        ' Open the device
        ' portion of the packet to capture
        ' 65536 guarantees that the whole packet will be captured on all the link layers
        ' promiscuous mode
        ' 1 second timeout
        Using comminicator As PacketCommunicator = selectedDevice.Open(65536, PacketDeviceOpenAttributes.Promiscuous, 1000)
            Console.Clear()
            Console.WriteLine("Listening on Port 2050...")

            ' filter traffic by port
            comminicator.SetFilter("port " & Properties.listenPort)

            ' Create a new thread for each packet
            Dim t As Thread = New Thread(AddressOf packetThreading)
            t.Start(comminicator)
            t.Join()
        End Using
    End Sub

    Private Shared Sub packetThreading(communicator As PacketCommunicator)
        ' Capture incoming Packets
        ' Each packet is handled on its own thread
        communicator.ReceivePackets(0, AddressOf packetHandler.Parse)
    End Sub
End Class
