Public Class Ventana_emergente

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Try
            Label1.Text = Form1.AxWindowsMediaPlayer1.currentMedia.getItemInfo("Name")
            TrackBar1.Value = Form1.AxWindowsMediaPlayer1.settings.volume
        Catch ex As Exception

        End Try


        If Label1.Text.Length > 29 Then
            Label1.Left = 0

        Else
            Label1.Left = Panel2.Width / 2 - Label1.Width / 2

        End If


    End Sub

    Private Sub PictureBox7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox7.Click
        ListaDrep.Show()
    End Sub

    Public mute As Boolean = False

    Private Sub PictureBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox2.Click

        If mute = False Then
            Form1.AxWindowsMediaPlayer1.settings.mute = True
            mute = True
            PictureBox2.Image = My.Resources.NotSound
            Form1.PictureBox8.Image = My.Resources.NotSound
        Else

            Form1.AxWindowsMediaPlayer1.settings.mute = False
            mute = False
            PictureBox2.Image = My.Resources.Sound
            Form1.PictureBox8.Image = My.Resources.Sound
        End If

    End Sub

    Dim ref As Integer = 0

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick

        ref = ref + 1

        If ref = 5 Then
            TrackBar1.Visible = False
            PictureBox9.Visible = True
            Timer2.Stop()
        End If

    End Sub

    Private Sub TrackBar1_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar1.Scroll

        ref = 0
        Form1.AxWindowsMediaPlayer1.settings.volume = TrackBar1.Value
        Form1.TrackBar2.Value = Me.TrackBar1.Value

    End Sub

    Private Sub PictureBox9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox9.Click
        Timer2.Start()
        TrackBar1.Visible = True
        PictureBox9.Visible = False
    End Sub

    Private Sub Timer3_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer3.Tick

        Try
            TrackBar2.Maximum = Form1.AxWindowsMediaPlayer1.Ctlcontrols.currentItem.duration

            Label3.Text = Form1.AxWindowsMediaPlayer1.Ctlcontrols.currentPositionString
            Label2.Text = Form1.AxWindowsMediaPlayer1.Ctlcontrols.currentItem.durationString

            TrackBar2.Value = Form1.AxWindowsMediaPlayer1.Ctlcontrols.currentPosition
        Catch ex As Exception

        End Try


    End Sub

    Private Sub TrackBar2_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TrackBar2.MouseDown
        Timer3.Stop()
    End Sub

    Private Sub TrackBar2_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar2.Scroll
        Form1.AxWindowsMediaPlayer1.Ctlcontrols.currentPosition = TrackBar2.Value

    End Sub

    Private Sub TrackBar2_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TrackBar2.MouseUp
        Timer3.Start()
    End Sub

    Private Sub Ventana_emergente_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Top = My.Computer.Screen.WorkingArea.Height - (Me.Height + 13)
        Me.Left = My.Computer.Screen.WorkingArea.Width - (Me.Width + 10)
    End Sub

    
    Private Sub PictureBox5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox5.Click
        If Form1.AxWindowsMediaPlayer1.playState = WMPLib.WMPPlayState.wmppsPlaying Then

            Form1.AxWindowsMediaPlayer1.Ctlcontrols.pause()
        Else
            Form1.AxWindowsMediaPlayer1.Ctlcontrols.play()
        End If
    End Sub

    Private Sub PictureBox3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox3.Click

        If Form1.ListRef = 1 Then

            Try
                If ListaDrep.ListView1.Items(Reprnum).Index = ListaDrep.ListView1.Items.Count - 1 Then

                    '______________________________________________________________________________________________________
                    Form1.AxWindowsMediaPlayer1.URL = Nothing

                Else
                    Reprnum = Reprnum - 1

                    Try
                        ListaDrep.ListView1.Items(Reprnum).Selected = True
                        Form1.AxWindowsMediaPlayer1.URL = ListaDrep.ListView1.Items(Reprnum).ToolTipText


                    Catch ex As Exception

                    End Try


                End If



            Catch ex As Exception

            End Try

            '_________________________________________________________________________________________________________________


        ElseIf Form1.ListRef = 2 Then


            If ListaDrep.ListView1.Items(Reprnum).Index = 0 Then

                Try

                    ListaDrep.ListView1.Items(ListaDrep.ListView1.Items.Count - 1).Selected = True
                    Form1.AxWindowsMediaPlayer1.URL = ListaDrep.ListView1.Items(ListaDrep.ListView1.Items.Count - 1).ToolTipText

                Catch ex As Exception

                End Try

            Else

                Reprnum = Reprnum - 1
                '______________________________________________________________________________________________________

                Try
                    ListaDrep.ListView1.Items(Reprnum).Selected = True
                    Form1.AxWindowsMediaPlayer1.URL = ListaDrep.ListView1.Items(Reprnum).ToolTipText


                Catch ex As Exception

                End Try
            End If


        ElseIf Form1.ListRef = 3 Then

            ListaDrep.ListView1.Items(ListaDrep.ListView1.Items(Reprnum).Index).Selected = True
            Form1.AxWindowsMediaPlayer1.URL = ListaDrep.ListView1.Items(Reprnum).ToolTipText

        ElseIf Form1.ListRef = 4 Then


            Try
                Dim a = Form1.GetNumR()
                Reprnum = a

                ListaDrep.ListView1.Items(Reprnum).Selected = True
                Form1.AxWindowsMediaPlayer1.URL = ListaDrep.ListView1.Items(Reprnum).ToolTipText

            Catch ex As Exception

            End Try
        End If

        Form1.AxWindowsMediaPlayer1.Ctlcontrols.play()

        Form1.Mm = False

    End Sub

    Private Sub PictureBox4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox4.Click

        If Form1.ListRef = 1 Then

            Try
                If ListaDrep.ListView1.Items(Reprnum).Index = ListaDrep.ListView1.Items.Count - 1 Then

                    '______________________________________________________________________________________________________
                    Form1.AxWindowsMediaPlayer1.URL = Nothing

                Else
                    Reprnum = Reprnum + 1

                    Try
                        ListaDrep.ListView1.Items(Reprnum).Selected = True
                        Form1.AxWindowsMediaPlayer1.URL = ListaDrep.ListView1.Items(Reprnum).ToolTipText



                    Catch ex As Exception

                    End Try


                End If



            Catch ex As Exception

            End Try

            '_________________________________________________________________________________________________________________


        ElseIf Form1.ListRef = 2 Then


            If ListaDrep.ListView1.Items(Reprnum).Index = ListaDrep.ListView1.Items.Count - 1 Then

                Try

                    ListaDrep.ListView1.Items(0).Selected = True
                    Form1.AxWindowsMediaPlayer1.URL = ListaDrep.ListView1.Items(0).ToolTipText

                Catch ex As Exception

                End Try

            Else

                Reprnum = Reprnum + 1
                '______________________________________________________________________________________________________

                Try
                    ListaDrep.ListView1.Items(Reprnum).Selected = True
                    Form1.AxWindowsMediaPlayer1.URL = ListaDrep.ListView1.Items(Reprnum).ToolTipText


                Catch ex As Exception

                End Try
            End If


        ElseIf Form1.ListRef = 3 Then

            ListaDrep.ListView1.Items(ListaDrep.ListView1.Items(Reprnum).Index).Selected = True
            Form1.AxWindowsMediaPlayer1.URL = ListaDrep.ListView1.Items(Reprnum).ToolTipText

        ElseIf Form1.ListRef = 4 Then


            Try
                Dim a = Form1.GetNumR()
                Reprnum = a

                ListaDrep.ListView1.Items(Reprnum).Selected = True
                Form1.AxWindowsMediaPlayer1.URL = ListaDrep.ListView1.Items(Reprnum).ToolTipText

            Catch ex As Exception

            End Try
        End If

        Form1.AxWindowsMediaPlayer1.Ctlcontrols.play()

        Form1.Mm = False


    End Sub

   

    Private Sub PictureBox6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox6.Click

