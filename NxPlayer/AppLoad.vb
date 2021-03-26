Imports System.Net.Sockets
Imports System.IO

Module Load_App
    Public Reprnum As Integer
    Public ListenNumber As Integer = 1855
    Dim G_AppName As String
    Dim TaskBarDir As String '= "N:\Nexus\AppWork\NxTaskBar\AppConfig"

    'Dim cUser As String = IO.File.ReadAllText("N:\Nexus\AppWork\Settings\CrrntUser\Name")
    'Public SystmColor As Color = Color.FromArgb(CInt(IO.File.ReadAllText("N:\Users\" & cUser & "\Configs\SysColr")))
    'Public SystmColor As Color = Color.FromArgb(-9868951)

    Sub Load_Me(ByVal AppName As String, ByVal AppIcon As Image, ByVal CPort As Integer)

        Port.Interval = 10
        G_AppName = AppName

        Dim Number_ As Integer = Val(IO.File.ReadAllText("N:\Nexus\AppWork\NxTaskBar\Process\ProcessCount"))

        Try

            If IO.Directory.Exists(TaskBarDir & "/" & AppName) = True Then

                IO.File.WriteAllText(TaskBarDir & "/" & AppName & "/Nmbr", CStr(Number_ + 1))
                IO.File.WriteAllText(TaskBarDir & "/" & AppName & "/Prt", CStr(CPort))
                IO.File.WriteAllText(TaskBarDir & "/" & AppName & "/Ld", "True")
                IO.File.WriteAllText(TaskBarDir & "/" & AppName & "/CMD", "")
                AppIcon.Save(TaskBarDir & "/" & AppName & "/Icon.png")

            Else
                IO.Directory.CreateDirectory(TaskBarDir & "/" & AppName)
                IO.File.WriteAllText(TaskBarDir & "/" & AppName & "/Nmbr", CStr(Number_ + 1))
                IO.File.WriteAllText(TaskBarDir & "/" & AppName & "/Prt", CStr(CPort))
                IO.File.WriteAllText(TaskBarDir & "/" & AppName & "/Ld", "True")
                IO.File.WriteAllText(TaskBarDir & "/" & AppName & "/CMD", "")
                AppIcon.Save(TaskBarDir & "/" & AppName & "/Icon.png")

            End If

        Catch ex As Exception

        End Try

        IO.File.WriteAllText("N:\Nexus\AppWork\NxTaskBar\Process\ProcessCount", Number_ + 1)

        Port.Enabled = True

    End Sub

    Dim WithEvents Port As New Timer

    Private Sub ListenPort(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Port.Tick
        Dim VisState As String = 0

        Try
            VisState = IO.File.ReadAllText(TaskBarDir & "/" & G_AppName & "/CMD")
        Catch ex As Exception

        End Try

        ListenPortx2()

        If VisState = "0" Then
            My.Application.ApplicationContext.MainForm.Close()
            IO.File.WriteAllText(TaskBarDir & "/" & G_AppName & "/CMD", "")

        ElseIf VisState = "1" Then
            My.Application.ApplicationContext.MainForm.Show()
            My.Application.ApplicationContext.MainForm.Focus()
            IO.File.WriteAllText(TaskBarDir & "/" & G_AppName & "/CMD", "")

        ElseIf VisState = "2" Then
            My.Application.ApplicationContext.MainForm.Hide()
            IO.File.WriteAllText(TaskBarDir & "/" & G_AppName & "/CMD", "")

        ElseIf VisState = "3" Then
            Ventana_emergente.Show()
            Ventana_emergente.Focus()
            IO.File.WriteAllText(TaskBarDir & "/" & G_AppName & "/CMD", "")

        ElseIf VisState = "4" Then
            Ventana_emergente.Hide()
            IO.File.WriteAllText(TaskBarDir & "/" & G_AppName & "/CMD", "")

        End If




    End Sub

    Dim localhost As Net.IPAddress = Net.Dns.GetHostEntry("localhost").AddressList(1)
    Dim Listener As New TcpListener(localhost, ListenNumber)
    Dim client As TcpClient
    Dim message As String

    Sub ListenPortx2()

        Try

            Listener.Start()

            If Listener.Pending = True Then

                message = ""
                client = Listener.AcceptTcpClient
                Dim streamr As New StreamReader(client.GetStream())
                While streamr.Peek > -1
                    message = message + Convert.ToChar(streamr.Read()).ToString
                End While

                CMD(message)

            End If


        Catch ex As Exception

        End Try

    End Sub

    Public lCMD As String = ""

    Sub CMD(ByVal Line As String)



        If lCMD = Line And Form1.AxWindowsMediaPlayer1.playState = WMPLib.WMPPlayState.wmppsPlaying Then

            Form1.Show()
            Form1.Focus()

            Exit Sub

        ElseIf CBool(lCMD = Line) = True And Not Form1.AxWindowsMediaPlayer1.playState = WMPLib.WMPPlayState.wmppsPlaying Then

           
            If Line.StartsWith("+") Then

                Form1.AxWindowsMediaPlayer1.Ctlcontrols.play()
                Form1.AxWindowsMediaPlayer1.Visible = False
                Ventana_emergente.PictureBox1.BackgroundImage = My.Resources.note_38578_640

                Form1.Show()
                Form1.Focus()

            ElseIf Line.StartsWith("-") Then

                Form1.AxWindowsMediaPlayer1.Ctlcontrols.play()
                Form1.AxWindowsMediaPlayer1.Visible = True
                Form1.AxWindowsMediaPlayer1.Location = Form1.PictureBox2.Location
                Form1.AxWindowsMediaPlayer1.Size = Form1.PictureBox2.Size
                Ventana_emergente.PictureBox1.BackgroundImage = My.Resources.videoT

                Form1.Show()
                Form1.Focus()

            End If

        Else

            If Line.StartsWith("+") Then
                Form1.AxWindowsMediaPlayer1.URL = Line.Replace("+", "")
                Form1.AxWindowsMediaPlayer1.Visible = False
                Ventana_emergente.PictureBox1.BackgroundImage = My.Resources.note_38578_640

                ListaDrep.LoadToPlayList(Line.Replace("+", ""))
                Form1.Show()
                Form1.Focus()

            ElseIf Line.StartsWith("-") Then

                Form1.AxWindowsMediaPlayer1.URL = Line.Replace("-", "")
                Form1.AxWindowsMediaPlayer1.Visible = True
                Form1.AxWindowsMediaPlayer1.Location = Form1.PictureBox2.Location
                Form1.AxWindowsMediaPlayer1.Size = Form1.PictureBox2.Size
                Ventana_emergente.PictureBox1.BackgroundImage = My.Resources.videoT

                ListaDrep.LoadToPlayList(Line.Replace("-", ""))
                Form1.Show()
                Form1.Focus()


            End If

        End If

        lCMD = Line

    End Sub


    Sub Close_Me(ByVal AppName As String)

        Dim Cont As String = IO.File.ReadAllText("N:\Nexus\AppWork\NxTaskBar\Process\ProcessCount")

        Try

            If IO.Directory.Exists(TaskBarDir & "/" & AppName) = True Then

                IO.File.WriteAllText(TaskBarDir & "/" & AppName & "/Nmbr", CStr(0))

                IO.File.WriteAllText(TaskBarDir & "/" & AppName & "/Ld", "False")


            Else
                IO.Directory.CreateDirectory(TaskBarDir & "/" & AppName)

                IO.File.WriteAllText(TaskBarDir & "/" & AppName & "/Nmbr", CStr(0))

                IO.File.WriteAllText(TaskBarDir & "/" & AppName & "/Ld", "False")


            End If

        Catch ex As Exception

        End Try

        IO.File.WriteAllText("N:\Nexus\AppWork\NxTaskBar\Process\ProcessCount", Val(Cont - 1))

    End Sub


    Sub Speak(ByVal Owner As String, ByVal CMD As String, ByVal tipe As String)

        Try
            client = New TcpClient("localhost", 2000)
            Dim streamw As New StreamWriter(client.GetStream())
            streamw.Write(Owner & "|" & CMD & ";" & tipe)
            streamw.Flush()
        Catch ex As Exception

        End Try


    End Sub

End Module