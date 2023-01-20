/*Connection with mongoDB using mongoose as handler*/

const mongoose = require('mongoose');

require('dotenv').config();

mongoose
        .connect(DB = process.env.DB, {useNewUrlParser: true})
        .then(() => {
            console.log('Connected successfully')})
        .catch((err) => {
            console.log('Unable to connect', err)
        })