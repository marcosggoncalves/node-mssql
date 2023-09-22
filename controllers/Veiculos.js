


const listagem = async (req,res) => {
    try {
        const result = await connect.query('SELECT * FROM veiculo where is_site = 1  order by id desc');

        res.render('Veiculos', { title: 'Meu Veiculos', veiculos: result.recordset }); 
    } catch (err) {
        res.status(500).send('Erro ao buscar registros.');
    }
}

const cadastrar = async(req, res) => {
    try {
        await connect.connect();

        const veiculo = req.body;

        if(veiculo && veiculo.is_site == 0){
            await connect.query(`delete from dbo.veiculo where id = ${veiculo.id}`);
            res.send('Cadastro removido foi removido site!');
            return;
        }

        await connect.request()
            .input('placa', veiculo.placa)
            .input('descricao', veiculo.descricao)
            .input('url_imagem', veiculo.url_imagem)
            .input('marca', veiculo.marca)
            .input('modelo', veiculo.modelo)
            .input('quantidade_lugares', veiculo.quantidade_lugares)
            .input('is_site', veiculo.is_site)
            .query('INSERT INTO dbo.veiculo (placa, descricao, url_imagem, marca, modelo, quantidade_lugares, is_site) VALUES (@placa, @descricao, @url_imagem, @marca, @modelo, @quantidade_lugares, @is_site)');

        res.send('Cadastro enviado e postado no site!'); 
    } catch (err) {
        res.status(500).send('Não foi possivel enviar cadastrado e postar!');
    }
} 

module.exports = {
    listagem,
    cadastrar
}