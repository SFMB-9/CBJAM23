const { model } = require('mongoose');
const scoreController = require('../controller/scoreController');

const router = require('express').Router();

router.post('/newScore', scoreController.setScore);


module.exports = router;