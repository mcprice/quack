CREATE TABLE Feed (
  FeedID INT NOT NULL IDENTITY PRIMARY KEY,
  FeedTime datetime NOT NULL,
  FeedType varchar(max) NOT NULL, /* Ideally this would be a foreign key reference to a FeedType Table ID*/
  FeedLocation varchar(max) NOT NULL,
  FeedGroupSize int NOT NULL,
  FeedGrams numeric(9,0) NOT NULL, 
  UserID int NOT NULL,
  constraint fk_feed_user foreign key (UserID) references Users (UserID)
);