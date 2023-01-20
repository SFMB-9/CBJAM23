const mongoose = require('mongoose');

const score = mongoose.Schema({

    name: {
        type: String
    },
    score: {
        type: Number
    }
})

module.exports = mongoose.model("scores", score);