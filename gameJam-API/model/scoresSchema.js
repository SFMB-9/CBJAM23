const mongoose = require('mongoose');

const score = mongoose.Schema({

    Name: {
        type: String
    },
    Score: {
        type: Number
    }
})

module.exports = mongoose.model("scores", score);