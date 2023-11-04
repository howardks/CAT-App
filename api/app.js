import express from 'express';
import routeInfo from './routes.js'; 

const app = express();

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

// https://www.youtube.com/watch?v=qdV9S7UE99o