Dim Con As New ADODB.Connection
Dim Rec As New ADODB.Recordset

Private Lista As ListView

Private Sub listagem()
    Con.Open "DSN=MSHOP30;Database=marcos;uid=sa;pwd=DBd4t43xp0rt@;"
        
    Rec.Open "Select * from veiculo ", Con, adOpenStatic, adLockReadOnly
    
    Do Until Rec.EOF
       Set lastItems = ListView1.ListItems.Add(, , Rec!id)
       lastItems.SubItems(1) = Rec!placa
       lastItems.SubItems(2) = Rec!modelo
       lastItems.SubItems(3) = Rec!marca
       lastItems.SubItems(4) = Rec!quantidade_lugares
       lastItems.SubItems(5) = Rec!descricao
       lastItems.SubItems(6) = Rec!url_imagem
       Rec.MoveNext
    Loop
    
    Rec.Close
    Con.Close
End Sub

Private Sub Recarregar_Click()
    ListView1.ListItems.Clear
    Call listagem
End Sub

Private Sub Voltar_Click()
    Unload Me
End Sub

Private Sub Form_Load()
    Call listagem
End Sub
