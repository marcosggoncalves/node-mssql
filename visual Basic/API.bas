Function JSON(id As Integer, placa As String, descricao As String, url_imagem As String, marca As String, modelo As String, quantidade_lugares As String, is_site As Integer) As String
    Dim jsonString As String
    jsonString = "{"
    jsonString = jsonString & """id_rp"": " & id & ","
    jsonString = jsonString & """placa"": """ & placa & ""","
    jsonString = jsonString & """descricao"": """ & descricao & ""","
    jsonString = jsonString & """url_imagem"": """ & url_imagem & ""","
    jsonString = jsonString & """marca"": """ & marca & ""","
    jsonString = jsonString & """marca"": """ & modelo & ""","
    jsonString = jsonString & """quantidade_lugares"": """ & quantidade_lugares & ""","
    jsonString = jsonString & """is_site"": " & is_site & ""
    jsonString = jsonString & "}"
    
    JSON = jsonString
End Function

Function enviaCadastro(id As Integer, placa As String, descricao As String, url_imagem As String, marca As String, modelo As String, quantidade_lugares As String, is_site As Integer) As String
    Dim xhr As Object

    Set xhr = CreateObject("MSXML2.ServerXMLHTTP")

    Dim mensagem As String

    xhr.Open "POST", "http://localhost:3000/veiculos" , False

    xhr.setRequestHeader "Content-Type", "application/json"
    
    xhr.send JSON(id, placa, descricao, url_imagem, marca, modelo, quantidade_lugares, is_site)

    If xhr.Status = 200 Then
        mensagem = xhr.responseText
    Else
        mensagem = "Erro ao acessar a API: " & xhr.Status & " - " & xhr.statusText
    End If

    Set xhr = Nothing

    enviaCadastro = mensagem
End Function
