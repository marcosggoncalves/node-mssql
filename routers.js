
const express = require('express');
const router = express.Router();

const {listagem, cadastrar} = require('./controllers/Veiculos');

router.get('/', listagem);
router.post('/', cadastrar);

module.exports = router;
