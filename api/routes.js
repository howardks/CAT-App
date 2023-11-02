const router = require('express').Router();

router.get("/status", (req, res) => {
    const status = {
        "Status": "Running"
    };
    res.send(status);
});

module.exports = router;