// controllers/userController.js
const User = require('../models/User');
const jwt = require('jsonwebtoken');
const bcrypt = require('bcrypt');

exports.register = async (req, res) => {
  const { username, password } = req.body;
  try {
    const hashed = await bcrypt.hash(password, 10);
    const user = new User({ username, password: hashed });
    await user.save();
    res.status(201).json({ msg: 'User registered' });
  } catch (err) {
    res.status(400).json({ msg: 'Username already exists' });
  }
};

exports.login = async (req, res) => {
  const { username, password } = req.body;
  const user = await User.findOne({ username });
  if (!user || !(await bcrypt.compare(password, user.password)))
    return res.status(401).json({ msg: 'Invalid credentials' });

  const token = jwt.sign({ id: user._id, username }, process.env.JWT_SECRET, { expiresIn: '1d' });
  res.json({ token, clearedStage: user.clearedStage });
};

exports.saveStage = async (req, res) => {
  const { stage } = req.body;
  const user = await User.findById(req.user.id);
  if (stage > user.clearedStage) {
    user.clearedStage = stage;
    await user.save();
  }
  res.json({ msg: 'Stage updated', clearedStage: user.clearedStage });
};

exports.getRanking = async (req, res) => {
  const users = await User.find().sort({ clearedStage: -1 }).select('username clearedStage -_id');
  res.json(users);
};
