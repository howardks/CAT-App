import express from 'express';
import sqlite3 from 'sqlite3';
import { open } from 'sqlite';

const router = express.Router();

const dbPromise = open({
    filename: './storage/data.db',
    driver: sqlite3.Database
});

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
    const db = await dbPromise;
    const username = req.query.username;
    const password = req.query.password;
    const balance = (req.query.balance) ? req.query.balance : 0.0;
    await db.run('INSERT INTO USER (username, password, balance) VALUES (?, ?, ?)', username, password, balance);
    res.redirect('/users');
});

router.get('/status', (req, res) => {
    const status = {
        'Status': 'Running'
    };
    res.send(status);
});

export default {
    router, 
    dbPromise
}