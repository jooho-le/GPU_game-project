// routes/userRoutes.js
const express = require('express');
const router = express.Router();
const controller = require('../controllers/userController');
const auth = require('../middleware/authMiddleware');

router.post('/register', controller.register);
router.post('/login', controller.login);
router.post('/stage', auth, controller.saveStage);
router.get('/ranking', controller.getRanking);

module.exports = router;
