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
    console.log(req.query);
    const username = req.query.username;
    const password = req.query.password;
    const passwordRepeat = req.query.passwordRepeat;
    let response;
    let responseStatus;
    
    if (password !== passwordRepeat) {
        response = { 'response': 'Passwords must match' };
        responseStatus = 400;
    } else {
        const passwordHash = await bcrypt.hash(password, SALT_ROUNDS);

        const db = await dbPromise;
        await db.run('INSERT INTO USER (username, password) VALUES (?, ?)', username, passwordHash
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
    const username = req.query.username;
    const password = req.query.password || null;
    let response;
    let responseStatus;

    const db = await dbPromise;
    
    await db.get('SELECT * FROM USER WHERE username=\'' + username + '\'', []
    ).then(async (row) => {
        await bcrypt.compare(password, row.password
        ).then((result) => {
            if (result) {
                response = {
                    'success': true,
                    'username': row.username, 
                    'balance': row.balance
                };
                responseStatus = 200;
            } else {
                response = { 
                    'success': false,
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

export default {
    router, 
    dbPromise
}