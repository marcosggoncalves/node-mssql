const sql = require('mssql');

const config = {
  user: 'sa',
  password: 'DBd4t43xp0rt@',
  server: '172.20.0.201',
  database: 'marcos',
  options: {
    encrypt: false
  },
};

module.exports =  new sql.ConnectionPool(config);
