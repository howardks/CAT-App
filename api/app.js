const express = require('express');
const app = express();

const routes = require('./routes');
app.use('/', routes);

const PORT  = process.env.PORT || 3000
app.listen(PORT, () => {
    console.info(`Server has started on ${PORT} - http://localhost:${PORT}`)
});

// https://blog.postman.com/how-to-create-a-rest-api-with-node-js-and-express/
