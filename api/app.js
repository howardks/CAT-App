const express = require('express');
const app = express();

const PORT  = process.env.PORT || 3000
app.listen(PORT, () => {
    console.info(`Server has started on ${PORT} - http://localhost:${PORT}`)
});

app.get("/status", (req, res) => {
    const status = {
        "Status": "Running"
    };
    res.send(status);
});
