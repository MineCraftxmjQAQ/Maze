Public Class Form1
    Private ReadOnly ScreenX As Integer = SystemInformation.PrimaryMonitorSize.Width '获取屏幕横向分辨率
    Public Raw() As Integer = New Integer(400) {}
    Public Sign() As Integer = New Integer(400) {}
    Private Start As Integer
    Private ScMode As Boolean
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim i As Integer
        For i = 0 To 400
            Raw(i) = 0
            Sign(i) = 0
        Next i
        TextBox2.Text = "迷宫生成起点为:"
        Call Sc()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim Direct() As Integer = New Integer(3) {} '标记方向是否为可打开
        Dim i As Integer
        Dim row As Integer, col As Integer
        Dim Dir_fin As Integer, NowBlock As Integer
        Dim St As Integer, NowSt As Integer
        Button1.Enabled = False
        Button2.Enabled = False
        ComboBox1.Enabled = False
        ListBox2.Items.Clear()
        ListBox3.Items.Clear()
        If ComboBox1.SelectedIndex = 0 Then
            ScMode = True
            Me.Left = ScreenX - Me.Width
        ElseIf ComboBox1.SelectedIndex = 1 Then
            ScMode = False
            Me.Left = (ScreenX - Me.Width) \ 2
        Else
            MsgBox("未选择显示模式,请选择后再点击", vbExclamation, "错误")
            Button1.Enabled = True
            ComboBox1.Enabled = True
            Exit Sub
        End If
        Randomize()
        For i = 0 To 400
            Raw(i) = 0
            Sign(i) = 0
        Next i
        For i = 0 To 3
            Direct(i) = 0
        Next i
        Call Sc()
        NowBlock = Int(Rnd() * 400) + 1 '生成范围1-400
        row = (NowBlock - 1) \ 20 + 1 '行号
        col = (NowBlock - 1) Mod 20 + 1 '列号
        St = 1
        Start = NowBlock
        TextBox2.Text = "迷宫生成起点为:第" + CStr(row) + "行,第" + CStr(col) + "列"
        Raw(NowBlock) = 1
        Sign(NowBlock) = St
        Call Sc()
        If ScMode = True Then MsgBox("当前更新的迷宫块序号:" + CStr(St) + vbCrLf + "当前更新的迷宫块坐标:第" + CStr(row) + "行,第" + CStr(col) + "列", , "过程显示")
        If row > 1 Then '向上
            If Raw(NowBlock - 20) = 0 Then
                Direct(0) = 1
            Else
                Direct(0) = 0
            End If
        Else
            Direct(0) = 0
        End If
        If col < 20 Then '向右
            If Raw(NowBlock + 1) = 0 Then
                Direct(1) = 1
            Else
                Direct(1) = 0
            End If
        Else
            Direct(1) = 0
        End If
        If row < 20 Then '向下
            If Raw(NowBlock + 20) = 0 Then
                Direct(2) = 1
            Else
                Direct(2) = 0
            End If
        Else
            Direct(2) = 0
        End If
        If col > 1 Then '向左
            If Raw(NowBlock - 1) = 0 Then
                Direct(3) = 1
            Else
                Direct(3) = 0
            End If
        Else
            Direct(3) = 0
        End If
        Dir_fin = DirSelect(Direct(0), Direct(1), Direct(2), Direct(3)) '随机选择当前块可允许的开出方向
        If Dir_fin = 0 Then '向上
            NowBlock -= 20
        ElseIf Dir_fin = 1 Then '向右
            NowBlock += 1
        ElseIf Dir_fin = 2 Then '向下
            NowBlock += 20
        ElseIf Dir_fin = 3 Then '向左
            NowBlock -= 1
        End If
        Raw(NowBlock) = 1
        St += 1
        NowSt = St
        Sign(NowBlock) = St
        Call Sc()
        If ScMode = True Then MsgBox("当前更新的迷宫块序号:" + CStr(St) + vbCrLf + "当前更新的迷宫块坐标:第" + CStr(row) + "行,第" + CStr(col) + "列", , "过程显示")
        While NowSt > 1
            row = (NowBlock - 1) \ 20 + 1 '行号
            col = (NowBlock - 1) Mod 20 + 1 '列号
            If row > 1 Then '向上
                If Raw(NowBlock - 20) = 0 Then
                    Direct(0) = 1
                Else
                    Direct(0) = 0
                End If
            Else
                Direct(0) = 0
            End If
            If col < 20 Then '向右
                If Raw(NowBlock + 1) = 0 Then
                    Direct(1) = 1
                Else
                    Direct(1) = 0
                End If
            Else
                Direct(1) = 0
            End If
            If row < 20 Then '向下
                If Raw(NowBlock + 20) = 0 Then
                    Direct(2) = 1
                Else
                    Direct(2) = 0
                End If
            Else
                Direct(2) = 0
            End If
            If col > 1 Then '向左
                If Raw(NowBlock - 1) = 0 Then
                    Direct(3) = 1
                Else
                    Direct(3) = 0
                End If
            Else
                Direct(3) = 0
            End If
            Dir_fin = DirSelect(Direct(0), Direct(1), Direct(2), Direct(3)) '随机选择当前块可允许的开出方向
            While Dir_fin <> -1
                If NextBlock(NowBlock, Dir_fin) = True Then
                    If Dir_fin = 0 Then '向上
                        NowBlock -= 20
                    ElseIf Dir_fin = 1 Then '向右
                        NowBlock += 1
                    ElseIf Dir_fin = 2 Then '向下
                        NowBlock += 20
                    ElseIf Dir_fin = 3 Then '向左
                        NowBlock -= 1
                    End If
                    Raw(NowBlock) = 1
                    St += 1
                    NowSt = St
                    Sign(NowBlock) = St
                    Call Sc()
                    If ScMode = True Then MsgBox("当前更新的迷宫块序号:" + CStr(St) + vbCrLf + "当前更新的迷宫块坐标:第" + CStr(row) + "行,第" + CStr(col) + "列", , "过程显示")
                    Exit While
                Else
                    Direct(Dir_fin) = 0
                    Dir_fin = DirSelect(Direct(0), Direct(1), Direct(2), Direct(3))
                End If
            End While
            While Dir_fin = -1
                For i = 1 To 400
                    If Sign(i) = NowSt - 1 And NowSt > 0 Then
                        NowBlock = i
                        NowSt -= 1
                        Exit For
                    End If
                Next i
                If NextBlock(NowBlock, 0) = True Then Exit While
                If NextBlock(NowBlock, 1) = True Then Exit While
                If NextBlock(NowBlock, 2) = True Then Exit While
                If NextBlock(NowBlock, 3) = True Then Exit While
            End While
        End While
        MsgBox("迷宫生成成功", vbInformation, "成功")
        For i = 1 To 400
            If Sign(i) > 0 And i <> Start Then ListBox2.Items.Add(CStr(i))
        Next
        Button1.Enabled = True
        Button2.Enabled = True
        ComboBox1.Enabled = True
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim NowBlock As Integer, EndBlcok As Integer
        Dim Direct As Integer
        Dim DirectCount As Integer, AllCount As Integer
        Dim temp As Integer
        Dim row As Integer, col As Integer, i As Integer
        Dim DirectTemp() As Integer = New Integer(3) {}
        Dim RawT() As Integer = New Integer(400) {}
        Dim RawTemp() As Integer = New Integer(400) {}
        Dim Route(,) As Integer = New Integer(400, 4) {}
        If ComboBox1.SelectedIndex = 0 Then
            ScMode = True
            Me.Left = ScreenX - Me.Width
        ElseIf ComboBox1.SelectedIndex = 1 Then
            ScMode = False
            Me.Left = (ScreenX - Me.Width) \ 2
        End If
        If ListBox2.SelectedIndex = -1 Then MsgBox("未选择终点序号,请选择后再点击", vbExclamation, "错误") : Exit Sub
        ListBox2.Enabled = False
        ListBox3.Items.Clear()
        EndBlcok = Val(ListBox2.SelectedItem)
        NowBlock = Start
        AllCount = 1
        For i = 1 To 400
            RawT(i) = Raw(i)
            RawTemp(i) = Raw(i)
        Next i
        row = (NowBlock - 1) \ 20 + 1 '行号
        col = (NowBlock - 1) Mod 20 + 1 '列号
        If row > 1 Then '向上
            If RawT(NowBlock - 20) = 1 Then
                DirectTemp(0) = 1
            Else
                DirectTemp(0) = 0
            End If
        Else
            DirectTemp(0) = 0
        End If
        If col < 20 Then '向右
            If RawT(NowBlock + 1) = 1 Then
                DirectTemp(1) = 1
            Else
                DirectTemp(1) = 0
            End If
        Else
            DirectTemp(1) = 0
        End If
        If row < 20 Then '向下
            If RawT(NowBlock + 20) = 1 Then
                DirectTemp(2) = 1
            Else
                DirectTemp(2) = 0
            End If
        Else
            DirectTemp(2) = 0
        End If
        If col > 1 Then '向左
            If RawT(NowBlock - 1) = 1 Then
                DirectTemp(3) = 1
            Else
                DirectTemp(3) = 0
            End If
        Else
            DirectTemp(3) = 0
        End If
        Direct = DirSelect(DirectTemp(0), DirectTemp(1), DirectTemp(2), DirectTemp(3))
        Route(AllCount, 0) = NowBlock
        Route(AllCount, 1) = DirectTemp(0)
        Route(AllCount, 2) = DirectTemp(1)
        Route(AllCount, 3) = DirectTemp(2)
        Route(AllCount, 4) = DirectTemp(3)
        If Direct = 0 Then
            NowBlock -= 20
        ElseIf Direct = 1 Then
            NowBlock += 1
        ElseIf Direct = 2 Then
            NowBlock += 20
        ElseIf Direct = 3 Then
            NowBlock -= 1
        End If
        AllCount += 1
        While NowBlock <> EndBlcok
            row = (NowBlock - 1) \ 20 + 1 '行号
            col = (NowBlock - 1) Mod 20 + 1 '列号
            DirectCount = 0
            If row > 1 Then '向上
                If RawT(NowBlock - 20) = 1 And Route(AllCount - 1, 0) <> NowBlock - 20 Then
                    DirectCount += 1
                    DirectTemp(0) = 1
                Else
                    DirectTemp(0) = 0
                End If
            Else
                DirectTemp(0) = 0
            End If
            If col < 20 Then '向右
                If RawT(NowBlock + 1) = 1 And Route(AllCount - 1, 0) <> NowBlock + 1 Then
                    DirectCount += 1
                    DirectTemp(1) = 1
                Else
                    DirectTemp(1) = 0
                End If
            Else
                DirectTemp(1) = 0
            End If
            If row < 20 Then '向下
                If RawT(NowBlock + 20) = 1 And Route(AllCount - 1, 0) <> NowBlock + 20 Then
                    DirectCount += 1
                    DirectTemp(2) = 1
                Else
                    DirectTemp(2) = 0
                End If
            Else
                DirectTemp(2) = 0
            End If
            If col > 1 Then '向左
                If RawT(NowBlock - 1) = 1 And Route(AllCount - 1, 0) <> NowBlock - 1 Then
                    DirectCount += 1
                    DirectTemp(3) = 1
                Else
                    DirectTemp(3) = 0
                End If
            Else
                DirectTemp(3) = 0
            End If
            If DirectCount = 0 Then '死路格
                Route(AllCount, 0) = NowBlock
                Route(AllCount, 1) = DirectTemp(0)
                Route(AllCount, 2) = DirectTemp(1)
                Route(AllCount, 3) = DirectTemp(2)
                Route(AllCount, 4) = DirectTemp(3)
                For i = 400 To 1 Step -1
                    If Route(i, 1) + Route(i, 2) + Route(i, 3) + Route(i, 4) > 1 Then
                        temp = i
                        Exit For
                    End If
                Next i
                AllCount = temp
                RawT(Route(temp + 1, 0)) = 0
                NowBlock = Route(temp, 0)
                temp += 1
                While temp <= 400 And Route(temp, 0) <> 0
                    Route(temp, 0) = 0
                    temp += 1
                End While
            ElseIf DirectCount = 1 Then '一般路径
                Direct = DirSelect(DirectTemp(0), DirectTemp(1), DirectTemp(2), DirectTemp(3))
                Route(AllCount, 0) = NowBlock
                Route(AllCount, 1) = DirectTemp(0)
                Route(AllCount, 2) = DirectTemp(1)
                Route(AllCount, 3) = DirectTemp(2)
                Route(AllCount, 4) = DirectTemp(3)
                If Direct = 0 Then
                    NowBlock -= 20
                ElseIf Direct = 1 Then
                    NowBlock += 1
                ElseIf Direct = 2 Then
                    NowBlock += 20
                ElseIf Direct = 3 Then
                    NowBlock -= 1
                End If
                AllCount += 1
            ElseIf DirectCount > 1 Then '分支点
                Direct = DirSelect(DirectTemp(0), DirectTemp(1), DirectTemp(2), DirectTemp(3))
                Route(AllCount, 0) = NowBlock
                Route(AllCount, 1) = DirectTemp(0)
                Route(AllCount, 2) = DirectTemp(1)
                Route(AllCount, 3) = DirectTemp(2)
                Route(AllCount, 4) = DirectTemp(3)
                If Direct = 0 Then
                    NowBlock -= 20
                ElseIf Direct = 1 Then
                    NowBlock += 1
                ElseIf Direct = 2 Then
                    NowBlock += 20
                ElseIf Direct = 3 Then
                    NowBlock -= 1
                End If
                AllCount += 1
            End If
        End While
        Raw(Start) = 2
        Raw(EndBlcok) = 2
        Call Sc()
        Route(AllCount, 0) = EndBlcok
        If ScMode = True Then
            For i = 1 To AllCount
                Raw(Route(i, 0)) = 2
                row = (Route(i, 0) - 1) \ 20 + 1 '行号
                col = (Route(i, 0) - 1) Mod 20 + 1 '列号
                MsgBox("当前计数:" + CStr(i) + "当前坐标:第" + CStr(row) + "行,第" + CStr(col) + "列", , "过程显示")
                Call Sc()
            Next i
        Else
            For i = 1 To AllCount
                Raw(Route(i, 0)) = 2
            Next i
            Call Sc()
        End If
        For i = 1 To 400
            Raw(i) = RawTemp(i)
        Next i
        i = 1
        While i <= 400 And Route(i, 0) <> 0
            row = (Route(i, 0) - 1) \ 20 + 1 '行号
            col = (Route(i, 0) - 1) Mod 20 + 1 '列号
            ListBox3.Items.Add("第" + NumLength(CStr(i)) + "个迷宫格,序号:" + NumLength(CStr(Route(i, 0))))
            ListBox3.Items.Add("位于第" + NumLength(CStr(row)) + "行,第" + NumLength(CStr(col)) + "列")
            i += 1
        End While
        MsgBox("路径搜索成功", vbInformation, "成功")
        ListBox3.Select()
        ListBox2.Enabled = True
    End Sub
    Private Function NextBlock(nowblock As Integer, direct As Integer) As Boolean
        Dim row As Integer, col As Integer, temp As Integer
        NextBlock = True
        If direct = 0 Then '向上
            temp = nowblock - 20
            row = (temp - 1) \ 20 + 1
            col = (temp - 1) Mod 20 + 1
            If row - 1 >= 1 Then
                If Raw(temp - 20) = 1 Then NextBlock = False
            End If
            If col + 1 <= 20 And temp >= 1 Then
                If Raw(temp + 1) = 1 Then NextBlock = False
            End If
            If col - 1 >= 1 And temp >= 1 Then
                If Raw(temp - 1) = 1 Then NextBlock = False
            End If
        ElseIf direct = 1 Then '向右
            temp = nowblock
            row = (temp - 1) \ 20 + 1
            col = (temp - 1) Mod 20 + 1
            If col + 2 <= 20 Then
                If Raw(temp + 2) = 1 Then NextBlock = False
            End If
            If row - 1 >= 1 And col < 20 Then
                If Raw(temp - 19) = 1 Then NextBlock = False
            End If
            If row + 1 <= 20 And col < 20 Then
                If Raw(temp + 21) = 1 Then NextBlock = False
            End If
        ElseIf direct = 2 Then '向下
            temp = nowblock + 20
            row = (temp - 1) \ 20 + 1
            col = (temp - 1) Mod 20 + 1
            If row + 1 <= 20 Then
                If Raw(temp + 20) = 1 Then NextBlock = False
            End If
            If col + 1 <= 20 And temp <= 400 Then
                If Raw(temp + 1) = 1 Then NextBlock = False
            End If
            If col - 1 >= 1 And temp <= 400 Then
                If Raw(temp - 1) = 1 Then NextBlock = False
            End If
        ElseIf direct = 3 Then '向左
            temp = nowblock
            row = (temp - 1) \ 20 + 1
            col = (temp - 1) Mod 20 + 1
            If col - 2 >= 1 Then
                If Raw(temp - 2) = 1 Then NextBlock = False
            End If
            If row - 1 >= 1 And col > 1 Then
                If Raw(temp - 21) = 1 Then NextBlock = False
            End If
            If row + 1 <= 20 And col > 1 Then
                If Raw(temp + 19) = 1 Then NextBlock = False
            End If
        End If
    End Function
    Private Shared Function DirSelect(Dir_0 As Integer, Dir_1 As Integer, Dir_2 As Integer, Dir_3 As Integer) As Integer
        Dim sum As Integer, i As Integer, t As Integer
        Dim Sel() As Integer = New Integer(3) {}
        Dim Dir_Sel() As Integer = New Integer(3) {}
        Dir_Sel(0) = Dir_0
        Dir_Sel(1) = Dir_1
        Dir_Sel(2) = Dir_2
        Dir_Sel(3) = Dir_3
        t = 0
        sum = Dir_0 + Dir_1 + Dir_2 + Dir_3
        If sum > 0 Then
            For i = 0 To 3
                If Dir_Sel(i) = 1 Then
                    Sel(t) = i
                    t += 1
                End If
            Next i
            Randomize()
            DirSelect = Sel(Int(Rnd() * sum))
        Else
            DirSelect = -1
        End If
    End Function
    Private Sub Sc()
        Dim i As Integer, j As Integer
        Dim Draw() As String = New String(2) {}
        Dim row As String
        Draw(0) = "■"
        Draw(1) = "□"
        Draw(2) = "☑"
        ListBox1.Items.Clear()
        ListBox1.Items.Add(" ---------------------------------------")
        For i = 1 To 20
            row = ""
            For j = 1 To 20
                row += Draw(Raw((i - 1) * 20 + j))
            Next j
            ListBox1.Items.Add("|" + row + "|")
        Next i
        ListBox1.Items.Add(" ---------------------------------------")
    End Sub
    Private Shared Function NumLength(Num As String) As String
        If Len(Num) = 1 Then
            NumLength = "  " + Num
        ElseIf Len(Num) = 2 Then
            NumLength = " " + Num
        Else
            NumLength = Num
        End If
    End Function
    Private Sub ListBox1_DrawItem(sender As Object, e As DrawItemEventArgs) Handles ListBox1.DrawItem
        e.Graphics.FillRectangle(New SolidBrush(e.BackColor), e.Bounds)
        If e.Index >= 0 Then
            Dim sStringFormat As New StringFormat With {
                .LineAlignment = StringAlignment.Center
            }
            e.Graphics.DrawString((CType(sender, ListBox)).Items(e.Index).ToString(), e.Font, New SolidBrush(e.ForeColor), e.Bounds, sStringFormat)
        End If
        e.DrawFocusRectangle()
    End Sub
End Class