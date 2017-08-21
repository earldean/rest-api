use artist;

create table dbo.Albums
(Id int identity(1, 1) primary key not null,
ArtistId int not null,
AlbumName nvarchar(128) not null,
genre nvarchar(128) not null,
albumYear int not null)
