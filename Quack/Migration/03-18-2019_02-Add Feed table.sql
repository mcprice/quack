CREATE TABLE Feed (
  FeedID INT NOT NULL IDENTITY PRIMARY KEY,
  FeedTime datetime NOT NULL,
  FeedTypeID int NOT NULL,
  FeedLocation varchar(max) NOT NULL,
  FeedGroupSize int NOT NULL,
  FeedKg numeric(9,0) NOT NULL,
  UserID int NOT NULL,
  constraint fk_feed_user foreign key (UserID) references Users (UserID),
  constraint fk_feed_feed_type foreign key (FeedTypeID) references FeedType (FeedTypeID)
);