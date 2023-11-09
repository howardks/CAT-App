import express from 'express';
import routeInfo from './routes/routes.js'; 

const app = express();
app.use(express.json());

const router = routeInfo.router;
app.use('/', router);

const dbPromise = routeInfo.dbPromise;
const setup = async() => {
    const db = await dbPromise;
    await db.migrate();

    const PORT  = process.env.PORT || 3000
    app.listen(PORT, () => {
        console.info(`Server has started on ${PORT} - http://localhost:${PORT}`)
    });
}
setup();
