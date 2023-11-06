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
    res.send(status);
});

router.get('/users', async (req, res) => {
    const db = await dbPromise;
    const data = await db.all('SELECT * FROM User;');
    res.send(data);
});

router.post('/register', async (req, res) => {
    console.log(req.query);
    const username = req.query.username;
    const password = req.query.password;
    const passwordRepeat = req.query.passwordRepeat;
    let response;
    
    if (password !== passwordRepeat) {
        response = {'response': 'Passwords must match'};
    } else {
        const passwordHash = await bcrypt.hash(password, SALT_ROUNDS);

        const db = await dbPromise;
        await db.run('INSERT INTO USER (username, password) VALUES (?, ?)', username, passwordHash)
            .then(() => {
                response = {'response': 'Registered successfully'}
            })
            .catch(() => {
                response = {'response': 'Username unavailable'}
            });
    }
        res.send(response);
});

router.post('/login', async (req, res) => {
    console.log(req.query);
    const username = req.query.username;
    const password = req.query.password;
    let response;

});

export default {
    router, 
    dbPromise
}