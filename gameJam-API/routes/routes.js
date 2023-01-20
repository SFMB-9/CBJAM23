const { model } = require('mongoose');
const scoreController = require('../controller/scoreController');

const router = require('express').Router();

router.post('/newScore', scoreController.setScore);
router.get('/highScores', scoreController.highScores);


module.exports = router;