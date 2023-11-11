import express from 'express';
import sqlite3 from 'sqlite3';
import { open } from 'sqlite';
import bcrypt from 'bcrypt';

const router = express.Router();

const dbPromise = open({
    filename: './storage/data.db',
    driver: sqlite3.Database
});

const SALT_ROUNDS = 10;

router.get('/', (req, res) => {
    const status = {
        'Status': 'Running'
    };
    res.status(200).json(status);
});

router.get('/users', async (req, res) => {
    const db = await dbPromise;
    const data = await db.all('SELECT * FROM User;');
    res.status(200).json(data);
});

router.post('/register', async (req, res) => {
    const username = req.body.username;
    const password = req.body.password;
    const passwordRepeat = req.body.passwordRepeat;
    const accountType = req.body.accountType;
    let response;
    let responseStatus;

    if (password !== passwordRepeat) {
        response = { 'response': 'Passwords must match' };
        responseStatus = 400;
    } else {
        const passwordHash = await bcrypt.hash(password, SALT_ROUNDS);

        const db = await dbPromise;
        await db.run('INSERT INTO USER (username, password, accountType) VALUES (?, ?, ?)', username, passwordHash, accountType
        ).then(() => {
            response = { 'response': 'Registered successfully' };
            responseStatus = 201;
        }).catch(() => {
            response = { 'response': 'Username unavailable' };
            responseStatus = 400;
        });
    }

    res.status(responseStatus).json(response);
});

router.post('/login', async (req, res) => {
    const username = req.body.username;
    const password = req.body.password;
    let response;
    let responseStatus;

    const db = await dbPromise;

    await db.get('SELECT * FROM USER WHERE username=?', username
    ).then(async (row) => {
        await bcrypt.compare(password, row.password
        ).then(async (result) => {
            const history = await db.all('SELECT * FROM HISTORY WHERE userId=?', row.id);

            if (result) {
                response = {
                    'success': "true",
                    'username': row.username,
                    'accountType': row.accountType,
                    'balance': row.balance,
                    'history': history
                };
                responseStatus = 200;
            } else {
                response = {
                    'success': "false",
                    'response': 'Incorrect password'
                };
                responseStatus = 400;
            }
        }).catch((err2) => {
            response = {
                'success': false,
                'response': 'Missing password'
            };
            responseStatus = 400;
        });

    }).catch((err) => {
        response = {
            'success': false,
            'response': 'Username does not exist'
        };
        responseStatus = 400;
    });

    res.status(responseStatus).json(response);
});

router.patch('/updateBalance', async (req, res) => { // This may be less redundant if more authentication is added
    console.log(req.body);
    const username = req.body.username;
    const password = req.body.password;
    const transactionType = req.body.transactionType;
    const amount = parseFloat(req.body.amount);
    let response;
    let responseStatus;

    const db = await dbPromise;

    await db.get('SELECT * FROM USER WHERE username=?', username
    ).then(async (row) => {
        await bcrypt.compare(password, row.password
        ).then(async (result) => {
            if (result) {
                await db.run('UPDATE USER SET balance=? WHERE username=?', [row.balance + amount, username]
                ).then(async () => {
                    await db.get('SELECT * FROM USER WHERE username=?', username
                    ).then((row) => {
                        response = {
                            'success': 'true',
                            'username': row.username,
                            'balance': row.balance
                        };
                        responseStatus = 200;
                    }).catch((err4) => {
                        response = {
                            'success': 'false',
                            'response': 'Error retrieving user'
                        };
                        responseStatus = 400;
                    });
                }).catch((err3) => {
                    response = {
                        'success': 'false',
                        'response': 'Error updating balance'
                    };
                    responseStatus = 400;
                });
            } else {
                response = {
                    'success': 'false',
                    'response': 'Incorrect password'
                };
                responseStatus = 400;
            }
        }).catch((err2) => {
            response = {
                'success': 'false',
                'response': 'Missing password'
            };
            responseStatus = 400;
        });
    }).catch((err) => {
        response = {
            'success': 'false',
            'response': 'Username does not exist'
        };
        responseStatus = 400;
    });

    res.status(responseStatus).json(response);
});

export default {
    router,
    dbPromise
}