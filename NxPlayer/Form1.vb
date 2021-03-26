Imports NxPlayer.Ventana_emergente

Public Class Form1

   
    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        ListaDrep.Close()
        'Try

        '    IO.File.WriteAllText("N:\Nexus\AppWork\NxReproductor/&State}", "Stopped")
        '    IO.File.WriteAllText("N:\Nexus\AppWork\NxReproductor/IsRun", "False")

        'Catch ex As Exception
        '    IO.File.WriteAllText("N:\Nexus\AppWork\NxReproductor/&State}", "Stopped")
        'End Try


    End Sub


    

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Listener.Start()

        ListaDrep.Show()
        Ventana_emergente.Show()
        ListaDrep.Hide()
        Ventana_emergente.Hide()
        TrackBar2.Value = AxWindowsMediaPlayer1.settings.volume
        'IO.File.WriteAllText("N:\Nexus\AppWork\NxReproductor/IsRun", "True")

        'Load_Me("NxPlayer", PictureBox1.Image, ListenNumber)
        'MyIconInTaskbar()
        'Read_()


    End Sub


   

    Dim changer As String = Nothing

    Sub Read_()

        Dim fmusic As String = ""
        Dim Bucle As String = ""
        Dim Tmedia As String = ""

        Try
            fmusic = IO.File.ReadAllText("N:\Nexus\AppWork\NxReproductor/&play&")
            Bucle = IO.File.ReadAllText("N:\Nexus\AppWork\NxReproductor/&bucle&")
            Tmedia = IO.File.ReadAllText("N:\Nexus\AppWork\NxReproductor/TMedia")
        Catch ex As Exception

        End Try



        If Bucle = "True" Then
            If changer = fmusic Then

                Exit Sub

            ElseIf fmusic = "" Then
                AxWindowsMediaPlayer1.Ctlcontrols.stop()
            Else
                AxWindowsMediaPlayer1.URL = fmusic
                changer = fmusic
                IO.File.WriteAllText("N:\Nexus\AppWork\NxReproductor/&bucle&", "False")

                If Tmedia = "Video" Then
                    AxWindowsMediaPlayer1.Visible = True
                    AxWindowsMediaPlayer1.Location = PictureBox2.Location
                    AxWindowsMediaPlayer1.Size = PictureBox2.Size
                    Ventana_emergente.PictureBox1.BackgroundImage = My.Resources.videoT

                Else
                    AxWindowsMediaPlayer1.Visible = False
                    Ventana_emergente.PictureBox1.BackgroundImage = My.Resources.note_38578_640

                End If
                Me.Focus()
            End If
        End If





    End Sub

    Private Sub Label5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label5.Click

        If maximised = True Then
            maximised = False

        End If

        'Close_Me("NxPlayer")

        Me.Close()

    End Sub




    'Activar reproduccion aleatoria (True)


    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        'Dim ElabelText As String

        Try
            TrackBar1.Maximum = AxWindowsMediaPlayer1.Ctlcontrols.currentItem.duration
            Label1.Text = AxWindowsMediaPlayer1.Ctlcontrols.currentPositionString
            Label2.Text = AxWindowsMediaPlayer1.Ctlcontrols.currentItem.durationString
            'ElabelText = AxWindowsMediaPlayer1.Ctlcontrols.currentItem.durationString
            TrackBar1.Value = AxWindowsMediaPlayer1.Ctlcontrols.currentPosition
            Label3.Left = Panel6.Width / 2 - Label3.Width / 2
        Catch ex As Exception
            Exit Try
        End Try


        'If AxWindowsMediaPlayer1.Ctlcontrols.currentPositionString >= ElabelText Then


        '    'If Aleatorio = False Then
        '    '    Try
        '    '        ListaDrep.ListView1.Items(ListaDrep.ListView1.SelectedItems(0).Index + 1).Selected = True
        '    '        AxWindowsMediaPlayer1.URL = ListaDrep.ListView1.SelectedItems(0).ToolTipText
        '    '    Catch ex As Exception

        '    '    End Try

        '    'Else

        '    '    Try
        '    '        Dim a = ListaDrep.ListView1.Items(GetNumR).ToolTipText
        '    '        AxWindowsMediaPlayer1.URL = a
        '    '    Catch ex As Exception

        '    '    End Try
        '    'End If


        'End If



    End Sub

    Function GetNumR() As Integer

        Dim x As Integer
        Dim ToR As Integer

        Randomize()

        For x = 0 To ListaDrep.ListView1.Items.Count
            ToR = Rnd() * ListaDrep.ListView1.Items.Count

        Next

        Return ToR
    End Function

    Private Sub AxWindowsMediaPlayer1_MediaChange(ByVal sender As Object, ByVal e As AxWMPLib._WMPOCXEvents_MediaChangeEvent) Handles AxWindowsMediaPlayer1.MediaChange

        Dim UrlWMP As String

        If ListaDrep.ListView1.Items(Reprnum).Tag = "Audio" Then
            AxWindowsMediaPlayer1.Visible = False
            Ventana_emergente.PictureBox1.BackgroundImage = My.Resources.note_38578_640
            AxWindowsMediaPlayer1.Ctlcontrols.play()
        ElseIf ListaDrep.ListView1.Items(Reprnum).Tag = "Video" Then
            AxWindowsMediaPlayer1.Visible = True
            AxWindowsMediaPlayer1.Location = PictureBox2.Location
            AxWindowsMediaPlayer1.Size = PictureBox2.Size
            Ventana_emergente.PictureBox1.BackgroundImage = My.Resources.videoT
            AxWindowsMediaPlayer1.Ctlcontrols.play()
        Else

            Label3.Text = "<Error! No media file>"
            Ventana_emergente.Label1.Text = "<Error! No media file>"
            AxWindowsMediaPlayer1.Ctlcontrols.stop()
        End If


        Try

            UrlWMP = AxWindowsMediaPlayer1.URL
            Dim mp3 As New ID3TagLibrary.MP3File(UrlWMP)
            Dim Mp3Image As Image = mp3.Tag2.Artwork(1)

            Dim TrackName As String = AxWindowsMediaPlayer1.currentMedia.getItemInfo("Name")

            If TrackName = "" Then
                TrackName = AxWindowsMediaPlayer1.currentMedia.name
            End If

            If Mp3Image Is Nothing Then
                PictureBox2.Image = My.Resources.NxMusicIII
                Label3.Text = TrackName
            Else
                PictureBox2.Image = Mp3Image
                Ventana_emergente.PictureBox1.Image = Mp3Image
                Label3.Text = TrackName
            End If


        Catch ex As Exception

        End Try


    End Sub

    Dim Playing As Boolean = False
    Public Mm As Boolean = False

    Private Sub AxWindowsMediaPlayer1_PlayStateChange(ByVal sender As System.Object, ByVal e As AxWMPLib._WMPOCXEvents_PlayStateChangeEvent) Handles AxWindowsMediaPlayer1.PlayStateChange

        Try
            If AxWindowsMediaPlayer1.playState = WMPLib.WMPPlayState.wmppsPlaying Then
                Mm = True
                Timer1.Start()
                Playing = True
                PictureBox5.Image = My.Resources.PausaN
                Ventana_emergente.PictureBox5.Image = My.Resources.PausaN
                'IO.File.WriteAllText("N:\Nexus\AppWork\NxReproductor/&State}", "Play")
                Reprnum = ListaDrep.ListView1.SelectedItems(0).Index

            ElseIf AxWindowsMediaPlayer1.playState = WMPLib.WMPPlayState.wmppsStopped Then
                If Mm = True Then

                    If ListRef = 1 Then
                        'MsgBox(ListaDrep.ListView1.Items(Reprnum).Index + " " + ListaDrep.ListView1.Items.Count - 1)
                        Try
                            If ListaDrep.ListView1.Items(Reprnum).Index = ListaDrep.ListView1.Items.Count - 1 Then
                                '______________________________________________________________________________________________________
                                AxWindowsMediaPlayer1.Ctlcontrols.pause()
                                AxWindowsMediaPlayer1.URL = "--"

                            Else
                                Reprnum = Reprnum + 1

                                Try
                                    ListaDrep.ListView1.Items(Reprnum).Selected = True
                                    AxWindowsMediaPlayer1.URL = ListaDrep.ListView1.Items(Reprnum).ToolTipText

                                    'Dim xR As New Threading.Thread(AddressOf RunNotifier)
                                    'xR.Start()

                                Catch ex As Exception

                                End Try


                            End If



                        Catch ex As Exception

                        End Try

                        '_________________________________________________________________________________________________________________


                    ElseIf ListRef = 2 Then

                        If ListaDrep.ListView1.Items(Reprnum).Index = ListaDrep.ListView1.Items.Count - 1 Then

                            Try
                                Reprnum = 0
                                ListaDrep.ListView1.Items(Reprnum).Selected = True
                                AxWindowsMediaPlayer1.URL = ListaDrep.ListView1.Items(Reprnum).ToolTipText

                                'Dim xR As New Threading.Thread(AddressOf RunNotifier)
                                'xR.Start()

                            Catch ex As Exception

                            End Try

                        Else

                            Reprnum = Reprnum + 1
                            '______________________________________________________________________________________________________

                            Try
                                ListaDrep.ListView1.Items(Reprnum).Selected = True
                                AxWindowsMediaPlayer1.URL = ListaDrep.ListView1.Items(Reprnum).ToolTipText

                                'Dim xR As New Threading.Thread(AddressOf RunNotifier)
                                'xR.Start()

                            Catch ex As Exception

                            End Try
                        End If


                    ElseIf ListRef = 3 Then 'Repeat playing

                        ListaDrep.ListView1.Items(ListaDrep.ListView1.Items(Reprnum).Index).Selected = True
                        AxWindowsMediaPlayer1.URL = ListaDrep.ListView1.Items(Reprnum).ToolTipText

                        'Dim xR As New Threading.Thread(AddressOf RunNotifier)
                        'xR.Start()

                    ElseIf ListRef = 4 Then 'random playing


                        Try
                            Dim a = GetNumR()
                            Reprnum = a

                            ListaDrep.ListView1.Items(Reprnum).Selected = True
                            AxWindowsMediaPlayer1.URL = ListaDrep.ListView1.Items(Reprnum).ToolTipText

                            'Dim xR As New Threading.Thread(AddressOf RunNotifier)
                            'xR.Start()

                        Catch ex As Exception

                        End Try
                    End If

                    AxWindowsMediaPlayer1.Ctlcontrols.play()

                    Mm = False

                End If

            Else
                Timer1.Stop()
                Playing = False
                PictureBox5.Image = My.Resources.PlayN
                Ventana_emergente.PictureBox5.Image = My.Resources.PlayN
                'IO.File.WriteAllText("N:\Nexus\AppWork\NxReproductor/&State}", "Stopped")
            End If
        Catch ex As Exception

        End Try




    End Sub

    Sub RunNotifier()
        'Speak("Notifier", "N:\Resourses\CDM.png-NxPlayer+Reproduciendo..." & "^" & Label3.Text, "C")
    End Sub

    Private Sub TrackBar1_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TrackBar1.MouseDown
        Timer1.Stop()
    End Sub

    Private Sub TrackBar1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TrackBar1.MouseUp
        Timer1.Start()

    End Sub

    Private Sub TrackBar1_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar1.Scroll
        AxWindowsMediaPlayer1.Ctlcontrols.currentPosition = TrackBar1.Value
    End Sub

    Dim lSize As Point
    Dim maximised As Boolean = False

    Private Sub Label6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label6.Click

        lSize = Me.Size

        Dim lX As Integer = My.Computer.Screen.WorkingArea.Width
        Dim lY As Integer = My.Computer.Screen.WorkingArea.Height

        Me.Left = 0
        Me.Top = 0

        Me.Width = lX
        Me.Height = lY

        ListaDrep.CheckBox1.Checked = True


        ListaDrep.Left = lX - ListaDrep.Width
        ListaDrep.BringToFront()

        maximised = True
    End Sub

    Private Sub Label7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label7.Click
        Me.Hide()
    End Sub

    Private Sub PictureBox3_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)

        PictureBox3.Image = My.Resources.BackO
        PictureBox4.Image = My.Resources.NextN

        If Playing = True Then
            PictureBox5.Image = My.Resources.PausaN
        Else
            PictureBox5.Image = My.Resources.PlayN

        End If

    End Sub

    Private Sub PictureBox4_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)

        PictureBox4.Image = My.Resources.NextO
        PictureBox3.Image = My.Resources.BackN

        If Playing = True Then
            PictureBox5.Image = My.Resources.PausaN
        Else
            PictureBox5.Image = My.Resources.PlayN

        End If


    End Sub

    Private Sub PictureBox5_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        PictureBox3.Image = My.Resources.BackN
        PictureBox4.Image = My.Resources.NextN

        If Playing = True Then
            PictureBox5.Image = My.Resources.PausaO
        Else
            PictureBox5.Image = My.Resources.PlayO

        End If

    End Sub


    Sub DefaulImage()

        PictureBox3.Image = My.Resources.BackN
        PictureBox4.Image = My.Resources.NextN

        If Playing = True Then
            PictureBox5.Image = My.Resources.PausaN
        Else
            PictureBox5.Image = My.Resources.PlayN

        End If


    End Sub


    Private Sub PictureBox5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox5.Click
        If AxWindowsMediaPlayer1.playState = WMPLib.WMPPlayState.wmppsPlaying Then

            AxWindowsMediaPlayer1.Ctlcontrols.pause()

        Else
            AxWindowsMediaPlayer1.Ctlcontrols.play()


        End If

    End Sub

    Private Sub Panel5_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        DefaulImage()
    End Sub

    Private Sub TrackBar1_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        DefaulImage()
    End Sub


    '________________________________________________
    Private aaa As Boolean = False
    Private MouseX As Integer
    Private MouseY As Integer
    '_________________________________________________

    Private Sub Panel1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel1.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            aaa = True
            MouseX = e.X
            MouseY = e.Y

        End If
    End Sub

    Private Sub Panel1_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel1.MouseMove
        If aaa = True Then
            Dim tmp As Point = New Point

            tmp.X = Me.Location.X + (e.X - MouseX)
            tmp.Y = Me.Location.Y + (e.Y - MouseY)
            Me.Location = tmp
            tmp = Nothing

            If maximised = True Then

                maximised = False
                Me.Size = New Drawing.Size(lSize)
               
            End If

        End If

    End Sub

    Private Sub Panel1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel1.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Left Then
            aaa = False

        End If
    End Sub

    '________________________________________________________________________
    Dim x As Integer, y As Integer, a As Integer = x, b As Integer = y
    Private XY As Point
    '________________________________________________________________________


    Private Sub PictureBox6_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox6.MouseDown
        XY.X = CInt(CLng(x))
        XY.Y = CInt(CLng(y))

        ListaDrep.Timer1.Start()
    End Sub

    Private Sub PictureBox6_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox6.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Right Or e.Button = Windows.Forms.MouseButtons.Left Then
            'redimensionamos el ancho
            If (Me.Width + (x + e.X)) > 0 Then

                Me.Width = Me.Width + (x + e.X)
            End If
            'redimensionams el alto
            If (Me.Height + (y + e.Y)) > 0 Then

                Me.Height = Me.Height + (y + e.Y)
            End If
        End If

        
    End Sub


    Private Sub PictureBox7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox7.Click
        ListaDrep.Show()
        ListaDrep.BringToFront()
    End Sub



    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox8.Click

        If Ventana_emergente.mute = False Then
            AxWindowsMediaPlayer1.settings.mute = True
            Ventana_emergente.mute = True
            PictureBox8.Image = My.Resources.NotSound

            Ventana_emergente.PictureBox2.Image = My.Resources.NotSound
        Else

            AxWindowsMediaPlayer1.settings.mute = False
            Ventana_emergente.mute = False
            PictureBox8.Image = My.Resources.Sound
            Ventana_emergente.PictureBox2.Image = My.Resources.Sound
        End If

    End Sub



    Dim Ref As Integer = 0

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Ref = Ref + 1

        If Ref = 5 Then
            TrackBar2.Visible = False
            PictureBox9.Visible = True
            Ref = 0
            Timer2.Stop()

        End If


    End Sub

    Private Sub PictureBox9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox9.Click
        Timer2.Start()
        PictureBox9.Visible = False
        TrackBar2.Visible = True
    End Sub

    Private Sub TrackBar2_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar2.Scroll
        Ref = 0
        AxWindowsMediaPlayer1.settings.volume = TrackBar2.Value

    End Sub

    Private Sub PictureBox3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox3.Click
        
        If ListRef = 1 Then

            Try
                If ListaDrep.ListView1.Items(Reprnum).Index = ListaDrep.ListView1.Items.Count - 1 Then

                    '______________________________________________________________________________________________________
                    AxWindowsMediaPlayer1.URL = Nothing

                Else
                    Reprnum = Reprnum - 1

                    Try
                        ListaDrep.ListView1.Items(Reprnum).Selected = True
                        AxWindowsMediaPlayer1.URL = ListaDrep.ListView1.Items(Reprnum).ToolTipText

                        
                    Catch ex As Exception

                    End Try


                End If



            Catch ex As Exception

            End Try

            '_________________________________________________________________________________________________________________


        ElseIf ListRef = 2 Then


            If ListaDrep.ListView1.Items(Reprnum).Index = 0 Then

                Try

                    ListaDrep.ListView1.Items(ListaDrep.ListView1.Items.Count - 1).Selected = True
                    AxWindowsMediaPlayer1.URL = ListaDrep.ListView1.Items(ListaDrep.ListView1.Items.Count - 1).ToolTipText

                Catch ex As Exception

                End Try

            Else

                Reprnum = Reprnum - 1
                '______________________________________________________________________________________________________

                Try
                    ListaDrep.ListView1.Items(Reprnum).Selected = True
                    AxWindowsMediaPlayer1.URL = ListaDrep.ListView1.Items(Reprnum).ToolTipText


                Catch ex As Exception

                End Try
            End If


        ElseIf ListRef = 3 Then

            ListaDrep.ListView1.Items(ListaDrep.ListView1.Items(Reprnum).Index).Selected = True
            AxWindowsMediaPlayer1.URL = ListaDrep.ListView1.Items(Reprnum).ToolTipText

        ElseIf ListRef = 4 Then


            Try
                Dim a = GetNumR()
                Reprnum = a

                ListaDrep.ListView1.Items(Reprnum).Selected = True
                AxWindowsMediaPlayer1.URL = ListaDrep.ListView1.Items(Reprnum).ToolTipText

            Catch ex As Exception

            End Try
        End If

        AxWindowsMediaPlayer1.Ctlcontrols.play()

        Mm = False


    End Sub


          
    Private Sub PictureBox4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox4.Click

        If ListRef = 1 Then

            Try
                If ListaDrep.ListView1.Items(Reprnum).Index = ListaDrep.ListView1.Items.Count - 1 Then

                    '______________________________________________________________________________________________________
                    AxWindowsMediaPlayer1.URL = Nothing

                Else
                    Reprnum = Reprnum + 1

                    Try
                        ListaDrep.ListView1.Items(Reprnum).Selected = True
                        AxWindowsMediaPlayer1.URL = ListaDrep.ListView1.Items(Reprnum).ToolTipText



                    Catch ex As Exception

                    End Try


                End If

            Catch ex As Exception

            End Try

            '_________________________________________________________________________________________________________________


        ElseIf ListRef = 2 Then


            If ListaDrep.ListView1.Items(Reprnum).Index = ListaDrep.ListView1.Items.Count - 1 Then

                Try

                    ListaDrep.ListView1.Items(0).Selected = True
                    AxWindowsMediaPlayer1.URL = ListaDrep.ListView1.Items(0).ToolTipText

                Catch ex As Exception

                End Try

            Else

                Reprnum = Reprnum + 1
                '______________________________________________________________________________________________________

                Try
                    ListaDrep.ListView1.Items(Reprnum).Selected = True
                    AxWindowsMediaPlayer1.URL = ListaDrep.ListView1.Items(Reprnum).ToolTipText


                Catch ex As Exception

                End Try
            End If


        ElseIf ListRef = 3 Then

            ListaDrep.ListView1.Items(ListaDrep.ListView1.Items(Reprnum).Index).Selected = True
            AxWindowsMediaPlayer1.URL = ListaDrep.ListView1.Items(Reprnum).ToolTipText

        ElseIf ListRef = 4 Then


            Try
                Dim a = GetNumR()
                Reprnum = a

                ListaDrep.ListView1.Items(Reprnum).Selected = True
                AxWindowsMediaPlayer1.URL = ListaDrep.ListView1.Items(Reprnum).ToolTipText

            Catch ex As Exception

            End Try
        End If

        AxWindowsMediaPlayer1.Ctlcontrols.play()

        Mm = False


    End Sub

    Public ListRef As Integer = 2

    Private Sub PictureBox10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox10.Click
