import http from 'k6/http'

export function getTimelineLoadTest(){

    var id ="001dc4b4-6372-4d2b-af60-2b588d7b8f07"


    http.get(`http://localhost:5106/api/tweetProcessor/populateTimeline/${id}`)
}