namespace timelineService.Models;

public class Tweet
{
    public string Id{get; }
    
    public string UserID{get; }
    public DateTime CreationTime{get;}
   
    public string tweet { get; }  
    
    
    public string? ImageURL { get; }

    public Tweet(string Id, string UserID, DateTime CreationTime, string tweet, string imageUrl)
    {
        this.Id = Id;
        this.UserID = UserID;
        this.CreationTime = CreationTime;
        this.tweet = tweet;
        this.ImageURL = imageUrl;
    }
}