1:

        ListRef = ListRef + 1

        If ListRef = 1 Then

            Ventana_emergente.PictureBox6.Image = My.Resources.ListOne
            PictureBox10.Image = My.Resources.ListOne

        ElseIf ListRef = 2 Then

            Ventana_emergente.PictureBox6.Image = My.Resources.RepeatAll
            PictureBox10.Image = My.Resources.RepeatAll

        ElseIf ListRef = 3 Then

            Ventana_emergente.PictureBox6.Image = My.Resources.RepeatOne
            PictureBox10.Image = My.Resources.RepeatOne

        ElseIf ListRef = 4 Then
            Ventana_emergente.PictureBox6.Image = My.Resources.Ramdon
            PictureBox10.Image = My.Resources.Ramdon

        ElseIf ListRef > 4 Then
            ListRef = 0
            GoTo 1
        End If



        


    End Sub

   
    'Private Sub Timer3_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Dim ShowMe As String = IO.File.ReadAllText("N:\Nexus\AppWork\NxReproductor\Shown")


    '    If ShowMe = "False" Then
    '        Me.Hide()
    '        IO.File.WriteAllText("N:\Nexus\AppWork\NxReproductor\Shown", "")
    '    Else
    '        If ShowMe = "True" Then
    '            Me.Show()
    '            Me.BringToFront()
    '            IO.File.WriteAllText("N:\Nexus\AppWork\NxReproductor\Shown", "")
    '        End If
    '    End If

    'End Sub

    Dim tb As Boolean = False
    Dim tbk As Boolean = False

    Private Sub ToolBar_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolBar.Tick

        Distancia = Distancia + 5
        tbk = False
        If Distancia = 80 And tb = False Then

            ToolBar.Stop()
            tb = True
            arriba = False
        Else
            Panel5.Top = Panel5.Top + 5
            ' Panel4.Height = Panel4.Height + 5
        End If
    End Sub

    Dim ToolBarGoBackk As Integer
    Dim arriba As Boolean = False
   

    Dim Distancia As Integer = 80

    Private Sub ToolBarGoBack_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolBarGoBack.Tick

        Distancia = Distancia - 5

        If Distancia = 0 And tbk = False Then
            arriba = True
            tb = True
            tbk = True
            ToolBarGoBack.Stop()
            Timer.Start()
        Else
            Panel5.Top = Panel5.Top - 5
            'Panel4.Height = Panel4.Height - 5
        End If
    End Sub

    Private Sub Timer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer.Tick
        tb = False
        ToolBarGoBackk = ToolBarGoBackk + 1

        If ToolBarGoBackk = 5 And mHover = False Then
            ToolBar.Start()
            Timer.Stop()
        End If



    End Sub

    Dim mHover As Boolean = False

    Private Sub PictureBox9_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar1.MouseHover, PictureBox9.MouseHover, PictureBox8.MouseHover, PictureBox5.MouseHover, PictureBox4.MouseHover, PictureBox3.MouseHover, Panel5.MouseHover, Label2.MouseHover, Label1.MouseHover
        mHover = True
        If arriba = False Then
            ToolBarGoBack.Start()
            ToolBarGoBackk = 0
        Else
            ToolBarGoBackk = 0
        End If


    End Sub

    Private Sub Panel5_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Panel5.MouseEnter
        ToolBarGoBackk = 0
    End Sub

    Private Sub Form1_Deactivate(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Deactivate
        Panel1.BackColor = Color.DarkGray
        Panel2.BackColor = Color.DarkGray


    End Sub

    Private Sub Form1_Activated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        'Panel1.BackColor = SystmColor
        'Panel2.BackColor = SystmColor

       
    End Sub


    Private Sub PictureBox4_MouseMove_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox4.MouseMove
        PictureBox4.Image = My.Resources.NextO
    End Sub

    Private Sub PictureBox4_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox4.MouseLeave
        PictureBox4.Image = My.Resources.NextN
    End Sub

    Private Sub PictureBox3_MouseMove_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox3.MouseMove
        PictureBox3.Image = My.Resources.BackO
    End Sub

    Private Sub PictureBox3_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox3.MouseLeave
        PictureBox3.Image = My.Resources.BackN
    End Sub

    Private Sub PictureBox5_MouseMove_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox5.MouseMove
        If AxWindowsMediaPlayer1.playState = WMPLib.WMPPlayState.wmppsPlaying Then
            PictureBox5.Image = My.Resources.PausaO
        Else
            PictureBox5.Image = My.Resources.PlayO
        End If
    End Sub

    Private Sub PictureBox5_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox5.MouseLeave
        If AxWindowsMediaPlayer1.playState = WMPLib.WMPPlayState.wmppsPlaying Then
            PictureBox5.Image = My.Resources.PausaN
        Else
            PictureBox5.Image = My.Resources.PlayN
        End If
    End Sub

    Private Sub Panel5_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Panel5.MouseLeave
        mHover = False
        ToolBarGoBackk = 0
    End Sub
End Class
