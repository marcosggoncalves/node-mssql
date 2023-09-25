
const express = require('express');
const router = express.Router();

const {listagem, cadastrar, deletarTudo} = require('../controllers/Veiculos');

router.get('/', listagem);
router.post('/', cadastrar);
router.post('/deletar-todos', deletarTudo);

module.exports = router;
