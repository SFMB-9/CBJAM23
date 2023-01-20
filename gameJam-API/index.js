const express = require('express');
const cors = require('cors');
const router = require('./routes/routes');
const db = require('./database/db');

require('dotenv').config();

const app = express();

app.use(cors())
app.use(express.json())
app.use(express.urlencoded({extended: false}))
db
app.use(router)

app.listen(process.env.PORT, () =>{
    console.log(`Connected on port ${process.env.PORT}`)
})
