

const connect = require('../database');

const listagem = async (req, res) => {
    try {
        await connect.connect();

        const result = await connect.query('SELECT * FROM veiculo_site order by id desc');

        const { recordset } = result;

        res.render('Veiculos', { title: 'Meu Veiculos', veiculos: recordset });
    } catch (err) {
        res.status(500).send('Erro ao buscar registros.');
    }
}

const deletarTudo = async(req, res) =>{
    try {
        await connect.connect();

        await connect.query(`delete from dbo.veiculo_site`);

        return res.send('Todas publicações foram excluidas com sucesso!');
    } catch (error) {
        res.status(500).send('Não foi possivel limpar todas publicações!');
    }
}

const cadastrar = async (req, res) => {
    try {
        await connect.connect();

        const veiculo = req.body;

        if (veiculo && veiculo.is_site == 0) {
            await connect.query(`delete from dbo.veiculo_site where id_rp = ${veiculo.id_rp}`);
            return res.send('Cadastro foi removido site!');
        }

        await connect.request()
            .input('id_rp', veiculo.id_rp)
            .input('placa', veiculo.placa)
            .input('descricao', veiculo.descricao)
            .input('url_imagem', veiculo.url_imagem)
            .input('marca', veiculo.marca)
            .input('modelo', veiculo.modelo)
            .input('quantidade_lugares', veiculo.quantidade_lugares)
            .query('INSERT INTO dbo.veiculo_site (id_rp, placa, descricao, url_imagem, marca, modelo, quantidade_lugares) VALUES (@id_rp, @placa, @descricao, @url_imagem, @marca, @modelo, @quantidade_lugares)');

        res.send('Cadastro enviado e postado no site!');
    } catch (err) {
        res.status(500).send('Não foi possivel enviar cadastro para o site!');
    }
}

module.exports = {
    listagem,
    cadastrar,
    deletarTudo
}