1:

        Form1.ListRef = Form1.ListRef + 1

        If Form1.ListRef = 1 Then

            Me.PictureBox6.Image = My.Resources.ListOne
            Form1.PictureBox10.Image = My.Resources.ListOne

        ElseIf Form1.ListRef = 2 Then

            Me.PictureBox6.Image = My.Resources.RepeatAll
            Form1.PictureBox10.Image = My.Resources.RepeatAll

        ElseIf Form1.ListRef = 3 Then

            Me.PictureBox6.Image = My.Resources.RepeatOne
            Form1.PictureBox10.Image = My.Resources.RepeatOne

        ElseIf Form1.ListRef = 4 Then
            Me.PictureBox6.Image = My.Resources.Ramdon
            Form1.PictureBox10.Image = My.Resources.Ramdon

        ElseIf Form1.ListRef > 4 Then
            Form1.ListRef = 0
            GoTo 1
        End If

    End Sub

  
    Private Sub NotifyIcon1_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon1.MouseClick
        Me.Hide()
    End Sub

    Private Sub NotifyIcon1_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        Me.Show()
    End Sub

    Private Sub Label1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.TextChanged
        If DirectCast(sender, Label).Text.Length > 35 Then
            DirectCast(sender, Label).Text = DirectCast(sender, Label).Text.Substring(0, 35) & "..."
        End If
    End Sub

    Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem1.Click
        Form1.Show()
    End Sub

    Private Sub NextToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NextToolStripMenuItem.Click

        If Form1.ListRef = 1 Then

            Try
                If ListaDrep.ListView1.Items(Reprnum).Index = ListaDrep.ListView1.Items.Count - 1 Then

                    '______________________________________________________________________________________________________
                    Form1.AxWindowsMediaPlayer1.URL = Nothing

                Else
                    Reprnum = Reprnum + 1

                    Try
                        ListaDrep.ListView1.Items(Reprnum).Selected = True
                        Form1.AxWindowsMediaPlayer1.URL = ListaDrep.ListView1.Items(Reprnum).ToolTipText



                    Catch ex As Exception

                    End Try


                End If



            Catch ex As Exception

            End Try

            '_________________________________________________________________________________________________________________


        ElseIf Form1.ListRef = 2 Then


            If ListaDrep.ListView1.Items(Reprnum).Index = ListaDrep.ListView1.Items.Count - 1 Then

                Try

                    ListaDrep.ListView1.Items(0).Selected = True
                    Form1.AxWindowsMediaPlayer1.URL = ListaDrep.ListView1.Items(0).ToolTipText

                Catch ex As Exception

                End Try

            Else

                Reprnum = Reprnum + 1
                '______________________________________________________________________________________________________

                Try
                    ListaDrep.ListView1.Items(Reprnum).Selected = True
                    Form1.AxWindowsMediaPlayer1.URL = ListaDrep.ListView1.Items(Reprnum).ToolTipText


                Catch ex As Exception

                End Try
            End If


        ElseIf Form1.ListRef = 3 Then

            ListaDrep.ListView1.Items(ListaDrep.ListView1.Items(Reprnum).Index).Selected = True
            Form1.AxWindowsMediaPlayer1.URL = ListaDrep.ListView1.Items(Reprnum).ToolTipText

        ElseIf Form1.ListRef = 4 Then


            Try
                Dim a = Form1.GetNumR()
                Reprnum = a

                ListaDrep.ListView1.Items(Reprnum).Selected = True
                Form1.AxWindowsMediaPlayer1.URL = ListaDrep.ListView1.Items(Reprnum).ToolTipText

            Catch ex As Exception

            End Try
        End If

        Form1.AxWindowsMediaPlayer1.Ctlcontrols.play()

        Form1.Mm = False
    End Sub

    Private Sub PreviousToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PreviousToolStripMenuItem.Click

        If Form1.ListRef = 1 Then

            Try
                If ListaDrep.ListView1.Items(Reprnum).Index = ListaDrep.ListView1.Items.Count - 1 Then

                    '______________________________________________________________________________________________________
                    Form1.AxWindowsMediaPlayer1.URL = Nothing

                Else
                    Reprnum = Reprnum - 1

                    Try
                        ListaDrep.ListView1.Items(Reprnum).Selected = True
                        Form1.AxWindowsMediaPlayer1.URL = ListaDrep.ListView1.Items(Reprnum).ToolTipText


                    Catch ex As Exception

                    End Try


                End If



            Catch ex As Exception

            End Try

            '_________________________________________________________________________________________________________________


        ElseIf Form1.ListRef = 2 Then


            If ListaDrep.ListView1.Items(Reprnum).Index = 0 Then

                Try

                    ListaDrep.ListView1.Items(ListaDrep.ListView1.Items.Count - 1).Selected = True
                    Form1.AxWindowsMediaPlayer1.URL = ListaDrep.ListView1.Items(ListaDrep.ListView1.Items.Count - 1).ToolTipText

                Catch ex As Exception

                End Try

            Else

                Reprnum = Reprnum - 1
                '______________________________________________________________________________________________________

                Try
                    ListaDrep.ListView1.Items(Reprnum).Selected = True
                    Form1.AxWindowsMediaPlayer1.URL = ListaDrep.ListView1.Items(Reprnum).ToolTipText


                Catch ex As Exception

                End Try
            End If


        ElseIf Form1.ListRef = 3 Then

            ListaDrep.ListView1.Items(ListaDrep.ListView1.Items(Reprnum).Index).Selected = True
            Form1.AxWindowsMediaPlayer1.URL = ListaDrep.ListView1.Items(Reprnum).ToolTipText

        ElseIf Form1.ListRef = 4 Then


            Try
                Dim a = Form1.GetNumR()
                Reprnum = a

                ListaDrep.ListView1.Items(Reprnum).Selected = True
                Form1.AxWindowsMediaPlayer1.URL = ListaDrep.ListView1.Items(Reprnum).ToolTipText

            Catch ex As Exception

            End Try
        End If

        Form1.AxWindowsMediaPlayer1.Ctlcontrols.play()

        Form1.Mm = False
    End Sub
End Class