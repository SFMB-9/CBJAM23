/*Saves score in the database*/
const mongoose = require('mongoose');
const score = require('../model/scoresSchema')

exports.setScore = async(req, res) => {
    try{

        const {
            name,
            score
        } = req.body


        /*Check for missing data*/
        if(!name)
            return res.status(400).send({message: 'Missing name'});
        if(!score)
            return res.status(400).send({message: 'Missing score'});
        
        /*Save information in database*/

        const Score = await new score({
            Name: name,
            Score: score
        }).save();

        return res.status(201).send({
            message: `Score saved!`
        })
    }catch(err){

    }
}