const multer  = require('multer')
const upload = multer()
const scoreController = require('../controller/scoreController');

const router = require('express').Router();

router.post('/newScore', upload.none() ,scoreController.setScore);
router.get('/highScores', scoreController.highScores);


module.exports = router;