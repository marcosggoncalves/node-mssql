const express = require('express');
const app = express();
const port = 3000;

const routesVeiculos = require('./routers/Veiculos');

app.use(express.json());

app.set('view engine', 'ejs');
app.set('views', __dirname + '/views');

app.use('/veiculos', routesVeiculos);

app.listen(port, () => {
  console.log(`Servidor rodando na porta ${port}`);
});
