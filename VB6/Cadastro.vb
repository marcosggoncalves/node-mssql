Dim Con As New ADODB.Connection
Dim Rec As New ADODB.Recordset

Private Sub Clear()
    id.Text = ""
    placa.Text = ""
    modelo.Text = ""
    marca.Text = ""
    quantidade_lugares.Text = ""
    descricao.Text = ""
    url.Text = ""
    
    WebBrowser1.Navigate "https://e7.pngegg.com/pngimages/829/733/png-clipart-logo-brand-product-trademark-font-not-found-logo-brand.png"
    
    DuplicarCadastro.Visible = False
    Excluir.Visible = False
    PublicaSite.Visible = False
    RemoverSite.Visible = False
    LabelSiteAcoes.Visible = False
    StatusSite.Visible = False
End Sub

Private Sub Form_Load()

End Sub

Private Sub listagem_Click()
    MeusVeiculos.Show
End Sub

Private Sub Pesquisar_Click()
    If placa.Text = "" Then
         MsgBox "Digite a Placa do Veiculo para realizar pesquisar!"
    Else
        Con.Open "DSN=MSHOP30;Database=marcos;uid=sa;pwd=DBd4t43xp0rt@;"
        
        Rec.Open "Select * from veiculo where placa='" & placa.Text & "'", Con, adOpenStatic, adLockReadOnly
        
        Call Clear
        
        If Rec.RecordCount > 0 Then
            id.Text = Rec.Fields!id
            placa.Text = Rec.Fields!placa
            modelo.Text = Rec.Fields!modelo
            marca.Text = Rec.Fields!marca
            quantidade_lugares.Text = Rec.Fields!quantidade_lugares
            descricao.Text = Rec.Fields!descricao
            url.Text = Rec.Fields!url_imagem
            
            WebBrowser1.Navigate Rec.Fields!url_imagem
            Excluir.Visible = True
            DuplicarCadastro.Visible = True
            LabelSiteAcoes.Visible = True
            StatusSite.Visible = True
            
            If Rec.Fields!is_site = 0 Or Rec.Fields!is_site = Null Then
                PublicaSite.Visible = True
                StatusSite.Caption = "NÃO FOI PUBLICADO NO SITE"
            Else
                RemoverSite.Visible = True
                StatusSite.Caption = "PUBLICADO NO SITE"
            End If
            
        Else
            MsgBox "Cadastro não encontrado!"
            
            If id.Text <> "" Then
                Call Clear
            End If
        End If
            
        Rec.Close
        Con.Close
    End If
End Sub

Private Sub Excluir_Click()
    If id.Text = "" Then
         MsgBox "Selecione um veiculo para realizar a exclusão!"
    Else
        Con.Open "DSN=MSHOP30;Database=marcos;uid=sa;pwd=DBd4t43xp0rt@;"
     
        Rec.Open "SELECT * FROM veiculo WHERE id = " & id.Text & "", Con, adOpenKeyset, adLockOptimistic
        
        If Not Rec.EOF Then
            MsgBox "Cadastro excluido com  sucesso!"
            
            Rec.Delete
            Rec.Close
            Con.Close
            
            Call Clear
        End If
    End If
End Sub

Private Sub Gravar_Click()
    Dim strsql As String

    If placa.Text = "" Then
        MsgBox "Digite a placa do Veiculo!"
    ElseIf modelo.Text = "" Then
        MsgBox "Digite o modelo do veiculo!"
    ElseIf marca.Text = "" Then
        MsgBox "Digite a marca do veiculo!"
    ElseIf quantidade_lugares.Text = "" Then
        MsgBox "Digite a quantidade de lugares do veiculo!"
    ElseIf descricao.Text = "" Then
        MsgBox "Digite uma descrição do veiculo!"
    ElseIf url.Text = "" Then
        MsgBox "Informe uma url de imagem do veiculo!"
    Else
        Con.Open "DSN=MSHOP30;Database=marcos;uid=sa;pwd=DBd4t43xp0rt@;"
        
        If (id.Text <> "" And DuplicarCadastro.Value = False) Then
            strsql = "Update dbo.veiculo set url_imagem = '" & url.Text & "', placa = '" & placa.Text & "', modelo = '" & modelo.Text & "', marca = '" & marca.Text & "', descricao = '" & descricao.Text & "', quantidade_lugares = '" & quantidade_lugares.Text & "' where id = '" & id.Text & "'"
        
            MsgBox "Cadastro alterado com  sucesso!", vbOKCancel, "Realizado!"
        Else
            strsql = "INSERT INTO dbo.veiculo(url_imagem, placa,modelo,marca,quantidade_lugares, descricao)VALUES('" & url.Text & "', '" & placa.Text & "', '" & modelo.Text & "','" & marca.Text & "', '" & quantidade_lugares.Text & "', '" & descricao.Text & "')"
            
            If DuplicarCadastro.Value = True Then
                MsgBox "Cadastro duplicado com sucesso!"
            Else
                MsgBox "Cadastro registrado com  sucesso!"
            End If
            
        End If
        
        Con.BeginTrans
        Con.Execute strsql
        Con.CommitTrans
        Con.Close
        
        Call Clear
    End If
End Sub


Function StatusVeiculoSite(is_site As Integer, id As Integer) As String
    Con.Open "DSN=MSHOP30;Database=marcos;uid=sa;pwd=DBd4t43xp0rt@"
    
    Con.BeginTrans
    Con.Execute "Update dbo.veiculo set is_site = " & is_site & "  where id = " & id & ""
    Con.CommitTrans
    Con.Close
    
    StatusVeiculoSite = "Status de cadastro foi alterado com sucesso!"
End Function

Private Sub PublicaSite_Click()
  Dim result As String
  result = APISITE.RequestSite(id.Text, placa.Text, descricao.Text, url.Text, marca.Text, modelo.Text, quantidade_lugares.Text, 1)
  MsgBox result
  result = StatusVeiculoSite(1, id.Text)
  MsgBox result
  Call Clear
End Sub

Private Sub RemoverSite_Click()
    Dim result As String
    result = APISITE.RequestSite(id.Text, "", "", "", "", "", "", 0)
    MsgBox result
    result = StatusVeiculoSite(0, id.Text)
    MsgBox result
    Call Clear
End Sub
