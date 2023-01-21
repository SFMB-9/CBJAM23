/*Saves score in the database*/
const mongoose = require('mongoose');
const score_ = require('../model/scoresSchema')

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

        const Score = await new score_({
            Name: name,
            Score: score
        }).save();

        return res.status(201).send({
            message: `Score saved!`
        })
    }catch(err){
        console.log(err);
        return res.status(400).send(err);
    }
}

exports.highScores = async(req, res) =>{
    try{
        /*Find all the scores save in database*/
        const Score = await score_.find({}, function(err, scores){
            if(err){
                return res.status(402).send({
                    message: 'Unexpected error'
                })
            }else{
                /*Sort scores from highest to lowest*/
                scores.sort((a,b) => a.Score < b.Score ? 1: -1);
                return res.status(200).send({"score": scores.slice(0, 10)});
            }
        })

    }catch(err){

    }
}