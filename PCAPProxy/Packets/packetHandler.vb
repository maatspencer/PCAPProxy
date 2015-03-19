Imports System
Imports System.IO
Imports System.Collections.Generic
Imports PcapDotNet.Core
Imports PcapDotNet.Packets
Public Class packetHandler
    Public Shared Sub Parse(packet As Packet)
        ' Check to see if the packet is more than just headers
        If packet.Buffer.Length <= 54 Then
            'Forward On
            Exit Sub
        End If

        'Define Byte Array
        Dim encryptedPacket As Byte() = New Byte(packet.Buffer.Length) {}
        For i = 0 To packet.Buffer.Length - 1
            encryptedPacket(i) = packet.Buffer(i)
        Next

        ' Open a Binary Reader
        Dim memStream As MemoryStream = New MemoryStream(encryptedPacket)
        Using bReader As New BinaryReader(memStream)

            ' Read the Ethernet Header
            Dim ethBytes As Byte() = bReader.ReadBytes(14)

            ' Read the IPv4 Header
            Dim IPv4Bytes As Byte() = bReader.ReadBytes(20)

            ' Read the TCP Header
            Dim srcPortBytes As Byte() = bReader.ReadBytes(2)
            byteArray.Endianess(srcPortBytes)
            Dim srcPort As UInt16 = BitConverter.ToUInt16(srcPortBytes, 0)
            '' Incoming?
            Dim incoming As Boolean
            Dim key As String
            If srcPort = 2050 Then
                incoming = True
                key = Properties.serverKey
            Else
                incoming = False
                key = Properties.clientKey
            End If

            Dim TCPBytes As Byte() = bReader.ReadBytes(18)
            '' The Header length equal to 20?
            If Hex(TCPBytes(10)).ToString <> "50" Then
                ' Forward on
                ' This may be the Policy Server
                Exit Sub
            End If

            ' Packet Length
            Dim lenBytes As Byte() = bReader.ReadBytes(4)
            byteArray.Endianess(lenBytes)
            Dim len As UInt32 = BitConverter.ToUInt32(lenBytes, 0)

            ' Packet Type
            Dim type As Integer = bReader.ReadByte()

            ' Packet Buffer
            Dim pBuffer As Byte() = New Byte(len) {}
            pBuffer = bReader.ReadBytes(len + 1)
            pBuffer = RC4.Main(pBuffer, key, False)

                ' Get Packet Name
                Dim name As Integer = Filter(type, incoming)

                ' Parse Variables
                Select Case name
                    Case 0
                        FAILURE.Main(incoming, pBuffer)
                    Case 1
                        CREATE_SUCCESS.Main(incoming, pBuffer)
                    Case 2
                        CREATE.Main(incoming, pBuffer)
                    Case 3
                        PLAYERSHOOT.Main(incoming, pBuffer)
                    Case 4
                        MOVE.Main(incoming, pBuffer)
                    Case 5
                        PLAYERTEXT.Main(incoming, pBuffer)
                    Case 6
                        TEXT.Main(incoming, pBuffer)
                    Case 7
                        SHOOT2.Main(incoming, pBuffer)
                    Case 8
                        DAMAGE.Main(incoming, pBuffer)
                    Case 9
                        UPDATE.Main(incoming, pBuffer)
                    Case 10
                        UPDATEACK.Main(incoming, pBuffer)
                    Case 11
                        NOTIFICATION.Main(incoming, pBuffer)
                    Case 12
                        NEW_TICK.Main(incoming, pBuffer)
                    Case 13
                        INVSWAP.Main(incoming, pBuffer)
                    Case 14
                        USEITEM.Main(incoming, pBuffer)
                    Case 15
                        SHOW_EFFECT.Main(incoming, pBuffer)
                    Case 16
                        HELLO.Main(incoming, pBuffer)
                    Case 17
                        GO_TO.Main(incoming, pBuffer)
                    Case 18
                        INVDROP.Main(incoming, pBuffer)
                    Case 19
                        INVRESULT.Main(incoming, pBuffer)
                    Case 20
                        RECONNECT.Main(incoming, pBuffer)
                    Case 21
                        PING.Main(incoming, pBuffer)
                    Case 22
                        PONG.Main(incoming, pBuffer)
                    Case 23
                        MAPINFO.Main(incoming, pBuffer)
                    Case 24
                        LOAD.Main(incoming, pBuffer)
                    Case 25
                        PIC.Main(incoming, pBuffer)
                    Case 26
                        SETCONDITION.Main(incoming, pBuffer)
                    Case 27
                        TELEPORT.Main(incoming, pBuffer)
                    Case 28
                        USEPORTAL.Main(incoming, pBuffer)
                    Case 29
                        DEATH.Main(incoming, pBuffer)
                    Case 30
                        BUY.Main(incoming, pBuffer)
                    Case 31
                        BUYRESULT.Main(incoming, pBuffer)
                    Case 32
                        AOE.Main(incoming, pBuffer)
                    Case 33
                        GROUNDDAMAGE.Main(incoming, pBuffer)
                    Case 34
                        PLAYERHIT.Main(incoming, pBuffer)
                    Case 35
                        ENEMYHIT.Main(incoming, pBuffer)
                    Case 36
                        AOEACK.Main(incoming, pBuffer)
                    Case 37
                        SHOOTACK.Main(incoming, pBuffer)
                    Case 38
                        OTHERHIT.Main(incoming, pBuffer)
                    Case 39
                        SQUAREHIT.Main(incoming, pBuffer)
                    Case 40
                        GOTOACK.Main(incoming, pBuffer)
                    Case 41
                        EDITACCOUNTLIST.Main(incoming, pBuffer)
                    Case 42
                        ACCOUNTLIST.Main(incoming, pBuffer)
                    Case 43
                        QUESTOBJID.Main(incoming, pBuffer)
                    Case 44
                        CHOOSENAME.Main(incoming, pBuffer)
                    Case 45
                        NAMERESULT.Main(incoming, pBuffer)
                    Case 46
                        CREATEGUILD.Main(incoming, pBuffer)
                    Case 47
                        CREATEGUILDRESULT.Main(incoming, pBuffer)
                    Case 48
                        GUILDREMOVE.Main(incoming, pBuffer)
                    Case 49
                        GUILDINVITE.Main(incoming, pBuffer)
                    Case 50
                        ALLYSHOOT.Main(incoming, pBuffer)
                    Case 51
                        SHOOT.Main(incoming, pBuffer)
                    Case 52
                        REQUESTTRADE.Main(incoming, pBuffer)
                    Case 53
                        TRADEREQUESTED.Main(incoming, pBuffer)
                    Case 54
                        TRADESTART.Main(incoming, pBuffer)
                    Case 55
                        CHANGETRADE.Main(incoming, pBuffer)
                    Case 56
                        TRADECHANGED.Main(incoming, pBuffer)
                    Case 57
                        ACCEPTTRADE.Main(incoming, pBuffer)
                    Case 58
                        CANCELTRADE.Main(incoming, pBuffer)
                    Case 59
                        TRADEDONE.Main(incoming, pBuffer)
                    Case 60
                        TRADEACCEPTED.Main(incoming, pBuffer)
                    Case 61
                        CLIENTSTAT.Main(incoming, pBuffer)
                    Case 62
                        CHECKCREDITS.Main(incoming, pBuffer)
                    Case 63
                        ESCAPE.Main(incoming, pBuffer)
                    Case 64
                        FILE.Main(incoming, pBuffer)
                    Case 65
                        INVITEDTOGUILD.Main(incoming, pBuffer)
                    Case 66
                        JOINGUILD.Main(incoming, pBuffer)
                    Case 67
                        CHANGEGUILDRANK.Main(incoming, pBuffer)
                    Case 68
                        PLAYSOUND.Main(incoming, pBuffer)
                    Case 69
                        GLOBAL_NOTIFICATION.Main(incoming, pBuffer)
                    Case 70
                        RESKIN.Main(incoming, pBuffer)
                    Case 79
                        ENTER_ARENA.Main(incoming, pBuffer)
                    Case Else
                        UNKNOWN.Main(incoming, pBuffer)
                End Select
        End Using
    End Sub
    Private Shared Function Filter(type As Integer, incoming As Boolean) As Integer
        Dim slotNumber As Integer = 1000
        For i = 0 To Properties.packetArray.Count - 1
            If type = Properties.packetArray(i).ToString.Split("=")(1) Then
                slotNumber = i
                Exit For
            End If
        Next
        Return slotNumber
    End Function
End Class
